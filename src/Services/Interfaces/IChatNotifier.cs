using Messenger.DTOs.Requests;

namespace Messenger.Services.Interfaces;

public interface IChatNotifier
{
    public Task MessagePostedAsync(RequestMessageDto message);
}