using BestPracticesTest.Entities;

namespace BestPracticesTest.Data;

public interface IWeatherForecastRepository
{
    Task<int[]> CreateRangeAsync(WeatherForecast[] weatherForecasts);
    Task<IEnumerable<WeatherForecast>> GetAllAsync();
}
