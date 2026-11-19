using System.Text.Json;
using Messenger.Caching;
using Messenger.DTOs.Requests;
using Messenger.DTOs.Responses;
using Messenger.Entities;
using Messenger.MapperDto.Interfaces;
using Messenger.Repositories.Interfaces;
using Messenger.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Messenger.Services;

public class ChatService(IMapper<Message, RequestMessageDto> mapperMessages,
    IResponseMapper<Message, ResponseMessageDto> responseMapper,
    IMessageRepository messageRepository, IDistributedCache cache, IChatNotifier notifier,
    ILogger<ChatService> logger) : IChatService
{
    // The cache always holds the latest page of a chat; smaller limits are served from it.
    private const int CachedPageSize = 50;

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

        // Invalidate the cached page; the next read reloads it from the database.
        await cache.RemoveAsync(cacheKey, ct);
        await notifier.MessagePostedAsync(requestMessageDto);
    }

    public async Task<IReadOnlyList<ResponseMessageDto>> GetMessagesAsync(Guid chatId, int limit, CancellationToken ct)
    {
        var cacheKey = CacheKeys.ChatMessages(chatId);
        var cachedData = await cache.GetStringAsync(cacheKey, ct);
        if (cachedData == null)
        {
            logger.LogDebug("Cache miss for chat {ChatId}, loading messages from database", chatId);
            var messages = await messageRepository.GetLatestAsync(chatId, CachedPageSize, ct: ct);
            var dtos = messages.Select(responseMapper.ToDto).ToList();

            var options = new DistributedCacheEntryOptions {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };

            await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(dtos), options, ct);
            return dtos.Take(limit).ToList();
        }

        logger.LogDebug("Cache hit for chat {ChatId}", chatId);
        var cachedDtos = JsonSerializer.Deserialize<List<ResponseMessageDto>>(cachedData)
            ?? throw new InvalidOperationException($"Cached messages for chat {chatId} could not be deserialized.");
        return cachedDtos.Take(limit).ToList();
    }
}
