using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using WeatherAPI.Models;
using DBConnect.Models;
using System.Reflection;
using MongoDB.Bson;

namespace DBConnect;

public class CollectionBuilder<T> where T : class
{
    public IMongoCollection<T> Get(IMongoDatabase database, string collectionName)
    {
        return database.GetCollection<T>(collectionName);
    }
}

public class WeatherDB
{
    // Seznam kolekcí/tabulek 
    /*private IMongoCollection<Quantities243> _quantities243;
    private IMongoCollection<Quantities242> _quantities242;
    private IMongoCollection<Quantities46> _quantities46;
    private IMongoCollection<Quantities56> _quantities56;
    private IMongoCollection<Quantities326> _quantities326;*/

    // Kolekce pro všechna data
    private IMongoCollection<Quantities> _quantities;

    public WeatherDB(IOptions<List<SensorConfig>> sensorsConfiguration, IConfiguration config)
    {
        // Konverze _id na string (_id je klíč mongodb databáze.... https://orangematter.solarwinds.com/2019/12/22/what-is-mongodbs-id-field-and-how-to-use-it/)
        var pack = new ConventionPack { new StringIdStoredAsObjectIdConvention() };
        // konverze aby názvy začínaly malím písmenem podle JSON specifikace 
        pack.Add(new IgnoreExtraElementsConvention(true));
        ConventionRegistry.Register("Custom Convention", pack, t => true);

        // Připojení k databázi
        var client = new MongoClient(config["DBurl"]);
        var database = client.GetDatabase("MeteorologicalStationValues");

        // kolekce kde jsou všechna data sensorů
        _quantities = database.GetCollection<Quantities>("sensor_data");
    }

    /*---------------------------------------------------------------*/
    /*---------------------------------------------------------------*/

    /// <summary>
    /// Ukládá data všech sensorů
    /// </summary>
    /// <param name="data">Data sensorů</param>
    /// <returns>void</returns>
    public async Task SaveSensorsData(Quantities data)
    {
        await _quantities.InsertOneAsync(data);
    }

    /// <summary>
    /// Vrací data ze všech sensorů podle data
    /// </summary>
    /// <param name="from">Datum od</param>
    /// <param name="to">Datum do</param>
    /// <returns>Vyfiltrovaná data</returns>
    public async Task<List<Quantities>> GetSensorsData(DateTime from, DateTime to)
    {
        // Filter data od
        var filter = Builders<Quantities>.Filter.Gt(x => x.Created, from.ToUniversalTime());
        // Filter data do
        filter &= Builders<Quantities>.Filter.Lt(x => x.Created, to.ToUniversalTime());
        // Vrací vyfiltrovaná data všech sensorů
        return await _quantities.Find<Quantities>(filter).ToListAsync();
    }

    /// <summary>
    /// Vrátí minimální a maximální datum 
    /// </summary>
    /// <returns>Mnimální a maximální datum</returns>
    public async Task<(DateTime Min, DateTime Max)> GetMinMaxDate()
    {
        var filter = Builders<Quantities>.Filter.Empty;
        // řazení 
        var sortDescending = Builders<Quantities>.Sort.Descending("_id");
        var sortAscending = Builders<Quantities>.Sort.Ascending("_id");

        var minValue = await _quantities.Find<Quantities>(filter).Sort(sortAscending).Limit(1).FirstOrDefaultAsync();
        var maxValue = await _quantities.Find<Quantities>(filter).Sort(sortDescending).Limit(1).FirstOrDefaultAsync();

        return new(minValue.Created.ToLocalTime().Date, maxValue.Created.ToLocalTime().Date);
    }

    /// <summary>
    /// Vrací poslední záznam
    /// </summary>
    /// <returns>Poslední záznam</returns>
    public async Task<Quantities> GetLastSensorData()
    {
        var filter = Builders<Quantities>.Filter.Empty;

        var sortDescending = Builders<Quantities>.Sort.Descending("_id");
        return await _quantities.Find<Quantities>(filter).Sort(sortDescending).Limit(1).FirstOrDefaultAsync();
    }

    //TODO: Preprocessing dat. Kvůli tomu aby se nemusela pokaždé tahat všechna data (průměry a tak)

    /*---------------------------------------------------------------*/
    /*---------------------------------------------------------------*/

}


// collection je tabulka in db, a já do ní do dávam data, ona vrací data co chci