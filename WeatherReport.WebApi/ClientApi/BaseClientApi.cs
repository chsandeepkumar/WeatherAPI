using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace WeatherReport.WebApi.ClientApi
{
    public  class BaseClientApi : RestClient
    {
        private IMemoryCache _cache;
        public BaseClientApi(IMemoryCache cache, IDeserializer serializer, string baseUrl)
        {
            _cache = cache;
            AddHandler("application/json", serializer);
            AddHandler("text/json", serializer);
            AddHandler("text/x-json", serializer);
            // ReSharper disable once VirtualMemberCallInConstructor
            BaseUrl = new Uri(baseUrl);
        }

        public async Task<IRestResponse> ExecuteAsyncGet(IRestRequest restRequest)
        {
            restRequest.AddHeader("x-rapidapi-host", "community-open-weather-map.p.rapidapi.com");
            restRequest.AddHeader("x-rapidapi-key", "9f138f2a30msha852af877a9b353p11b298jsna102951728b6");

            var taskCompletion = new TaskCompletionSource<IRestResponse>();

            base.ExecuteAsync(restRequest,(restResponse, handle) =>taskCompletion.SetResult(restResponse) );
            var res = (RestResponse)await taskCompletion.Task;

            return res;
        }


    }
}
