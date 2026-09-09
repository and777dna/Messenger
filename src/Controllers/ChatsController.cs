using Messenger.DTOs.Requests;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[ApiController]
[Route("api/chats")]
public class ChatsController(IChatService chatService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMine([FromQuery] Guid userId) =>
        Ok(await chatService.GetChatsForUserAsync(userId));

    [HttpPost]
    public async Task<IActionResult> Create([FromQuery] Guid userId, CreateChatRequest request) =>
        Ok(await chatService.CreateDirectChatAsync(userId, request));
}
