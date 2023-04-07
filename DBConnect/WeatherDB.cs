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
    // Kolekce pro všechna data
    private IMongoCollection<Quantities> _quantities;
    private IMongoCollection<Quantities> _quantitiesAwg;
    private IMongoCollection<Quantities> _historicalquantities;


    public WeatherDB(IOptions<List<SensorConfig>> sensorsConfiguration, IConfiguration config)
    {
        // Konverze _id na string (_id je klíč mongodb databáze.... https://orangematter.solarwinds.com/2019/12/22/what-is-mongodbs-id-field-and-how-to-use-it/)
        var pack = new ConventionPack { new StringIdStoredAsObjectIdConvention() };
        // konverze aby názvy začínaly malím písmenem podle JSON specifikace 
        pack.Add(new IgnoreExtraElementsConvention(true));
        ConventionRegistry.Register("Custom Convention", pack, t => true);
        ConventionRegistry.Register("IgnoreIfNullConvention", new ConventionPack { new IgnoreIfDefaultConvention(true) }, t => true);

        // Připojení k databázi
        var client = new MongoClient(config["DBurl"]);
        var database = client.GetDatabase("MeteorologicalStationValues");

        // kolekce kde jsou všechna data sensorů
        _quantities = database.GetCollection<Quantities>("sensor_data");
        _quantitiesAwg = database.GetCollection<Quantities>("sensor_data_průměry");
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

    // uloží data a v případě že záznam již je, provede přepis, pokud není založí nový
    public async Task SaveSensorsDataAWG(Quantities data)
    {
        await _quantitiesAwg.ReplaceOneAsync(filter: new BsonDocument("SensorFirstTime", data.SensorFirstTime
            ), options: new ReplaceOptions { IsUpsert = true }, replacement: data);
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
        var filter = Builders<Quantities>.Filter.Gt(x => x.SensorFirstTime, from.ToUniversalTime());
        // Filter data do
        filter &= Builders<Quantities>.Filter.Lt(x => x.SensorFirstTime, to.ToUniversalTime());
        // Vrací vyfiltrovaná data všech sensorů
        return await _quantities.Find<Quantities>(filter).ToListAsync();
    }

    public async Task<List<Quantities>> GetSensorsDataAwg(DateTime from, DateTime to)
    {
        // Filter data od
        var filter = Builders<Quantities>.Filter.Gt(x => x.Quantities243.ts, from.ToUniversalTime());
        // Filter data do
        filter &= Builders<Quantities>.Filter.Lt(x => x.Quantities243.ts, to.ToUniversalTime());
        // Vrací vyfiltrovaná data všech sensorů
        return await _quantitiesAwg.Find<Quantities>(filter).ToListAsync();
    }


    /// <summary>
    /// Vrátí minimální a maximální datum 
    /// </summary>
    /// <returns>Mnimální a maximální datum</returns>
    public async Task<(DateTime Min, DateTime Max)> GetMinMaxDate()
    {
        var filter = Builders<Quantities>.Filter.Empty;
        // řazení 
        var sortDescending = Builders<Quantities>.Sort.Descending("SensorFirstTime");
        var sortAscending = Builders<Quantities>.Sort.Ascending("SensorFirstTime");

        var minValue = await _quantities.Find<Quantities>(filter).Sort(sortAscending).Limit(1).FirstOrDefaultAsync();
        var maxValue = await _quantities.Find<Quantities>(filter).Sort(sortDescending).Limit(1).FirstOrDefaultAsync();

        return new(minValue.SensorFirstTime.ToLocalTime().Date, maxValue.SensorFirstTime.ToLocalTime().Date);
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

  
    public async Task<Quantities> GetToDaySensorDataMax(string vel)
    {

        // Filter data od
        var filter = Builders<Quantities>.Filter.Gt(x => x.SensorFirstTime, DateTime.Now.Date.ToUniversalTime());
        // Filter data do
        filter &= Builders<Quantities>.Filter.Lt(x => x.SensorFirstTime, DateTime.Now.ToUniversalTime());

        var Max = Builders<Quantities>.Sort.Descending(vel);
        // Vrací maximální hodnotu podle veličiny
        return await _quantities.Find<Quantities>(filter).Sort(Max).Limit(1).FirstOrDefaultAsync();


    }

    public async Task<Quantities> GetToDaySensorDataMin(string vel)
    {

        // Filter data od
        var filter = Builders<Quantities>.Filter.Gt(x => x.SensorFirstTime, DateTime.Now.Date.ToUniversalTime());
        // Filter data do
        filter &= Builders<Quantities>.Filter.Lt(x => x.SensorFirstTime, DateTime.Now.ToUniversalTime());

        var Min = Builders<Quantities>.Sort.Ascending(vel);
        // Vrací minimální hodnotu podle veličiny
        var document = await _quantities.Find<Quantities>(filter).Sort(Min).Limit(1).FirstOrDefaultAsync();
        return document;

    }


    //TODO: Preprocessing dat. Kvůli tomu aby se nemusela pokaždé tahat všechna data (průměry a tak)

    /*---------------------------------------------------------------*/
    /*---------------------------------------------------------------*/

}


// collection je tabulka in db, a já do ní do dávam data, ona vrací data co chci