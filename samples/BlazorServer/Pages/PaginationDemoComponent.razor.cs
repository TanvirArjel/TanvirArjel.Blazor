using TanvirArjel.Blazor.Components;

namespace BlazorServer.Pages
{
    public partial class PaginationDemoComponent
    {
        private PaginationModel PaginationModel { get; set; }
        protected override void OnInitialized()
        {
            PaginationModel = new PaginationModel()
            {
                PageIndex = 1,
                TotalPages = 6,
                TotalItems = 30,
                PageItemsEndsAt = 5,
                PageItemsStartsAt = 1,
            };
        }
    }
}
