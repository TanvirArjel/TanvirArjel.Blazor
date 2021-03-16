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
                PageIndex = 15,
                PageSize = 5,
                TotalItems = 300,
                ListUrl = "products"
            };
        }
    }
}
