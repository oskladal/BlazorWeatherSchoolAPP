using System;
namespace WeatherAPI.Models
{
    public class Sensors_response
    {
        public List<Sensor_data> sensors { get; set; }

    }

    public class Sensor
    {
        public string? sensor_type { get; set; }
        public Dictionary<string, string> data { get; set; }
        public static explicit operator Sensor(Sensor_data s) { return new Sensor { sensor_type = s.sensor_type, data = s.data[0] }; }
    }
    public class Sensor_data
    {
        public string? sensor_type { get; set; }
        public List<Dictionary<string, string>> data { get; set; }
    }
}

