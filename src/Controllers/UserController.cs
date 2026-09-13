using Messenger.Entities;
using Messenger.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[ApiController]
[Route("user")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly CancellationTokenSource _tokenSource = new();
    [HttpPost]
    public IActionResult RegisterUser(User user)
    {
        userService.AddUserAsync(user, _tokenSource.Token);
        return Ok();
    }
}