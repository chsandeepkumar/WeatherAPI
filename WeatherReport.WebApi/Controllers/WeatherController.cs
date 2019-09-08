using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using WeatherReport.WebApi.ClientApi;

namespace WeatherReport.WebApi.Controllers
{
    [Route("Weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IWeatherClientApi _weatherClientApi;

        public WeatherController(IMemoryCache memoryCache,IWeatherClientApi weatherClientApi)
        {
            _memoryCache = memoryCache;
            _weatherClientApi = weatherClientApi;
        }

        [HttpGet]
        [Route("Report/{city},{country}")]
        public  async Task<dynamic> GetWeatherReport(string city,string country)
        {

           var result=await  _weatherClientApi.GetWeatherReportBy(city,country);

            return JsonConvert.DeserializeObject<dynamic>(result.Content);
        }

    

       
    }
}