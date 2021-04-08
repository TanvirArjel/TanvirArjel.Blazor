using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TanvirArjel.Blazor.Extensions;

namespace BlazorWasm.Pages
{
    public partial class FetchDataComponent
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public FetchDataComponent(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        private WeatherForecast[] forecasts;

        protected override async Task OnInitializedAsync()
        {
            _navigationManager.SetQuery("test", 10);
            forecasts = await _httpClient.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
        }

        public class WeatherForecast
        {
            public DateTime Date { get; set; }

            public int TemperatureC { get; set; }

            public string Summary { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}
