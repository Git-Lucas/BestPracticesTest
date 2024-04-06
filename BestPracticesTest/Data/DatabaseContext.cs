using BestPracticesTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace BestPracticesTest.Data;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
}