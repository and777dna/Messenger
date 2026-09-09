using Messenger.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserRepository userRepository) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id) =>
        Ok(await userRepository.GetByIdAsync(id));
}
