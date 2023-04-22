using DataProcessing;
using DBConnect;
using WeatherAPI;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Builder.Configuration - obsahuji proměnný který jsem si definovali
builder.Services.AddOptions();

builder.Services.AddHttpClient();

//Vytvoření instanci connectAPI a registroval ji do služeb, kde je možné ji vytáhnout v kódu.
builder.Services.AddSingleton<ConnectAPI>();
builder.Services.AddSingleton<WeatherDB>();


builder.Services.AddCronJob<AvgMinMaxCronHour>(c =>
{
    c.TimeZoneInfo = TimeZoneInfo.Local;
    c.CronExpression = @"*/6 * * * *";
});

builder.Services.AddCronJob<AvgMinMaxCronJobWeek>(c =>
{
    c.TimeZoneInfo = TimeZoneInfo.Local;
    c.CronExpression = @"15 0 * * *";
});

builder.Services.AddCronJob<DataDBCronJob>(c =>
{
    c.TimeZoneInfo = TimeZoneInfo.Local;
    c.CronExpression = @"*/5 * * * *";
});

builder.Services.AddCronJob<ImportHistoricalDate>(c =>
{
    c.TimeZoneInfo = TimeZoneInfo.Local;
    c.CronExpression = @"10 0 * * *";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

