using System.ComponentModel.DataAnnotations;
using Messenger.DTOs.Requests;
using Messenger.Entities.Operations;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[ApiController]
[Route("api/chats")]
public class ChatController(IChatService chatService) : ControllerBase
{
    private readonly CancellationTokenSource _tokenSource = new();
    [HttpPost("messages")]
    public async Task<IActionResult> AddMessage(RequestMessageDto requestMessageDto, 
        [FromHeader(Name = "Idempotency-Key"), Required] Guid? idempotencyKey)
    {
        await chatService.SendMessageAsync(requestMessageDto, _tokenSource.Token);
        return Ok();
    }
}