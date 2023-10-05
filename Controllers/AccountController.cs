using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;

namespace Blog.Controllers;

[ApiController]
public class AccountController : ControllerBase
{

    [HttpPost("v/accounts/")]
    public async Task<IActionResult> Post(
        [FromBody]RegisterViewModel model,
        [FromServices]BlogDataContext context)
    {
        if(!ModelState.IsValid) 
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var user = new User
        {
            nameof = model.Name,
            Email = model.Email,
            slug = model.Email.Replace("@", "-").Replace(".", "-")
        };
    }

    [HttpPost("v1/accounts/login")]
    public IActionResult Login([FromServices]TokenService tokenService)
    {
        var token = tokenService.GenerateToken(null);

        return Ok(token);
    }

}

