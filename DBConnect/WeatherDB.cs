using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using WeatherAPI.Models;

namespace DBConnect;
public class WeatherDB
{
    private IMongoCollection<Sensor> _468230;
    public WeatherDB(IConfiguration config)
    {

        var pack = new ConventionPack { new StringIdStoredAsObjectIdConvention() };
        pack.Add(new IgnoreExtraElementsConvention(true));

        ConventionRegistry.Register("Custom Convention", pack, t => true);
        var client = new MongoClient(config["DBurl"]);
        var database = client.GetDatabase("MeteorologicalStationValues");
        _468230 = database.GetCollection<Sensor>("_468230");


;    }


    public void SaveData_468230(Sensor data)
    {
        _468230.InsertOneAsync(data);
    }

    public async Task<List<Sensor>>GwtFunckingAllShitFrom_468230()
    {
        var filtetr = Builders<Sensor>.Filter.Empty;
        return await _468230.Find<Sensor>(filtetr).ToListAsync();
    }
}


// collection je tabulka in db, a já do ní do dávam data, ona vrací data co chci hodne ezz :D 