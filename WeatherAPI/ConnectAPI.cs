using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using WeatherAPI.Models;
using static System.Net.WebRequestMethods;

namespace WeatherAPI;
public class ConnectAPI
{
    private string _API_Key;
    private string _API_Secret;
    private HttpClient _Client;
    public ConnectAPI(IConfiguration config, HttpClient client)
    {
        _API_Key = config["API_Key"];
        _API_Secret = config["API_Secret"];
        _Client = client;
    }

    public string timeunix()
    {
        long unixTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
        return unixTime.ToString();
    }

    public string getsignature(string time, string? stationid = null)
    {
        string data = "";
        if (stationid == null)
        {
            data = "api-key" + _API_Key + "t" + time;
        }
        else
        {
            data = "api-key" + _API_Key + "station-id" + stationid + "t" + time;
        }
        //vracení podpisu
        var hash = HashHMACHex(_API_Secret, data);
        return BitConverter.ToString(hash).Replace("-", string.Empty);
    }

    public async Task<Sensors_response> getstation()
    {
        var time = timeunix();
        //var urlAPI = $"https://api.weatherlink.com/v2/stations?api-key={_API_Key}&api-signature={getsignature(time)}&t={time}";
        var urlAPI = $"https://api.weatherlink.com/v2/current/124952?api-key={_API_Key}&api-signature={getsignature(time, "124952")}&t={time}";
        var result = await _Client.GetAsync(urlAPI);
        
        string data =  await result.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<Sensors_response>(data);

        return response;
    }

    // Hashování
    private static byte[] HashHMACHex(string secret, string data)
    {
        var message = Encoding.UTF8.GetBytes(data);
        var hash = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        return hash.ComputeHash(message);
    }

    public static byte[] StringToByteArray(string hex)
    {
        return Enumerable.Range(0, hex.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                         .ToArray();
    }
}




/*
$key = 'tl4o4olkrzdznzkmljlrr05ruov4uvcz';
$secret = 'f84ixwqpkdionigkuy1bufubeezehbdd';
$time = time();
$data1= "api-key" .$key. "t" .$time;    
$hash = hash_hmac('sha256',$data1,$secret);   
$url_info = "https://api.weatherlink.com/v2/stations?api-key=$key&t=$time&api-signature=$hash";    

//Hodnoty aktuální ze stanice ID: 15017 
$id_stanice = "15017";
$data2 = "api-key" .$key. "station-id" .$id_stanice. "t" .$time; 
$hash2 = hash_hmac('sha256',$data2,$secret);      
$url_stanice1_data = "https://api.weatherlink.com/v2/current/$id_stanice?api-key=$key&t=$time&api-signature=$hash2";      
$url_senzory = "https://api.weatherlink.com/v2/sensors?api-key=$key&t=$time&api-signature=$hash";
*/