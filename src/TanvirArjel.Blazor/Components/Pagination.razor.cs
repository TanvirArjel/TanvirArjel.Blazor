using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace TanvirArjel.Blazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Pagination
    {
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public PaginationModel PaginationModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string ListBaseUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Dictionary<string, string> QueryStrings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public bool ShowDetails { get; set; }

        private string PageLink { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            if (QueryStrings != null && QueryStrings.Any())
            {
                string uriWithQueryString = QueryHelpers.AddQueryString(ListBaseUrl, QueryStrings);
                PageLink = uriWithQueryString + "&pageIndex=";
            }
            else
            {
                PageLink = ListBaseUrl + "?pageIndex=";
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PaginationModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MaxPageLink { get; } = 5;

        /// <summary>
        /// 
        /// </summary>
        public long PageItemsStartsAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long PageItemsEndsAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasPreviousPage => PageIndex > 1;

        /// <summary>
        /// 
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
