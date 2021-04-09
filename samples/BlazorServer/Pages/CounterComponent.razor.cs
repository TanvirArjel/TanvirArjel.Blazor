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

        protected override void OnInitialized()
        {
            string v = _navigationManager.GetQuery<string>("Hello");
        }

        private void IncrementCount()
        {
            currentCount++;
        }

        private void UpdateQuery()
        {
            if (currentCount == 4)
            {
                _navigationManager.SetQuery<string>("Test", null);
            }
            else
            {
                _navigationManager.SetQuery("Test", $"Tanvir{currentCount++}");
            }
        }
    }
}
