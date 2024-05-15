using BestPracticesTest.Data;
using BestPracticesTest.Entities;
using BestPracticesTest.UseCases;

namespace BestPracticesTest.Services;

public class CreateRangeUseCase(IWeatherForecastRepository weatherForecastRepository) : ICreateRangeUseCase
{
    private readonly IWeatherForecastRepository _weatherForecastRepository = weatherForecastRepository;

    public async Task<int[]> ExecuteAsync()
    {
        string[] summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        WeatherForecast[] weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }).ToArray();

        int[] idsFromDatabase = await _weatherForecastRepository.CreateRangeAsync(weatherForecasts);

        return idsFromDatabase;
    }
}
