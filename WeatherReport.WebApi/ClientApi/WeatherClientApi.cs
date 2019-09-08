using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace WeatherReport.WebApi.ClientApi
{

    public interface IWeatherClientApi 
    {
        Task<dynamic> GetWeatherReportBy(string city, string country);
    }
    public sealed class WeatherClientApi : BaseClientApi, IWeatherClientApi
    {
        private readonly IMemoryCache _memoryCacheService;

        public WeatherClientApi(IMemoryCache memoryCacheService) : base(memoryCacheService,null, "https://community-open-weather-map.p.rapidapi.com")
        {
            _memoryCacheService = memoryCacheService;
        }
        public async  Task<dynamic> GetWeatherReportBy(string city, string country)
        {

            RestRequest restRequest= new RestRequest("/weather?q={city},{country}", Method.GET);
            restRequest.AddUrlSegment("city", "London");
            restRequest.AddUrlSegment("country", "UK");
            return await ExecuteAsyncGet(restRequest);
        }
    }

}
