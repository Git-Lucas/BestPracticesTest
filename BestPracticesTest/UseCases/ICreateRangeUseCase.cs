namespace BestPracticesTest.UseCases;

public interface ICreateRangeUseCase
{
    Task<int[]> ExecuteAsync();
}
