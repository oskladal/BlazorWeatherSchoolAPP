using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using WeatherAPI.Models;
using DBConnect.Models;

namespace DBConnect;
public class WeatherDB
{
    // Seznam kolekcí/tabulek 
    private IMongoCollection<Quantities243> _quantities243;
    private IMongoCollection<Quantities242> _quantities242;
    private IMongoCollection<Quantities46> _quantities46;
    private IMongoCollection<Quantities56> _quantities56;
    private IMongoCollection<Quantities326> _quantities326;

    public WeatherDB(IOptions<List<SensorConfig>> sensorsConfiguration, IConfiguration config)
    {
        // Konverze _id na string (_id je klíč mongodb databáze.... https://orangematter.solarwinds.com/2019/12/22/what-is-mongodbs-id-field-and-how-to-use-it/)
        var pack = new ConventionPack { new StringIdStoredAsObjectIdConvention() };
        pack.Add(new IgnoreExtraElementsConvention(true));
        ConventionRegistry.Register("Custom Convention", pack, t => true);

        // Připojení k databázi
        var client = new MongoClient(config["DBurl"]);
        var database = client.GetDatabase("MeteorologicalStationValues");

        

        _quantities243 = database.GetCollection<Quantities243>("sensor_" + 243);
        _quantities242 = database.GetCollection<Quantities242>("sensor_" + 242);
        _quantities46 = database.GetCollection<Quantities46>("sensor_" + 46);
        _quantities56 = database.GetCollection<Quantities56>("sensor_" + 56);
        _quantities326 = database.GetCollection<Quantities326>("sensor_" + 326);

    }


    public async Task SaveNewSensorData243(Quantities243 data)
    {
        await _quantities243.InsertOneAsync(data);
        
    }

    public async Task SaveNewSensorData242(Quantities242 data)
    {
        await _quantities242.InsertOneAsync(data);

    }

    public async Task SaveNewSensorData46(Quantities46 data)
    {
        await _quantities46.InsertOneAsync(data);

    }

    public async Task SaveNewSensorData56(Quantities56 data)
    {
        await _quantities56.InsertOneAsync(data);

    }

    public async Task SaveNewSensorData326(Quantities326 data)
    {
        await _quantities326.InsertOneAsync(data);

    }

    // Načte všechny data sensoru

    public async Task<Quantities243> Get()
    {
        //prázdnej filtr
        var filter = Builders<Quantities243>.Filter.Empty;
        //bere podle podledního id 
        var sort = Builders<Quantities243>.Sort.Descending("_id");
        // dává data
        var data = await _quantities243.Find<Quantities243>(filter).Sort(sort).FirstOrDefaultAsync();

        return data;

    }

    public async Task<List<Quantities243>> GetMore(DateTime zacatek, DateTime konec)
    {
        //prázdnej filtr
        var filter = Builders<Quantities243>.Filter.Gt(x => x.ts, zacatek.ToUniversalTime());
        filter &= Builders<Quantities243>.Filter.Lt(x => x.ts, konec.ToUniversalTime());
        // dává data
        var data = await _quantities243.Find<Quantities243>(filter).ToListAsync();

        return data;

    }

}


// collection je tabulka in db, a já do ní do dávam data, ona vrací data co chci