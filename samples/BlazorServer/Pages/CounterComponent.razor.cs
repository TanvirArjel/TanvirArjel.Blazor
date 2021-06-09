using Microsoft.AspNetCore.Components;
using TanvirArjel.Blazor.Extensions;

namespace BlazorServer.Pages
{
    public partial class CounterComponent
    {
        private readonly NavigationManager _navigationManager;

        public CounterComponent(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        private int currentCount = 0;

        private string QueryValue { get; set; }

        private void IncrementCount()
        {
            currentCount++;
        }

        private void UpdateQuery()
        {
            if (currentCount == 4)
            {
                _navigationManager.SetQuery<string>("Test0", null);
            }
            else
            {

                _navigationManager.SetQuery($"Test{currentCount}", $"Tanvir{currentCount}");
                currentCount++;
            }
        }

        private void GetQuery()
        {
            QueryValue = _navigationManager.GetQuery<string>($"Test{--currentCount}");
        }
    }
}
