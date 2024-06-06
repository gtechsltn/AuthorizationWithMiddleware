using Microsoft.AspNetCore.Mvc;
using AuthorizationWithMiddleware.Models;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User user)
    {
        var authenticatedUser = _userService.Authenticate(user.Username, user.Password);
        if (authenticatedUser == null)
            return Unauthorized("Invalid username or password");

        var apiKey = _userService.GenerateApiKey(authenticatedUser);
        return Ok(new { ApiKey = apiKey.Key });
    }
}
