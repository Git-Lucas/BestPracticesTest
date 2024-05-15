using BestPracticesTest.Entities;
using BestPracticesTest.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BestPracticesTest.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(int[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromServices] ICreateRangeUseCase createRangeUseCase)
    {
        int[] weatherForecastsIds = await createRangeUseCase.ExecuteAsync();

        return Created(string.Empty, weatherForecastsIds);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync([FromServices] IGetAllUseCase getAllUseCase)
    {
        IEnumerable<WeatherForecast> weatherForecasts = await getAllUseCase.ExecuteAsync();

        return Ok(weatherForecasts);
    }

    [HttpGet("count")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> CountAsync([FromServices] ICountUseCase countUseCase)
    {
        int countWeatherForecasts = await countUseCase.ExecuteAsync();

        return Ok(countWeatherForecasts);
    }
}
