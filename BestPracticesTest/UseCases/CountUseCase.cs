using BestPracticesTest.Data;

namespace BestPracticesTest.UseCases;

public class CountUseCase(IWeatherForecastRepository weatherForecastRepository) : ICountUseCase
{
    private readonly IWeatherForecastRepository _weatherForecastRepository = weatherForecastRepository;

    public async Task<int> ExecuteAsync()
    {
        int countDatabase = await _weatherForecastRepository.CountAsync();

        return countDatabase;
    }
}
