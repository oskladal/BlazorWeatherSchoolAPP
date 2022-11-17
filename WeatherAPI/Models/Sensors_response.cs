using System;
namespace WeatherAPI.Models
{
    public class Sensors_response
    {
        public List<Sensor> sensors { get; set; }
    }

    public class Sensor
    {
        public string? Id { get; set; }
        public long lsid { get; set; }
        public List<Dictionary<string, string>> data { get; set; }
        public int sensor_type { get; set; }
        public int data_structure_type { get; set; }
    }
}

