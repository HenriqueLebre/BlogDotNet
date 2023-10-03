using Blog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blog.Controllers;

[ApiController]
public class AccountController : ControllerBase
{

    [HttpPost("v1/login")]
    public IActionResult Login([FromServices]TokenService tokenService)
    {
        var token = tokenService.GenerateToken(null);

        return Ok(token);
    }
}

