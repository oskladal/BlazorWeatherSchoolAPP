// Kde dochází k zapínání programu - spuštění - přidávání modulů 

using System.Threading;
using BlazorWeatherSchoolAPP;
using WeatherAPI;
using DBConnect;
using WeatherAPI.Models;
using Microsoft.AspNetCore.Components;

public class BackgroundWorkerService : BackgroundService
{
    readonly ILogger<BackgroundWorkerService> _logger;

    public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger)
    {
        _logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Sace mongoDB at:{time}", DateTimeOffset.Now);


            
            




            await Task.Delay(1000, stoppingToken);
        }
    }
}
