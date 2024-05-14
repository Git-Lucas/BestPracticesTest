using BestPracticesTest.Data;
using BestPracticesTest.Services;
using BestPracticesTest.UseCases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddScoped<IGetAllUseCase, GetAllUseCase>()
    .AddScoped<ICreateRangeUseCase, CreateRangeUseCase>()
    .AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

var app = builder.Build();

using IServiceScope scope = app.Services.CreateScope();
DatabaseContext context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
await context.Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
