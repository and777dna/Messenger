using Messenger.DTOs.Requests;
using Messenger.DTOs.Responses;
using Messenger.Entities;
using Messenger.MapperDto.Interfaces;
using Messenger.Repositories.Interfaces;
using Messenger.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Messenger.Services.Implementations;

public class ChatService(IMapper<Message, RequestMessageDto> mapperMessages,IMapper<Chat, ResponseChatDto> mapperChat, IRepository<Message> messageRepository, IRepository<Chat> chatRepository, IDistributedCache cache) : IChatService
{
    public async Task SendMessageAsync(RequestMessageDto requestMessageDto, CancellationToken ct = default)
    {
        var message = mapperMessages.ToEntity(requestMessageDto);
        //requestMessageDto.Operation = new AddMessageOperation(message);//TODO: to decide whether to implement this for DI
        await messageRepository.AddAsync(message);
    }

    public async Task<ResponseChatDto> GetMessagesAsync(Guid chatId, int page, int pageSize, CancellationToken ct = default)
    {
        var messages = await chatRepository.GetByIdAsync(chatId, page, pageSize);
        var chatToDto = mapperChat.ToDto(messages);
        return chatToDto;
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