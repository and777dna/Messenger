using Messenger.DTOs.Requests;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RequestUserDto requestUserDto, CancellationToken cancellationToken)
    {
        await userService.AddUserAsync(requestUserDto, cancellationToken);
        return Ok();
    }
}