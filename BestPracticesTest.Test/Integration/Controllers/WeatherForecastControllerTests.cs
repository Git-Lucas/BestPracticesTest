using Meziantou.Xunit;
using NBomber.Contracts;
using NBomber.CSharp;

namespace BestPracticesTest.Test.Integration.Controllers;

[DisableParallelization]
public class WeatherForecastControllerTests
{
    private readonly Uri _baseAddress = new("https://localhost:7048/");

    [Theory]
    [InlineData(850, 1, 10)]
    public async Task CreateAsync_WhenManyRequestsToCreate5WeatherForecasts_ReturnsCountOfAllCreatedFromDatabase(int rate, int secondsInterval, int secondsDuring)
    {
        using HttpClient client = new() { BaseAddress = _baseAddress};
        int previousCountWeatherForecasts = await CountDatabase(client);
        ScenarioProps scenario = Scenario.Create("Teste de Carga", async context =>
        {
            HttpResponseMessage response = await client.PostAsync(requestUri: "weatherForecast", content: null);

            return response.IsSuccessStatusCode
                ? Response.Ok()
                : Response.Fail();
        }).WithoutWarmUp()
          .WithLoadSimulations(Simulation.Inject(rate,
                                                              interval: TimeSpan.FromSeconds(secondsInterval),
                                                              during: TimeSpan.FromSeconds(secondsDuring)));

        NBomberRunner
            .RegisterScenarios(scenario)
            .Run();

        int expectedCount = rate * secondsInterval * secondsDuring * 5;
        int countNewWeatherForecasts = await CountDatabase(client) - previousCountWeatherForecasts;
        Assert.Equal(expectedCount, countNewWeatherForecasts);
    }

    private async Task<int> CountDatabase(HttpClient client)
    {
        HttpResponseMessage response = await client.GetAsync(requestUri: "weatherForecast/count");
        string jsonContent = await response.Content.ReadAsStringAsync();
        return Convert.ToInt32(jsonContent);
    }
}
