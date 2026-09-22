using Messenger.DTOs.Requests;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.Hubs;

public class SignalRChatNotifier(IHubContext<ChatHub, IChatClient<RequestMessageDto>> hub) : IChatNotifier
{
    public Task MessagePostedAsync(RequestMessageDto message)
        => hub.Clients.Group($"room:{message.ChatId}").ReceiveMessage(message);
}