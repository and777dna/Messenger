using Messenger.DTOs.Requests;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.Hubs;

public class SignalRChatNotifier(
    IHubContext<ChatHub, IChatClient<RequestMessageDto>> hub,
    ILogger<SignalRChatNotifier> logger) : IChatNotifier
{
    public Task MessagePostedAsync(RequestMessageDto message)
    {
        var group = $"room:{message.ChatId}";
        logger.LogDebug("Sending message for chat {ChatId} to SignalR group {Group}", message.ChatId, group);
        return hub.Clients.Group(group).ReceiveMessage(message);
    }
}