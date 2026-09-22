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
    IMapper<Chat, ResponseChatDto> mapperChat, IMessageRepository messageRepository, 
    IRepository<Chat> chatRepository, IDistributedCache cache, IChatNotifier notifier) : IChatService
{
    public async Task SendMessageAsync(RequestMessageDto requestMessageDto, CancellationToken ct = default)
    {
        var cacheKey = CacheKeys.ChatMessages(requestMessageDto.ChatId);
        var message = mapperMessages.ToEntity(requestMessageDto);
        //requestMessageDto.Operation = new AddMessageOperation(message);//TODO: to decide whether to implement this for DI
        await cache.RefreshAsync(cacheKey);
        await messageRepository.AddAsync(message);
        await notifier.MessagePostedAsync(requestMessageDto);
    }

    public async Task<IReadOnlyList<Message>> GetMessagesAsync(Guid chatId, int limit, CancellationToken ct = default)
    {
        var cacheKey = CacheKeys.ChatMessages(chatId);
        var cachedData = await cache.GetStringAsync(cacheKey);
        if (cachedData == null)
        {
            var messages = await messageRepository.GetLatestAsync(chatId, limit);
            
            var options = new DistributedCacheEntryOptions {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };
            
            await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(messages), options);
            return messages;
        }
        return JsonSerializer.Deserialize<List<Message>>(cachedData);
    }
}