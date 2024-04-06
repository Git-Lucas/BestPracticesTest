using BestPracticesTest.Entities;
using BestPracticesTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace BestPracticesTest.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(IWeatherForecastService weatherForecastService) : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService = weatherForecastService;

    [HttpPost]
    [ProducesResponseType(typeof(int[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync()
    {
        int[] weatherForecastsIds = await _weatherForecastService.CreateRangeAsync();

        return Created(string.Empty, weatherForecastsIds);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        IEnumerable<WeatherForecast> weatherForecasts = await _weatherForecastService.GetAllAsync();

        return Ok(weatherForecasts);
    }
}
