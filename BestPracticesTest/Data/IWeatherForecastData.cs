using BestPracticesTest.Entities;

namespace BestPracticesTest.Data;

public interface IWeatherForecastData
{
    Task<int[]> CreateRangeAsync(WeatherForecast[] weatherForecasts);
    Task<IEnumerable<WeatherForecast>> GetAllAsync();
}
