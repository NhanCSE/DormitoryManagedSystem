using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private ILoggerManager _logger;

    public WeatherForecastController(ILoggerManager logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInfo("Here is info message from our values controller." );
        _logger.LogDebug( "Here is debug message from our values controller." );
        _logger.LogWarn("Here is warn message from our values controller." );
        _logger.LogError( "Here is an error message from our values controller." );
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
        
    }
}
