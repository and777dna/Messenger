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
        if (idempotencyKey == Guid.Empty)throw new ArgumentException("Idempotency key cannot be empty.", nameof(idempotencyKey));
        requestMessageDto.IdempotencyKey = idempotencyKey;
        await chatService.SendMessageAsync(requestMessageDto, cancellationToken);
        return Ok();
    }
}