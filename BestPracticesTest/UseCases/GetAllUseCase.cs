using BestPracticesTest.Data;
using BestPracticesTest.Entities;

namespace BestPracticesTest.UseCases;

public class GetAllUseCase(IWeatherForecastRepository weatherForecastRepository) : IGetAllUseCase
{
    private readonly IWeatherForecastRepository _weatherForecastRepository = weatherForecastRepository;

    public async Task<IEnumerable<WeatherForecast>> ExecuteAsync()
    {
        IEnumerable<WeatherForecast> weatherForecasts = await _weatherForecastRepository.GetAllAsync();

        return weatherForecasts;
    }
}
