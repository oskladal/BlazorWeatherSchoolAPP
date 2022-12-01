using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using WeatherAPI.Models;

namespace DBConnect;
public class WeatherDB
{
    // Seznam kolekcí/tabulek podle nastavení v appsettings.json 
    private Dictionary<string, IMongoCollection<Sensor>> _collections;

    public WeatherDB(IOptions<List<SensorConfig>> sensorsConfiguration, IConfiguration config)
    {
        // Konverze _id na string (_id je klíč mongodb databáze.... https://orangematter.solarwinds.com/2019/12/22/what-is-mongodbs-id-field-and-how-to-use-it/)
        var pack = new ConventionPack { new StringIdStoredAsObjectIdConvention() };
        pack.Add(new IgnoreExtraElementsConvention(true));
        ConventionRegistry.Register("Custom Convention", pack, t => true);

        // Připojení k databázi
        var client = new MongoClient(config["DBurl"]);
        var database = client.GetDatabase("MeteorologicalStationValues");

        // Seznam kolekcí/tabulek podle nastavení v appsettings.json 
        _collections = new Dictionary<string, IMongoCollection<Sensor>>();
        if (sensorsConfiguration.Value != null)
        {
            foreach (var sensor in sensorsConfiguration.Value)
            {
                _collections.Add(sensor.sensor_type, database.GetCollection<Sensor>("sensor_" + sensor.sensor_type));
            }
        }
    }

    // Ukládá do databáze data ze sensoru
    public async Task SaveSensorData(Sensor data)
    {
        IMongoCollection<Sensor>? collection;
        _collections.TryGetValue(data.sensor_type.ToString(), out collection);
        if (collection != null)
        {
            await collection.InsertOneAsync(data);
        }
    }

    // Načte všechny data sensoru
    public async Task<List<Sensor>?> GetSensorData(string sensor_type)
    {
        IMongoCollection<Sensor>? collection;
        _collections.TryGetValue(sensor_type, out collection);
        if (collection != null)
        {
            var filter = Builders<Sensor>.Filter.Empty;
            return await collection.Find<Sensor>(filter).ToListAsync();
        }
        else
        {
            return null;
        }
    }
}


// collection je tabulka in db, a já do ní do dávam data, ona vrací data co chci hodne ezz :D 