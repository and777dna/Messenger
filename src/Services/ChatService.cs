using System.Text.Json;
using Messenger.Caching;
using Messenger.DTOs.Requests;
using Messenger.Entities;
using Messenger.MapperDto.Interfaces;
using Messenger.Repositories.Interfaces;
using Messenger.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Messenger.Services;

public class ChatService(IMapper<Message, RequestMessageDto> mapperMessages,
    IMessageRepository messageRepository, IDistributedCache cache, IChatNotifier notifier,
    ILogger<ChatService> logger) : IChatService
{
    public async Task SendMessageAsync(RequestMessageDto requestMessageDto, CancellationToken ct)
    {
        var cacheKey = CacheKeys.ChatMessages(requestMessageDto.ChatId);
        var message = mapperMessages.ToEntity(requestMessageDto);
        //requestMessageDto.Operation = new AddMessageOperation(message);//TODO: to decide whether to implement this for DI
        var added = await messageRepository.AddAsync(message, ct);
        if (!added)
        {
            logger.LogInformation(
                "Duplicate message for chat {ChatId} ignored, idempotency key {IdempotencyKey} was already used",
                requestMessageDto.ChatId, requestMessageDto.IdempotencyKey);
            return;
        }

        logger.LogInformation("Message {MessageId} saved to chat {ChatId} by sender {SenderId}",
            message.Id, requestMessageDto.ChatId, requestMessageDto.SenderId);

        await cache.RefreshAsync(cacheKey, ct);
        await notifier.MessagePostedAsync(requestMessageDto);
    }

    public async Task<IReadOnlyList<Message>> GetMessagesAsync(Guid chatId, int limit, CancellationToken ct)
    {
        var cacheKey = CacheKeys.ChatMessages(chatId);
        var cachedData = await cache.GetStringAsync(cacheKey, ct);
        if (cachedData == null)
        {
            logger.LogDebug("Cache miss for chat {ChatId}, loading messages from database", chatId);
            var messages = await messageRepository.GetLatestAsync(chatId, limit, ct: ct);
            
            var options = new DistributedCacheEntryOptions {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };
            
            await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(messages), options, ct);
            return messages;
        }

        logger.LogDebug("Cache hit for chat {ChatId}", chatId);
        var messagesDeserialize = JsonSerializer.Deserialize<List<Message>>(cachedData);
        if (messagesDeserialize == null)
        {
            throw new InvalidOperationException($"Cached messages for chat {chatId} could not be deserialized.");
        }
        return messagesDeserialize;
    }
}