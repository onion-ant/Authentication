using Authentication.API.Extensions;
using Authentication.Application.Interfaces;
using Authentication.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        var result = await _userService.CreateUserAsync(request);
        return result.ToActionResult();
    }
}
