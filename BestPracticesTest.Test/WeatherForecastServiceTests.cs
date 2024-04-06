using BestPracticesTest.Data;
using BestPracticesTest.Services;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace BestPracticesTest.Test;

public class WeatherForecastServiceTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer =
        new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("BestPracticesTestDb")
            .WithPassword("changeme")
            .Build();

    private IWeatherForecastRepository? _weatherForecastRepository;

    [Fact]
    public async Task CreateRangeAsync_Returns5IdsOfWeatherForecastsCreatedFromDatabase()
    {
        WeatherForecastService weatherForecastService = new(_weatherForecastRepository!);

        int[] idsFromDatabase = await weatherForecastService.CreateRangeAsync();

        Assert.Equal(5, idsFromDatabase.Length);
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        DbContextOptions options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseNpgsql(_dbContainer.GetConnectionString())
            .Options;

        DatabaseContext context = new(options);
        await context.Database.MigrateAsync();

        _weatherForecastRepository = new WeatherForecastRepository(context);
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}