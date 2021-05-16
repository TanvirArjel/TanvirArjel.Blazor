// <copyright file="Pagination.razor.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Components;

namespace TanvirArjel.Blazor.Components
{
    /// <summary>
    /// The pagination component which will render the paginaton in html.
    /// </summary>
    public partial class Pagination
    {
        /// <summary>
        /// Gets or sets the pagination object that contains necessary information related to the pagination.
        /// </summary>
        [Parameter]
        public PaginationModel PaginationModel { get; set; }

        private int TotalPages { get; set; }

        private int MaxPageLink { get; set; } = 5;

        private long PageItemsStartsAt { get; set; }

        private long PageItemsEndsAt { get; set; }

        private bool HasPreviousPage { get; set; }

        private bool HasNextPage { get; set; }

        private string PageLink { get; set; }

        private string PrevDisabled { get; set; }

        private string NextDisabled { get; set; }

        /// <summary>
        /// This method will be called during initializaiton of the component.
        /// </summary>
        protected override void OnInitialized()
        {
            if (PaginationModel == null)
            {
                throw new InvalidOperationException($"The {nameof(PaginationModel)} is null.");
            }

            if (PaginationModel.PageIndex <= 0)
            {
                throw new InvalidOperationException($"The {nameof(PaginationModel.PageIndex)} must be greater than 0.");
            }

            if (PaginationModel.PageSize <= 0)
            {
                throw new InvalidOperationException($"The {nameof(PaginationModel.PageSize)} must be greater than 0.");
            }

            if (PaginationModel.TotalItems <= 0)
            {
                throw new InvalidOperationException($"The {nameof(PaginationModel.TotalItems)} can't be less than 0.");
            }

            int pageIndex = PaginationModel.PageIndex;
            int pageSize = PaginationModel.PageSize;
            long totalItems = PaginationModel.TotalItems;

            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            PageItemsStartsAt = totalItems > 0 ? ((pageIndex - 1) * pageSize) + 1 : 0;
            PageItemsEndsAt = totalItems > 0 ? (pageIndex * pageSize > totalItems ? totalItems : pageIndex * pageSize) : 0;
            HasPreviousPage = pageIndex > 1;
            HasNextPage = pageIndex < TotalPages;

            PrevDisabled = HasPreviousPage ? string.Empty : "disabled";
            NextDisabled = HasNextPage ? string.Empty : "disabled";

            if (PaginationModel.QueryStrings != null && PaginationModel.QueryStrings.Any())
            {
                string queryString = new Uri(PaginationModel.ListUrl).Query;
                NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(queryString);

                foreach (KeyValuePair<string, string> item in PaginationModel.QueryStrings)
                {
                    if (item.Value != null)
                    {
                        nameValueCollection[item.Key] = item.Value.ToString();
                    }
                    else
                    {
                        nameValueCollection.Remove(item.Key);
                    }
                }

                UriBuilder updatedUri = new UriBuilder(PaginationModel.ListUrl)
                {
                    Query = nameValueCollection.ToString(),
                };

                PageLink = updatedUri.ToString() + "&pageIndex=";
            }
            else
            {
                PageLink = PaginationModel.ListUrl + "?pageIndex=";
            }
        }
    }

    /// <summary>
    /// The model contains the pagination configuration.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Not applicable here.")]
    public class PaginationModel
    {
        /// <summary>
        /// Gets or sets the index of the current page.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the number of items in each page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets total items in the collection.
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        /// Gets or sets the item list url.
        /// </summary>
        public string ListUrl { get; set; }

        /// <summary>
        /// Gets or sets the query strings of the item list if any. This is can be used to retain the search or order during pagination.
        /// </summary>
        public Dictionary<string, string> QueryStrings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the pagination details will be shown. Default value is true.
        /// </summary>
        public bool ShowDetails { get; set; } = true;
    }
}
