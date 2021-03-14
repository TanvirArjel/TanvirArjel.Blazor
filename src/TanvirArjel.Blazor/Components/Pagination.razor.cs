using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace TanvirArjel.Blazor.Components
{
    /// <summary>
    /// The pagination component which will render the paginaton in html.
    /// </summary>
    public partial class Pagination
    {
        /// <summary>
        /// The pagination object that contains all the information related to the pagination.
        /// </summary>
        [Parameter]
        public PaginationModel PaginationModel { get; set; }

        /// <summary>
        /// The item list url.
        /// </summary>
        [Parameter]
        public string ListUrl { get; set; }

        /// <summary>
        /// The query strings of the item list if any. This is can be used to retain the search or order during pagination.
        /// </summary>
        [Parameter]
        public Dictionary<string, string> QueryStrings { get; set; }

        /// <summary>
        /// Whether the pagination details will be shown. Default value is true.
        /// </summary>
        [Parameter]
        public bool ShowDetails { get; set; }

        private string PageLink { get; set; }

        private string PrevDisabled => PaginationModel.HasPreviousPage ? string.Empty : "disabled";

        private string NextDisabled => PaginationModel.HasNextPage ? string.Empty : "disabled";

        /// <summary>
        /// This method will be called during initializaiton of the component.
        /// </summary>
        protected override void OnInitialized()
        {
            if (QueryStrings != null && QueryStrings.Any())
            {
                string uriWithQueryString = QueryHelpers.AddQueryString(ListUrl, QueryStrings);
                PageLink = uriWithQueryString + "&pageIndex=";
            }
            else
            {
                PageLink = ListUrl + "?pageIndex=";
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PaginationModel
    {
        /// <summary>
        /// The index of the current page.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Total number of pages in the collection.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Total items in the collection.
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        /// Number link button will be shown in the pagination.
        /// </summary>
        public int MaxPageLink { get; set; } = 5;

        /// <summary>
        /// Current page items start index.
        /// </summary>
        public long PageItemsStartsAt { get; set; }

        /// <summary>
        /// Current page item end index.
        /// </summary>
        public long PageItemsEndsAt { get; set; }

        /// <summary>
        /// Whether there is any previous page in the pagination.
        /// </summary>
        public bool HasPreviousPage => PageIndex > 1;

        /// <summary>
        /// Whether there is any next page in the pagination.
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
