using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using WeatherReport.WebApi.ClientApi;

namespace WeatherReport.WebApi.Controllers
{
    [Route("Weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IWeatherClientApi _weatherClientApi;
        private readonly ILogger _logger;

        public WeatherController(IMemoryCache memoryCache,IWeatherClientApi weatherClientApi, ILogger logger)
        {
            _memoryCache = memoryCache;
            _weatherClientApi = weatherClientApi;
            _logger = logger;
        }

        [HttpGet]
        [Route("Report/{city},{country}")]
        public  async Task<dynamic> GetWeatherReport(string city,string country)
        {
            _logger.LogDebug($"The request parameters are ");
           var result=await  _weatherClientApi.GetWeatherReportBy(city,country);

            return JsonConvert.DeserializeObject<dynamic>(result.Content);
        }

    

       
    }
}