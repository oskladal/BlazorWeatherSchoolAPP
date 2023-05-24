using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DBConnect;
using DBConnect.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI;

namespace DataProcessing
{
    public class ImportHistoricalDate : CronJobService
    {
        readonly ILogger<ImportHistoricalDate> _logger;
        readonly ConnectAPI _Api;
        readonly WeatherDB _Db;


        public ImportHistoricalDate(IScheduleConfig<ImportHistoricalDate> config, ILogger<ImportHistoricalDate> logger, WeatherDB Db, ConnectAPI Api)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _Api = Api;
            _Db = Db;
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CronJob DataDB Start");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CronJob DataDB Stop");
            return base.StopAsync(cancellationToken);
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

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sace mongoDB at:{time}", DateTimeOffset.Now);

            DateTime currentDate = DateTime.Today.AddDays(-1);
            for (int i = 0; i < 7; i++)
            {
                // začátek období dne
                DateTime startDate = currentDate.Date;
                int unixTimeStart = (int)(startDate.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                // konec období dne
                DateTime endDate = startDate.AddDays(1).AddTicks(-1);
                int unixTimeStop = (int)(endDate.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                // Načtení dat
                var apiData = await _Api.gethistorystation(unixTimeStart, unixTimeStop);

                List<Quantities> HistoryResult = new List<Quantities>(); 

                for (int k = 0; k < apiData.sensors[0].data.Count(); k++)
                {   Quantities OneResult = new Quantities();
                    OneResult.Quantities243 = new Quantities243();
                    OneResult.Quantities243.temp_in = FarenhaiToCelsius(apiData.sensors[0].data[k]["temp_in_last"]);
                    OneResult.Quantities243.hum_in = Dot(apiData.sensors[0].data[k]["hum_in_last"]);
                    OneResult.Quantities243.dew_point_in = FarenhaiToCelsius(apiData.sensors[0].data[k]["dew_point_in"]);
                    OneResult.Quantities243.heat_index_in = FarenhaiToCelsius(apiData.sensors[0].data[k]["heat_index_in"]);
                    OneResult.Quantities243.ts = UnixSecondsToDateTime(long.Parse(apiData.sensors[0].data[k]["ts"]));
                    HistoryResult.Add(OneResult);
                }

                for (int k = 0; k < apiData.sensors[1].data.Count(); k++)
                {
                    HistoryResult[k].Quantities242 = new Quantities242();
                    HistoryResult[k].Quantities242.bar_sea_level = InchToHpa(apiData.sensors[1].data[k]["bar_sea_level"]);
                    HistoryResult[k].Quantities242.ts = UnixSecondsToDateTime(long.Parse(apiData.sensors[1].data[k]["ts"]));
                }

                for (int k = 0; k < apiData.sensors[2].data.Count(); k++)
                {
                    HistoryResult[k].Quantities46 = new Quantities46();
                    HistoryResult[k].Quantities46.temp = FarenhaiToCelsius(apiData.sensors[2].data[k]["temp_last"]);
                    HistoryResult[k].Quantities46.wind_speed_last = MilesToKm(apiData.sensors[2].data[k]["wind_speed_avg"]);
                    HistoryResult[k].Quantities46.wind_speed_avg_last_10_min = MilesToKm(apiData.sensors[2].data[k]["wind_speed_avg"]);
                    HistoryResult[k].Quantities46.wind_dir_last = Dot(apiData.sensors[2].data[k]["wind_dir_of_prevail"]);
                    HistoryResult[k].Quantities46.hum = Dot(apiData.sensors[2].data[k]["hum_last"]);
                    HistoryResult[k].Quantities46.rain_rate_last_mm = MilesToKm(apiData.sensors[2].data[k]["rain_rate_hi_mm"]);
                    HistoryResult[k].Quantities46.uv_index = Dot(apiData.sensors[2].data[k]["uv_index_avg"]);
                    HistoryResult[k].Quantities46.solar_rad = Dot(apiData.sensors[2].data[k]["solar_rad_avg"]);
                    HistoryResult[k].Quantities46.rainfall_last_15_min_mm = Dot(apiData.sensors[2].data[k]["rainfall_mm"]);
                    HistoryResult[k].Quantities46.dew_point = FarenhaiToCelsius(apiData.sensors[2].data[k]["dew_point_last"]);
                    HistoryResult[k].Quantities46.heat_index = FarenhaiToCelsius(apiData.sensors[2].data[k]["heat_index_last"]);
                    HistoryResult[k].Quantities46.wind_chill = FarenhaiToCelsius(apiData.sensors[2].data[k]["wind_chill_last"]);
                    HistoryResult[k].Quantities46.ts = UnixSecondsToDateTime(long.Parse(apiData.sensors[2].data[k]["ts"]));
                }
                Console.WriteLine(i);
                if (i < 9) { Console.WriteLine(apiData.sensors[6].data.Count()); };
                for (int k = 0; k < apiData.sensors[6].data.Count(); k++)
                {
                    if (k > HistoryResult.Count - 1) { break; }
                    HistoryResult[k].Quantities326 = new Quantities326();
                    HistoryResult[k].Quantities326.temp = FarenhaiToCelsius(apiData.sensors[6].data[k]["temp_avg"]);
                    HistoryResult[k].Quantities326.hum = Dot(apiData.sensors[6].data[k]["hum_last"]);
                    HistoryResult[k].Quantities326.dew_point = FarenhaiToCelsius(apiData.sensors[6].data[k]["dew_point_last"]);
                    HistoryResult[k].Quantities326.heat_index = FarenhaiToCelsius(apiData.sensors[6].data[k]["heat_index_last"]);
                    HistoryResult[k].Quantities326.pm_1 = Dot(apiData.sensors[6].data[k]["pm_1_avg"]);
                    HistoryResult[k].Quantities326.pm_2p5 = Dot(apiData.sensors[6].data[k]["pm_2p5_avg"]);
                    HistoryResult[k].Quantities326.pm_10 = Dot(apiData.sensors[6].data[k]["pm_10_avg"]);
                    HistoryResult[k].Quantities326.aqi_type = apiData.sensors[6].data[k]["aqi_type"];
                    HistoryResult[k].Quantities326.aqi_val = Dot(apiData.sensors[6].data[k]["aqi_avg_val"]);
                    HistoryResult[k].Quantities326.aqi_desc = apiData.sensors[6].data[k]["aqi_avg_desc"];
                    HistoryResult[k].Quantities326.ts = UnixSecondsToDateTime(long.Parse(apiData.sensors[6].data[k]["ts"]));
                }

                for (int k = 0; k < apiData.sensors[4].data.Count(); k++)
                {
                    HistoryResult[k].Quantities56 = new Quantities56();
                    HistoryResult[k].Quantities56.moist_soil_1 = Dot(apiData.sensors[4].data[k]["moist_soil_last_1"]);
                    HistoryResult[k].Quantities56.temp_1 = FarenhaiToCelsius(apiData.sensors[4].data[k]["temp_last_1"]);
                    HistoryResult[k].Quantities56.ts = UnixSecondsToDateTime(long.Parse(apiData.sensors[4].data[k]["ts"]));
                }

                /*-----------------------------------------------------*/

                foreach (var history in HistoryResult) {

                    history.Created = DateTime.Now;
                    history.SensorFirstTime = history.Quantities243.ts;
                    try { await _Db.SaveSensorsData(history); } catch { };
                }

                // posunutí aktuálního data o jeden den do minulosti
                currentDate = currentDate.AddDays(-1);
            }

            

        }

    }
}
