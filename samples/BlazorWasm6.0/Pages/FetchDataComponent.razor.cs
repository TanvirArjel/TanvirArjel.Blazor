using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TanvirArjel.Blazor.Extensions;

namespace BlazorWasm6._0.Pages
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
            forecasts = await _httpClient.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>()
            {
                ["Test1"] = "Tanvir",
                ["Test2"] = "Ahmad",
                ["Test"] = string.Empty
            };

            _navigationManager.NavigateTo("counter", "Hello", 10);
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
