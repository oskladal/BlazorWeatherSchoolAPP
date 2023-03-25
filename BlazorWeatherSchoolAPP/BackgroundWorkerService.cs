// Kde dochází k zapínání programu - spuštění - přidávání modulů 

using System.Threading;
using BlazorWeatherSchoolAPP;
using WeatherAPI;
using DBConnect;
using WeatherAPI.Models;
using Microsoft.AspNetCore.Components;
using DBConnect.Models;
using System.Globalization;

public class BackgroundWorkerService : BackgroundService
{
    readonly ILogger<BackgroundWorkerService> _logger;
    readonly ConnectAPI _Api;
    readonly WeatherDB _Db;

    public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger, ConnectAPI Api, WeatherDB Db)
    {
        _logger = logger;
        _Api = Api;
        _Db = Db;
    }

    public DateTime UnixSecondsToDateTime(long timestamp)
    {
        var offset = DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime;
        return offset;
    }

    public double FarenhaiToCelsius(string way)
    {
        var newset = 5 * (double.Parse(way, CultureInfo.InvariantCulture) - 32) / 9;
        return Math.Round(newset, 1);
    }

    public double InchToHpa(string way)
    {
        var newset = (double.Parse(way, CultureInfo.InvariantCulture)) * 33.86;
        return Math.Round(newset, 1);
    }

    public double MilesToKm(string way)
    {
        var newset = (double.Parse(way, CultureInfo.InvariantCulture)) * 1.6093427;
        return Math.Round(newset, 1);
    }

    public double Dot(string way)
    {
        var newset = (double.Parse(way, CultureInfo.InvariantCulture));
        return Math.Round(newset, 1);
    }



    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Sace mongoDB at:{time}", DateTimeOffset.Now);

            // Načtení dat
            var apiData = await _Api.getstation();

            Quantities243 newData243 = new Quantities243();
            newData243.temp_in = FarenhaiToCelsius(apiData.sensors[0].data[0]["temp_in"]);
            newData243.hum_in = Dot(apiData.sensors[0].data[0]["hum_in"]);
            newData243.ts = UnixSecondsToDateTime(long.Parse(apiData.sensors[0].data[0]["ts"]));

            Quantities242 newData242 = new Quantities242();
            newData242.bar_sea_level = InchToHpa(apiData.sensors[1].data[0]["bar_sea_level"]);
            newData242.bar_trend = InchToHpa(apiData.sensors[1].data[0]["bar_trend"]);
            newData242.ts = UnixSecondsToDateTime(long.Parse(apiData.sensors[1].data[0]["ts"]));

            Quantities46 newData46 = new Quantities46();
            newData46.temp = FarenhaiToCelsius(apiData.sensors[2].data[0]["temp"]);
            newData46.wind_speed_last = MilesToKm(apiData.sensors[2].data[0]["wind_speed_last"]);
            newData46.wind_speed_avg_last_10_min = MilesToKm(apiData.sensors[2].data[0]["wind_speed_avg_last_10_min"]);
            newData46.wind_dir_last = Dot(apiData.sensors[2].data[0]["wind_dir_last"]);
            newData46.hum = Dot(apiData.sensors[2].data[0]["hum"]);
            newData46.rain_rate_last_mm = MilesToKm(apiData.sensors[2].data[0]["rain_rate_last_mm"]);
            newData46.uv_index = Dot(apiData.sensors[2].data[0]["uv_index"]);
            newData46.solar_rad = Dot(apiData.sensors[2].data[0]["solar_rad"]);
            newData46.rainfall_daily_mm = Dot(apiData.sensors[2].data[0]["rainfall_daily_mm"]);
            newData46.rainfall_monthly_mm = Dot(apiData.sensors[2].data[0]["rainfall_monthly_mm"]);
            newData46.rainfall_year_mm = Dot(apiData.sensors[2].data[0]["rainfall_year_mm"]);
            newData46.dew_point = FarenhaiToCelsius(apiData.sensors[2].data[0]["dew_point"]);
            newData46.heat_index = FarenhaiToCelsius(apiData.sensors[2].data[0]["heat_index"]);
            newData46.wind_chill = FarenhaiToCelsius(apiData.sensors[2].data[0]["wind_chill"]);
            newData46.ts = UnixSecondsToDateTime(long.Parse(apiData.sensors[2].data[0]["ts"]));

            Quantities326 newData326 = new Quantities326();
            newData326.temp = FarenhaiToCelsius(apiData.sensors[6].data[0]["temp"]);
            newData326.hum = Dot(apiData.sensors[6].data[0]["hum"]);
            newData326.dew_point = FarenhaiToCelsius(apiData.sensors[6].data[0]["dew_point"]);
            newData326.heat_index = FarenhaiToCelsius(apiData.sensors[6].data[0]["heat_index"]);
            newData326.pm_1 = Dot(apiData.sensors[6].data[0]["pm_1"]);
            newData326.pm_2p5 = Dot(apiData.sensors[6].data[0]["pm_2p5"]);
            newData326.pm_10 = Dot(apiData.sensors[6].data[0]["pm_10"]);
            newData326.aqi_type = apiData.sensors[6].data[0]["aqi_type"];
            newData326.aqi_val = Dot(apiData.sensors[6].data[0]["aqi_val"]);
            newData326.aqi_desc = apiData.sensors[6].data[0]["aqi_desc"];
            newData326.pm_10_24_hour = Dot(apiData.sensors[6].data[0]["pm_10_24_hour"]);
            newData326.pm_2p5_24_hour = Dot(apiData.sensors[6].data[0]["pm_2p5_24_hour"]);
            newData326.ts = UnixSecondsToDateTime(long.Parse(apiData.sensors[6].data[0]["ts"]));

            Quantities56 newData56 = new Quantities56();
            newData56.moist_soil_1 = Dot(apiData.sensors[4].data[0]["moist_soil_1"]);
            newData56.temp_1 = FarenhaiToCelsius(apiData.sensors[4].data[0]["temp_1"]);
            newData56.ts = UnixSecondsToDateTime(long.Parse(apiData.sensors[4].data[0]["ts"]));

            /*-----------------------------------------------------*/

            // Objekt kde jsou všechna data
            Quantities quantities = new Quantities
            {
                Created = DateTime.Now,
                Quantities242 = newData242,
                Quantities243 = newData243,
                Quantities46 = newData46,
                Quantities56 = newData56,
                Quantities326 = newData326
            };

            // uložení do DB
            await _Db.SaveSensorsData(quantities);

            // Doba po které bude docházet k ukládání
            await Task.Delay(300000, stoppingToken);
        }
    }
}


