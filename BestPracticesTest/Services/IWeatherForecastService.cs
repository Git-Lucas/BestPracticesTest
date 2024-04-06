using BestPracticesTest.Entities;

namespace BestPracticesTest.Services;

public interface IWeatherForecastService
{
    Task<int[]> CreateRangeAsync();
    Task<IEnumerable<WeatherForecast>> GetAllAsync();
}
