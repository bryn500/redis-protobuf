using Microsoft.AspNetCore.Mvc;
using Redis.Client;
using Redis.Models;

namespace Redis.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private readonly ICacheClient _cacheClient;
    private readonly ILogger<ApiController> _logger;

    public ApiController(ICacheClient cacheClient, ILogger<ApiController> logger)
    {
        _cacheClient = cacheClient;
        _logger = logger;
    }

    [HttpGet(Name = "stuff")]
    public async Task<IEnumerable<Data>> Get()
    {
        return await _cacheClient.GetData();
    }
}
