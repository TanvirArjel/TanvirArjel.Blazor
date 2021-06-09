using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorServer.Models;
using BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using TanvirArjel.Blazor.Extensions;

namespace BlazorServer.Pages
{
    public partial class FetchDataComponent
    {
        private readonly WeatherForecastService _weatherForecastService;
        private readonly NavigationManager _navigationManager;

        public FetchDataComponent(WeatherForecastService weatherForecastService, NavigationManager navigationManager)
        {
            _weatherForecastService = weatherForecastService;
            _navigationManager = navigationManager;
        }

        private WeatherForecast[] Forecasts { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>()
            {
                ["Test1"] = "Tanvir",
                ["Test2"] = "Ahmad",
                ["Test"] = null
            };

            _navigationManager.NavigateTo("counter", keyValuePairs);
            Forecasts = await _weatherForecastService.GetForecastAsync(DateTime.Now);
        }
    }
}
