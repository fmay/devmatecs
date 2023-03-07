using Microsoft.AspNetCore.Mvc;
using Crotchety.Services;

namespace Crotchety.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public string Get()
    {
        return "Hello";
    }

    [HttpGet("ping")]
    public string ping()
    {
        return "pong";
    }

    [HttpGet("database")]
    public async Task<string> database()
    {
        return await HealthService.Database();
    }

}

