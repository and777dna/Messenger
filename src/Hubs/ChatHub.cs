using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.Hubs;

public class ChatHub(IMessageService messageService) : Hub
{
    public Task SendMessage(Guid chatId, string content) =>
        throw new NotImplementedException();

    public Task JoinChat(Guid chatId) =>
        throw new NotImplementedException();
}
