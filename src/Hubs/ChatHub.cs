using Messenger.DTOs.Requests;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.Hubs;

public class ChatHub(IChatService chatService) : Hub<IChatClient<RequestMessageDto>>
{
    public async Task SendMessage(RequestMessageDto messageDto)
    {
        await chatService.SendMessageAsync(messageDto);
    }
}