using Amazon.Runtime.CredentialManagement;
using Event_planner.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event_planner.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private ISingletonSecretsManagerService smService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ISingletonSecretsManagerService secretsManagerService )
    {
        _logger = logger;
        this.smService = secretsManagerService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {


        var dbSecret = smService.getDatabaseCredentialAsync("prodKey");
        return  new ObjectResult(dbSecret);
    }
}
