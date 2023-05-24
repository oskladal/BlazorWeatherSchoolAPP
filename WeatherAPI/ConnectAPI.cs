using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WeatherAPI.Models;
using static System.Net.WebRequestMethods;

namespace WeatherAPI;
public class ConnectAPI
{
    private string? _API_Key;
    private string? _API_Secret;
    private HttpClient _Client;

    public ConnectAPI(IConfiguration config, HttpClient client)
    {
        _API_Key = config["API_Key"];
        _API_Secret = config["API_Secret"];
        _Client = client;
    }


    public string getsignature(string time, string? stationid = null, int? start=0, int? stop=0)
    {
        string data = "";
        if (stationid == null)
        {
            data = "api-key" + _API_Key + "t" + time;
        }
        else
        {
            if (start != 0)
            {
                data = "api-key" + _API_Key + "end-timestamp" + stop + "start-timestamp" + start + "station-id" + stationid + "t" + time;
            }
            else {
                data = "api-key" + _API_Key + "station-id" + stationid + "t" + time;
            }
            
        }
        //vracení podpisu
        var hash = HashHMACHex(_API_Secret, data);
        return BitConverter.ToString(hash).Replace("-", string.Empty);
    }


    public async Task<Sensors_response?> getstation()
    {
        var time = GetUnixTimestamp();
        var urlAPI = $"https://api.weatherlink.com/v2/current/124952?api-key={_API_Key}&api-signature={getsignature(time, "124952")}&t={time}";
        var result = await _Client.GetAsync(urlAPI);

        string data = await result.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Sensors_response>(data);

        return response;
    }

    public async Task<Sensors_response?> gethistorystation(int start, int stop)
    {
        var time = GetUnixTimestamp();
        var urlAPI = $"https://api.weatherlink.com/v2/historic/124952?api-key={_API_Key}&api-signature={getsignature(time, "124952", start, stop)}&t={time}&start-timestamp={start}&end-timestamp={stop}";
        var result = await _Client.GetAsync(urlAPI);

        string data = await result.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Sensors_response>(data);

        return response;
    }

    // Hashování
    private static byte[] HashHMACHex(string secret, string data)
    {
        var message = Encoding.UTF8.GetBytes(data);
        var hash = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        return hash.ComputeHash(message);
    }

    public string GetUnixTimestamp()
    {
        long unixTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
        return unixTime.ToString();
    }

}

