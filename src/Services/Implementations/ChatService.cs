using Messenger.Caching;
using Messenger.DTOs.Requests;
using Messenger.DTOs.Responses;
using Messenger.Entities;
using Messenger.MapperDto.Interfaces;
using Messenger.Repositories.Interfaces;
using Messenger.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Messenger.Services.Implementations;

public class ChatService(IMapper<Message, RequestMessageDto> mapperMessages,IMapper<Chat, ResponseChatDto> mapperChat, IMessageRepository messageRepository, IRepository<Chat> chatRepository, IDistributedCache cache) : IChatService
{
    
    public async Task SendMessageAsync(RequestMessageDto requestMessageDto, CancellationToken ct = default)
    {
        var message = mapperMessages.ToEntity(requestMessageDto);
        //requestMessageDto.Operation = new AddMessageOperation(message);//TODO: to decide whether to implement this for DI
        await messageRepository.AddAsync(message);
    }

    public async Task<string> GetMessagesAsync(Guid chatId, int limit, CancellationToken ct = default)
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
            
            await cache.SetStringAsync(cacheKey, messages, options);
            return messages;
        }
        return cachedData;
    }
    
    /*public async Task<string> GetDataAsync(string key) {
           var cachedData = await _cache.GetStringAsync(key);

           if (cachedData == null) {

               // Fetch the data from source.
               var data = await FetchDataFromSource();

               // Cache the data with options.
               var options = new DistributedCacheEntryOptions {
                  AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                  SlidingExpiration = TimeSpan.FromMinutes(5)
               };

               await _cache.SetStringAsync(key, data, options);
               return data;
           }

           return cachedData;
       }*/
}