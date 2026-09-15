using Messenger.DTOs.Requests;

namespace Messenger.Services.Interfaces;

public interface IChatService
{
    public Task SendMessageAsync(RequestMessageDto requestMessageDto, CancellationToken ct = default);
}