using System.Collections.Generic;
using TanvirArjel.Blazor.Components;

namespace BlazorWasm.Pages
{
    public partial class PaginationComponent
    {
        private PaginationModel PaginationModel { get; set; }
        protected override void OnInitialized()
        {
            PaginationModel = new PaginationModel()
            {
                PageIndex = 1,
                PageSize = 10,
                TotalItems = 100,
                ListPath = "products",
                QueryStrings = new Dictionary<string, string>()
                {
                    ["name"] = "hello"
                }
            };
        }
    }
}
