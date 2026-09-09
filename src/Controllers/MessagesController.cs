using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[ApiController]
[Route("api/messages")]
public class MessagesController(IMessageService messageService) : ControllerBase
{
    [HttpGet("{chatId:guid}")]
    public async Task<IActionResult> GetHistory(Guid chatId, [FromQuery] DateTime? since) =>
        Ok(await messageService.GetHistoryAsync(chatId, since));
}
