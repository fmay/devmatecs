using System;
using System.Text;
using Crotchety.Services;
using Crotchety.Domain.Finder;
using Microsoft.AspNetCore.Mvc;

namespace Crotchety.Controllers;

[ApiController]
[Route("[controller]")]
public class FinderController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public FinderController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<string> Post()
    {
        //string rawContent = string.Empty;
        StreamReader reader = new StreamReader(Request.Body);
        string rawContent = await reader.ReadToEndAsync();
        //using (var reader = new StreamReader(Request.Body,
        //              encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false))
        //{
        //    rawContent = await reader.ReadToEndAsync();
        //}
        //return "OK";
        return await FinderService.RunQuery(rawContent);
    }


}