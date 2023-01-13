using System;
namespace DBConnect.Models
{
    public class Quantities243
    {
        public double temp_in { get; set; }
        public double heat_index_in { get; set; }
        public double dew_point_in { get; set; }
        public double hum_in { get; set; }
        public DateTime ts { get; set; }
    }
    public class Quantities242
    {
        public double bar_absolute { get; set; }
        public double bar_sea_level { get; set; }
        public double bar_trend { get; set; }
        public DateTime ts { get; set; }
    }
    public class Quantities46
    {
        public double temp { get; set; }
        public double wind_speed_last { get; set; }
        public double wind_speed_avg_last_10_min { get; set; }
        public double wind_dir_last { get; set; }
        public double hum { get; set; }
        public double rain_rate_last_mm { get; set; }
        public double uv_index { get; set; }
        public double solar_rad { get; set; }
        public double rainfall_daily_mm { get; set; }
        public double rainfall_monthly_mm { get; set; }
        public double rainfall_year_mm { get; set; }
        public double dew_point { get; set; }
        public double heat_index { get; set; }
        public double wind_chill { get; set; }
        public DateTime ts { get; set; }
    }
    public class Quantities326
    {
        public double temp { get; set; }
        public double hum { get; set; }
        public double dew_point { get; set; }
        public double heat_index { get; set; }
        public double pm_1 { get; set; }
        public double pm_2p5 { get; set; }
        public double pm_10 { get; set; }
        public string aqi_type { get; set; }
        public double aqi_val { get; set; }
        public string aqi_desc { get; set; }
        public DateTime ts { get; set; }
    }
    public class Quantities56
    {
        public double moist_soil_1 { get; set; }
        public double tx_id { get; set; }
        public double temp_1 { get; set; }
        public DateTime ts { get; set; }
    }
}

