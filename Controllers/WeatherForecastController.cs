using Amazon.Runtime.CredentialManagement;
using Event_planner.Data;
using Event_planner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    private EventPlannerContext context;
    public WeatherForecastController(ILogger<WeatherForecastController> logger, ISingletonSecretsManagerService secretsManagerService, EventPlannerContext context)
    {
        _logger = logger;
        this.smService = secretsManagerService;
        this.context = context;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {


        return new ObjectResult(Summaries);
    }
}
