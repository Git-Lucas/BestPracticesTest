using BestPracticesTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace BestPracticesTest.Data;

public class WeatherForecastData(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;

    public async Task<int[]> CreateRangeAsync(WeatherForecast[] weatherForecasts)
    {
        await _context.WeatherForecasts.AddRangeAsync(weatherForecasts);
        await _context.SaveChangesAsync();

        return weatherForecasts
            .Select(x => x.Id)
            .ToArray();
    }

    public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
    {
        return await _context.WeatherForecasts
            .ToArrayAsync();
    }
}
