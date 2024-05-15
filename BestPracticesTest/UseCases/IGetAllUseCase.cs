using BestPracticesTest.Entities;

namespace BestPracticesTest.UseCases;

public interface IGetAllUseCase
{
    Task<IEnumerable<WeatherForecast>> ExecuteAsync();
}
