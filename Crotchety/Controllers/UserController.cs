using Microsoft.AspNetCore.Mvc;
using Crotchety.Services;

namespace Crotchety.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public UserController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet("list")]
    public async Task<string> Get()
    {
        _logger.LogInformation(HttpContext.Request.Method + " " + HttpContext.Request.Path);
        return await UserService.GetUsers();
    }

    [HttpGet]
    public async Task<string> GetUser(string id)
    {
        _logger.LogInformation(HttpContext.Request.Method + " " + HttpContext.Request.Path);
        return await UserService.GetUser(id);
    }
    
}

