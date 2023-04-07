using System;
namespace DBConnect.Models
{
    public class Quantities243 : IQuantities
    {
        public double temp_in { get; set; }
        public double temp_in_min { get; set; }
        public double temp_in_max { get; set; }
        public DateTime temp_ts_min { get; set; }
        public DateTime temp_ts_max { get; set; }
        public double hum_in { get; set; }
        public double hum_in_min { get; set; }
        public double hum_in_max { get; set; }
        public DateTime hum_ts_min { get; set; }
        public DateTime hum_ts_max { get; set; }
        public double dew_point_in { get; set; }
        public double dew_point_in_min { get; set; }
        public double dew_point_in_max { get; set; }
        public DateTime dewpoint_in_ts_min { get; set; }
        public DateTime dewpoint_in_ts_max { get; set; }
        public double heat_index_in { get; set; }
        public double heat_index_in_min { get; set; }
        public double heat_index_in_max { get; set; }
        public DateTime heatindex_in_ts_min { get; set; }
        public DateTime heatindex_in_ts_max { get; set; }
        public DateTime ts { get; set; }
    }
    public class Quantities242 : IQuantities
    {
        public double bar_sea_level_min { get; set; }
        public double bar_sea_level_max { get; set; }
        public double bar_sea_level { get; set; }
        public DateTime barsea_ts_min { get; set; }
        public DateTime barsea_ts_max { get; set; }
        public double bar_trend { get; set; }
        public DateTime ts { get; set; }
    }
    public class Quantities46 : IQuantities
    {
        public double temp { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public DateTime temp_ts_min { get; set; }
        public DateTime temp_ts_max { get; set; }
        public double wind_speed_last { get; set; }
        public double wind_speed_last_min { get; set; }
        public double wind_speed_last_max { get; set; }
        public DateTime windspeed_ts_min { get; set; }
        public DateTime windspeed_ts_max { get; set; }
        public double wind_speed_avg_last_10_min { get; set; }
        public double wind_dir_last { get; set; }
        public double hum { get; set; }
        public double hum_min { get; set; }
        public double hum_max { get; set; }
        public DateTime hum_ts_min { get; set; }
        public DateTime hum_ts_max { get; set; }
        public double rain_rate_last_mm { get; set; }
        public double rain_rate_last_mm_min { get; set; }
        public double rain_rate_last_mm_max { get; set; }
        public DateTime rainrate_ts_min { get; set; }
        public DateTime rainrate_ts_max { get; set; }
        public double uv_index { get; set; }
        public double uv_index_min { get; set; }
        public double uv_index_max { get; set; }
        public DateTime uvindex_ts_min { get; set; }
        public DateTime uvindex_ts_max { get; set; }
        public double solar_rad { get; set; }
        public double solar_rad_min { get; set; }
        public double solar_rad_max { get; set; }
        public DateTime solarrad_ts_min { get; set; }
        public DateTime solarrad_ts_max { get; set; }
        public double rainfall_last_15_min_mm { get; set; }
        public double rainfall_last_60_min_mm { get; set; }
        public double rainfall_last_24_hr_mm { get; set; }
        public double rainfall_daily_mm { get; set; }
        public double rainfall_monthly_mm { get; set; }
        public double rainfall_year_mm { get; set; }
        public double dew_point { get; set; }
        public double dew_point_min { get; set; }
        public double dew_point_max { get; set; }
        public DateTime dewpoint_ts_min { get; set; }
        public DateTime dewpoint_ts_max { get; set; }
        public double heat_index { get; set; }
        public double heat_index_min { get; set; }
        public double heat_index_max { get; set; }
        public DateTime heatindex_ts_min { get; set; }
        public DateTime heatindex_ts_max { get; set; }
        public double wind_chill { get; set; }
        public double wind_chill_min { get; set; }
        public double wind_chill_max { get; set; }
        public DateTime windchill_ts_min { get; set; }
        public DateTime windchill_ts_max { get; set; }
        public DateTime ts { get; set; }
    }
    public class Quantities326 : IQuantities
    {
        public double temp { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public DateTime temp_ts_min { get; set; }
        public DateTime temp_ts_max { get; set; }
        public double hum { get; set; }
        public double hum_min { get; set; }
        public double hum_max { get; set; }
        public DateTime hum_ts_min { get; set; }
        public DateTime hum_ts_max { get; set; }
        public double dew_point { get; set; }
        public double dew_point_min { get; set; }
        public double dew_point_max { get; set; }
        public DateTime dewpoint_ts_min { get; set; }
        public DateTime dewpoint_ts_max { get; set; }
        public double heat_index { get; set; }
        public double heat_index_min { get; set; }
        public double heat_index_max { get; set; }
        public DateTime heatindex_ts_min { get; set; }
        public DateTime heatindex_ts_max { get; set; }
        public double pm_1 { get; set; }
        public double pm_1_min { get; set; }
        public double pm_1_max { get; set; }
        public DateTime pm1_ts_min { get; set; }
        public DateTime pm1_ts_max { get; set; }
        public double pm_2p5 { get; set; }
        public double pm_2p5_min { get; set; }
        public double pm_2p5_max { get; set; }
        public DateTime pm2p5_ts_min { get; set; }
        public DateTime pm2p5_ts_max { get; set; }
        public double pm_10 { get; set; }
        public double pm_10_min { get; set; }
        public double pm_10_max { get; set; }
        public DateTime pm10_ts_min { get; set; }
        public DateTime pm10_ts_max { get; set; }
        public double pm_10_24_hour { get; set; }
        public double pm_2p5_24_hour { get; set; }
        public double pm_10_1_hour { get; set; }
        public double pm_2p5_1_hour { get; set; }
        public string? aqi_type { get; set; }
        public double aqi_val { get; set; }
        public double aqi_val_min { get; set; }
        public double aqi_val_max { get; set; }
        public DateTime aqival_ts_min { get; set; }
        public DateTime aqival_ts_max { get; set; }
        public string? aqi_desc { get; set; }
        public string? aqi_desc_min { get; set; }
        public string? aqi_desc_max { get; set; }
        public DateTime ts { get; set; }
    }
    public class Quantities56 : IQuantities
    {
        public double moist_soil_1 { get; set; }
        public double moist_soil_1_min { get; set; }
        public double moist_soil_1_max { get; set; }
        public DateTime moist_ts_min { get; set; }
        public DateTime moist_ts_max { get; set; }
        public double temp_1 { get; set; }
        public double temp_1_min { get; set; }
        public double temp_1_max { get; set; }
        public DateTime temp_ts_min { get; set; }
        public DateTime temp_ts_max { get; set; }
        public DateTime ts { get; set; }
    }

    public class Quantities
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime SensorFirstTime { get; set; }
        public Quantities243 Quantities243 { get; set; }
        public Quantities242 Quantities242 { get; set; }
        public Quantities46 Quantities46 { get; set; }
        public Quantities326 Quantities326 { get; set; }
        public Quantities56 Quantities56 { get; set; }
    }

    public interface IQuantities
    {

    }
}

