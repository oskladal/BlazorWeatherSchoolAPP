using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using DBConnect;
using DBConnect.Models;


namespace DataProcessing
{
    public class AvgMinMaxCronJobWeek : CronJobService
    {
        private readonly ILogger<AvgMinMaxCronJobWeek> _logger;
        readonly WeatherDB _Db;

        public AvgMinMaxCronJobWeek(IScheduleConfig<AvgMinMaxCronJobWeek> config, ILogger<AvgMinMaxCronJobWeek> logger, WeatherDB Db)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _Db = Db;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CronJob AvgMinMaxWeek Start");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            DateTime currentDate = DateTime.Today.AddDays(-1);
            for (int i = 0; i < 7; i++)
            {

                //Definování formátu pro rozsah
                DateTime startDate = currentDate.Date;

                // konec období dne
                DateTime endDate = startDate.AddDays(1).AddTicks(-1);

                // ukládání průměrných dat do lokální proměnné 
                var denidata = await _Db.GetSensorsData(startDate, endDate);



                await _Db.SaveSensorsDataAWG(variableAws(denidata));
                _logger.LogInformation("Sace mongoDB week AWG at:{time}", DateTimeOffset.Now);
                currentDate = currentDate.AddDays(-1); ;
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CronJob week AvgMinMax Stop");
            return base.StopAsync(cancellationToken);
        }

