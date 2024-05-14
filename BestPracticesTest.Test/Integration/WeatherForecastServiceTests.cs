using BestPracticesTest.Data;
using BestPracticesTest.Entities;
using BestPracticesTest.Services;
using BestPracticesTest.UseCases;
using Meziantou.Xunit;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace BestPracticesTest.Test.Integration;

[DisableParallelization]
public class WeatherForecastServiceTests : IAsyncLifetime
{
    private IWeatherForecastRepository? _weatherForecastRepository;

    private readonly PostgreSqlContainer _dbContainer =
        new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("BestPracticesTestDb")
            .WithPassword("changeme")
            .Build();

    [Fact]
    public async Task CreateRangeAsync_Returns5IdsOfWeatherForecastsCreatedFromDatabase()
    {
        ICreateRangeUseCase createRangeUseCase = new CreateRangeUseCase(_weatherForecastRepository!);

        int[] idsFromDatabase = await createRangeUseCase.ExecuteAsync();

        Assert.Equal(5, idsFromDatabase.Length);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllCreatedWeatherForecastFromDatabase()
    {
        ICreateRangeUseCase createRangeUseCase = new CreateRangeUseCase(_weatherForecastRepository!);
        int[] idsFromDatabase = await createRangeUseCase.ExecuteAsync();
        IGetAllUseCase getAllUseCase = new GetAllUseCase(_weatherForecastRepository!);

        IEnumerable<WeatherForecast> weatherForecasts = await getAllUseCase.ExecuteAsync();

        Assert.NotEmpty(idsFromDatabase);
        Assert.Equal(idsFromDatabase.Length, weatherForecasts.Count());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmptyListOfWeatherForecastFromDatabase()
    {
        IGetAllUseCase getAllUseCase = new GetAllUseCase(_weatherForecastRepository!);

        IEnumerable<WeatherForecast> weatherForecasts = await getAllUseCase.ExecuteAsync();

        Assert.Empty(weatherForecasts);
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