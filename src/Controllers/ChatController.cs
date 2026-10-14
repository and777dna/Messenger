using System.ComponentModel.DataAnnotations;
using Messenger.DTOs.Requests;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[ApiController]
[Route("api/chats")]
public class ChatController(IChatService chatService) : ControllerBase
{
    [HttpPost("messages")]
    public async Task<IActionResult> AddMessage(RequestMessageDto requestMessageDto,
        [FromHeader(Name = "Idempotency-Key"), Required] Guid? idempotencyKey, CancellationToken cancellationToken)
    {
        await chatService.SendMessageAsync(requestMessageDto, cancellationToken);
        return Ok();
    }
}