        public Quantities variableAws(List<Quantities> denidata)
        {
            Quantities newdatetable_averages = new Quantities();

            newdatetable_averages.Quantities243 = new Quantities243()
            {
                temp_in = AwgData(denidata.Select(x => x.Quantities243.temp_in).ToList()),
                hum_in = AwgData(denidata.Select(x => x.Quantities243.hum_in).ToList()),
                temp_in_min = denidata.OrderBy(x => x.Quantities243.temp_in).First().Quantities243.temp_in,
                temp_in_max = denidata.OrderByDescending(x => x.Quantities243.temp_in).First().Quantities243.temp_in,
                hum_in_min = denidata.OrderBy(x => x.Quantities243.hum_in).First().Quantities243.hum_in,
                hum_in_max = denidata.OrderByDescending(x => x.Quantities243.hum_in).First().Quantities243.hum_in,
                temp_ts_min = denidata.OrderBy(x => x.Quantities243.temp_in).First().Quantities243.ts,
                temp_ts_max = denidata.OrderByDescending(x => x.Quantities243.temp_in).First().Quantities243.ts,
                hum_ts_min = denidata.OrderBy(x => x.Quantities243.hum_in).First().Quantities243.ts,
                hum_ts_max = denidata.OrderByDescending(x => x.Quantities243.hum_in).First().Quantities243.ts,
                dew_point_in = denidata[denidata.Count - 1].Quantities243.dew_point_in,
                dew_point_in_min = denidata.OrderBy(x => x.Quantities243.dew_point_in).First().Quantities243.dew_point_in,
                dew_point_in_max = denidata.OrderByDescending(x => x.Quantities243.dew_point_in).First().Quantities243.dew_point_in,
                dewpoint_in_ts_min = denidata.OrderBy(x => x.Quantities243.dew_point_in).First().Quantities243.ts,
                dewpoint_in_ts_max = denidata.OrderByDescending(x => x.Quantities243.dew_point_in).First().Quantities243.ts,
                heat_index_in = denidata[denidata.Count - 1].Quantities243.heat_index_in,
                heat_index_in_min = denidata.OrderBy(x => x.Quantities243.heat_index_in).First().Quantities243.heat_index_in,
                heat_index_in_max = denidata.OrderByDescending(x => x.Quantities243.heat_index_in).First().Quantities243.heat_index_in,
                heatindex_in_ts_min = denidata.OrderBy(x => x.Quantities243.heat_index_in).First().Quantities243.ts,
                heatindex_in_ts_max = denidata.OrderByDescending(x => x.Quantities243.heat_index_in).First().Quantities243.ts,
                ts = denidata[denidata.Count - 1].Quantities243.ts.Date

            };

            newdatetable_averages.Quantities242 = new Quantities242()
            {
                bar_sea_level = AwgData(denidata.Select(x => x.Quantities242.bar_sea_level).ToList()),
                bar_sea_level_min = denidata.OrderBy(x => x.Quantities242.bar_sea_level).First().Quantities242.bar_sea_level,
                bar_sea_level_max = denidata.OrderByDescending(x => x.Quantities242.bar_sea_level).First().Quantities242.bar_sea_level,
                barsea_ts_min = denidata.OrderBy(x => x.Quantities242.bar_sea_level).First().Quantities242.ts,
                barsea_ts_max = denidata.OrderByDescending(x => x.Quantities242.bar_sea_level).First().Quantities242.ts,
                ts = denidata[denidata.Count - 1].Quantities242.ts.Date
            };

            newdatetable_averages.Quantities46 = new Quantities46()
            {
                temp = AwgData(denidata.Select(x => x.Quantities46.temp).ToList()),
                temp_min = denidata.OrderBy(x => x.Quantities46.temp).First().Quantities46.temp,
                temp_max = denidata.OrderByDescending(x => x.Quantities46.temp).First().Quantities46.temp,
                temp_ts_min = denidata.OrderBy(x => x.Quantities46.temp).First().Quantities46.ts,
                temp_ts_max = denidata.OrderByDescending(x => x.Quantities46.temp).First().Quantities46.ts,
                wind_speed_avg_last_10_min = AwgData(denidata.Select(x => x.Quantities46.wind_speed_avg_last_10_min).ToList()),
                wind_speed_last_min = denidata.OrderBy(x => x.Quantities46.wind_speed_last).First().Quantities46.wind_speed_last,
                wind_speed_last_max = denidata.OrderByDescending(x => x.Quantities46.wind_speed_last).First().Quantities46.wind_speed_last,
                windspeed_ts_min = denidata.OrderBy(x => x.Quantities46.wind_speed_last).First().Quantities46.ts,
                windspeed_ts_max = denidata.OrderByDescending(x => x.Quantities46.wind_speed_last).First().Quantities46.ts,
                wind_dir_last = AwgData(denidata.Select(x => x.Quantities46.wind_dir_last).ToList()),
                hum = AwgData(denidata.Select(x => x.Quantities46.hum).ToList()),
                hum_min = denidata.OrderBy(x => x.Quantities46.hum).First().Quantities46.hum,
                hum_max = denidata.OrderByDescending(x => x.Quantities46.hum).First().Quantities46.hum,
                hum_ts_min = denidata.OrderBy(x => x.Quantities46.hum).First().Quantities46.ts,
                hum_ts_max = denidata.OrderByDescending(x => x.Quantities46.hum).First().Quantities46.ts,
                rain_rate_last_mm = AwgData(denidata.Select(x => x.Quantities46.rain_rate_last_mm).ToList()),
                rain_rate_last_mm_min = denidata.OrderBy(x => x.Quantities46.rain_rate_last_mm).First().Quantities46.rain_rate_last_mm,
                rain_rate_last_mm_max = denidata.OrderByDescending(x => x.Quantities46.rain_rate_last_mm).First().Quantities46.rain_rate_last_mm,
                rainrate_ts_min = denidata.OrderBy(x => x.Quantities46.rain_rate_last_mm).First().Quantities46.ts,
                rainrate_ts_max = denidata.OrderByDescending(x => x.Quantities46.rain_rate_last_mm).First().Quantities46.ts,
                uv_index = AwgData(denidata.Select(x => x.Quantities46.uv_index).ToList()),
                uv_index_min = denidata.OrderBy(x => x.Quantities46.uv_index).First().Quantities46.uv_index,
                uv_index_max = denidata.OrderByDescending(x => x.Quantities46.uv_index).First().Quantities46.uv_index,
                uvindex_ts_min = denidata.OrderBy(x => x.Quantities46.uv_index).First().Quantities46.ts,
                uvindex_ts_max = denidata.OrderByDescending(x => x.Quantities46.uv_index).First().Quantities46.ts,
                solar_rad = AwgData(denidata.Select(x => x.Quantities46.solar_rad).ToList()),
                solar_rad_min = denidata.OrderBy(x => x.Quantities46.solar_rad).First().Quantities46.solar_rad,
                solar_rad_max = denidata.OrderByDescending(x => x.Quantities46.solar_rad).First().Quantities46.solar_rad,
                solarrad_ts_min = denidata.OrderBy(x => x.Quantities46.solar_rad).First().Quantities46.ts,
                solarrad_ts_max = denidata.OrderByDescending(x => x.Quantities46.solar_rad).First().Quantities46.ts,
                rainfall_last_15_min_mm = AwgData(denidata.Select(x => x.Quantities46.rainfall_last_15_min_mm).ToList()),
                dew_point = denidata[denidata.Count - 1].Quantities46.dew_point,
                dew_point_min = denidata.OrderBy(x => x.Quantities46.dew_point).First().Quantities46.dew_point,
                dew_point_max = denidata.OrderByDescending(x => x.Quantities46.dew_point).First().Quantities46.dew_point,
                dewpoint_ts_min = denidata.OrderBy(x => x.Quantities46.dew_point).First().Quantities46.ts,
                dewpoint_ts_max = denidata.OrderByDescending(x => x.Quantities46.dew_point).First().Quantities46.ts,
                heat_index = denidata[denidata.Count - 1].Quantities46.heat_index,
                heat_index_min = denidata.OrderBy(x => x.Quantities46.heat_index).First().Quantities46.heat_index,
                heat_index_max = denidata.OrderByDescending(x => x.Quantities46.heat_index).First().Quantities46.heat_index,
                heatindex_ts_min = denidata.OrderBy(x => x.Quantities46.heat_index).First().Quantities46.ts,
                heatindex_ts_max = denidata.OrderByDescending(x => x.Quantities46.heat_index).First().Quantities46.ts,
                wind_chill = denidata[denidata.Count - 1].Quantities46.wind_chill,
                wind_chill_min = denidata.OrderBy(x => x.Quantities46.wind_chill).First().Quantities46.wind_chill,
                wind_chill_max = denidata.OrderByDescending(x => x.Quantities46.wind_chill).First().Quantities46.wind_chill,
                windchill_ts_min = denidata.OrderBy(x => x.Quantities46.wind_chill).First().Quantities46.ts,
                windchill_ts_max = denidata.OrderByDescending(x => x.Quantities46.wind_chill).First().Quantities46.ts,
                ts = denidata[denidata.Count - 1].Quantities46.ts.Date

            };

            newdatetable_averages.Quantities326 = new Quantities326
            {

                temp = AwgData(denidata.Select(x => x.Quantities326.temp).ToList()),
                temp_min = denidata.OrderBy(x => x.Quantities326.temp).First().Quantities326.temp,
                temp_max = denidata.OrderByDescending(x => x.Quantities326.temp).First().Quantities326.temp,
                temp_ts_min = denidata.OrderBy(x => x.Quantities326.temp).First().Quantities326.ts,
                temp_ts_max = denidata.OrderByDescending(x => x.Quantities326.temp).First().Quantities326.ts,
                hum = AwgData(denidata.Select(x => x.Quantities326.hum).ToList()),
                hum_min = denidata.OrderBy(x => x.Quantities326.hum).First().Quantities326.hum,
                hum_max = denidata.OrderByDescending(x => x.Quantities326.hum).First().Quantities326.hum,
                hum_ts_min = denidata.OrderBy(x => x.Quantities326.hum).First().Quantities326.ts,
                hum_ts_max = denidata.OrderByDescending(x => x.Quantities326.hum).First().Quantities326.ts,
                dew_point = AwgData(denidata.Select(x => x.Quantities326.dew_point).ToList()),
                dew_point_min = denidata.OrderBy(x => x.Quantities326.dew_point).First().Quantities326.dew_point,
                dew_point_max = denidata.OrderByDescending(x => x.Quantities326.dew_point).First().Quantities326.dew_point,
                dewpoint_ts_min = denidata.OrderBy(x => x.Quantities326.dew_point).First().Quantities326.ts,
                dewpoint_ts_max = denidata.OrderByDescending(x => x.Quantities326.dew_point).First().Quantities326.ts,
                heat_index = AwgData(denidata.Select(x => x.Quantities326.heat_index).ToList()),
                heat_index_min = denidata.OrderBy(x => x.Quantities326.heat_index).First().Quantities326.heat_index,
                heat_index_max = denidata.OrderByDescending(x => x.Quantities326.heat_index).First().Quantities326.heat_index,
                heatindex_ts_min = denidata.OrderBy(x => x.Quantities326.heat_index).First().Quantities326.ts,
                heatindex_ts_max = denidata.OrderByDescending(x => x.Quantities326.heat_index).First().Quantities326.ts,
                pm_1 = AwgData(denidata.Select(x => x.Quantities326.pm_1).ToList()),
                pm_1_min = denidata.OrderBy(x => x.Quantities326.pm_1).First().Quantities326.pm_1,
                pm_1_max = denidata.OrderByDescending(x => x.Quantities326.pm_1).First().Quantities326.pm_1,
                pm1_ts_min = denidata.OrderBy(x => x.Quantities326.pm_1).First().Quantities326.ts,
                pm1_ts_max = denidata.OrderByDescending(x => x.Quantities326.pm_1).First().Quantities326.ts,
                pm_2p5 = AwgData(denidata.Select(x => x.Quantities326.pm_2p5).ToList()),
                pm_2p5_min = denidata.OrderBy(x => x.Quantities326.pm_2p5).First().Quantities326.pm_2p5,
                pm_2p5_max = denidata.OrderByDescending(x => x.Quantities326.pm_2p5).First().Quantities326.pm_2p5,
                pm2p5_ts_min = denidata.OrderBy(x => x.Quantities326.pm_2p5).First().Quantities326.ts,
                pm2p5_ts_max = denidata.OrderByDescending(x => x.Quantities326.pm_2p5).First().Quantities326.ts,
                pm_10 = AwgData(denidata.Select(x => x.Quantities326.pm_10).ToList()),
                pm_10_min = denidata.OrderBy(x => x.Quantities326.pm_10).First().Quantities326.pm_10,
                pm_10_max = denidata.OrderByDescending(x => x.Quantities326.pm_10).First().Quantities326.pm_10,
                pm10_ts_min = denidata.OrderBy(x => x.Quantities326.pm_10).First().Quantities326.ts,
                pm10_ts_max = denidata.OrderByDescending(x => x.Quantities326.pm_10).First().Quantities326.ts,
                aqi_val = AwgData(denidata.Select(x => x.Quantities326.aqi_val).ToList()),
                aqi_val_min = denidata.OrderBy(x => x.Quantities326.aqi_val).First().Quantities326.aqi_val,
                aqi_val_max = denidata.OrderByDescending(x => x.Quantities326.aqi_val).First().Quantities326.aqi_val,
                aqival_ts_min = denidata.OrderBy(x => x.Quantities326.aqi_val).First().Quantities326.ts,
                aqival_ts_max = denidata.OrderByDescending(x => x.Quantities326.aqi_val).First().Quantities326.ts,
                aqi_desc_min = denidata.OrderBy(x => x.Quantities326.aqi_val).First().Quantities326.aqi_desc,
                aqi_desc_max = denidata.OrderByDescending(x => x.Quantities326.aqi_val).First().Quantities326.aqi_desc,
                ts = denidata[denidata.Count - 1].Quantities326.ts.Date

            };
            try
            {
                newdatetable_averages.Quantities56 = new Quantities56
                {

                    moist_soil_1 = AwgData(denidata.Select(x => x.Quantities56.moist_soil_1).ToList()),
                    moist_soil_1_min = denidata.OrderBy(x => x.Quantities56.moist_soil_1).First().Quantities56.moist_soil_1,
                    moist_soil_1_max = denidata.OrderByDescending(x => x.Quantities56.moist_soil_1).First().Quantities56.moist_soil_1,
                    moist_ts_min = denidata.OrderBy(x => x.Quantities56.moist_soil_1).First().Quantities56.ts,
                    moist_ts_max = denidata.OrderByDescending(x => x.Quantities56.moist_soil_1).First().Quantities56.ts,
                    temp_1 = AwgData(denidata.Select(x => x.Quantities56.temp_1).ToList()),
                    temp_1_min = denidata.OrderBy(x => x.Quantities56.temp_1).First().Quantities56.temp_1,
                    temp_1_max = denidata.OrderByDescending(x => x.Quantities56.temp_1).First().Quantities56.temp_1,
                    temp_ts_min = denidata.OrderBy(x => x.Quantities56.temp_1).First().Quantities56.ts,
                    temp_ts_max = denidata.OrderByDescending(x => x.Quantities56.temp_1).First().Quantities56.ts,
                    ts = denidata[denidata.Count - 1].Quantities56.ts.Date
                };
            }
            catch { };


            newdatetable_averages.Created = DateTime.Now;
            newdatetable_averages.SensorFirstTime = denidata[denidata.Count - 1].Quantities243.ts.Date;


            return newdatetable_averages;

        }
        public double AwgData(List<double> way)
        {

            return Math.Round(way.Average(), 1);

        }

    }
}

