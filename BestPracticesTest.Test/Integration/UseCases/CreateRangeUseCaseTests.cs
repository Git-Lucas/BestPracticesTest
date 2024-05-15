using BestPracticesTest.Data;
using BestPracticesTest.Services;
using BestPracticesTest.UseCases;
using Meziantou.Xunit;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace BestPracticesTest.Test.Integration.UseCases;

[DisableParallelization]
public class CreateRangeUseCaseTests : IAsyncLifetime
{
    private IWeatherForecastRepository? _weatherForecastRepository;

    private readonly PostgreSqlContainer _dbContainer =
        new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("BestPracticesTestDb")
            .WithPassword("changeme")
            .Build();

    [Fact]
    public async Task ExecuteAsync_Returns5IdsOfWeatherForecastsCreatedFromDatabase()
    {
        ICreateRangeUseCase createRangeUseCase = new CreateRangeUseCase(_weatherForecastRepository!);

        int[] idsFromDatabase = await createRangeUseCase.ExecuteAsync();

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
