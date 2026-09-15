using Messenger.DTOs.Requests;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly CancellationTokenSource _tokenSource = new();
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RequestUserDto requestUserDto)
    {
        await userService.AddUserAsync(requestUserDto, _tokenSource.Token);
        return Ok();
    }
}