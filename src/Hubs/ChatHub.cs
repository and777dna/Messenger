using Messenger.DTOs.Requests;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.Hubs;

public class ChatHub(IChatService chatService) : Hub<IChatClient<RequestMessageDto>>
{
    public async Task JoinChat(Guid chatId) => 
        await Groups.AddToGroupAsync(Context.ConnectionId, $"room:{chatId}");
    public async Task SendMessage(RequestMessageDto messageDto)
    {
        await Clients.Group($"room:{messageDto.ChatId}").ReceiveMessage(messageDto);
    }
}