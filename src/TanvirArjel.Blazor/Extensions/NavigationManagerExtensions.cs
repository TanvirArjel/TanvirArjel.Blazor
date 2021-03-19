// <copyright file="NavigationManagerExtensions.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace TanvirArjel.Blazor.Extensions
{
    /// <summary>
    /// Contains necessary extension methods of <see cref="NavigationManager"/>.
    /// </summary>
    public static class NavigationManagerExtensions
    {
        /// <summary>
        /// Gets the value of a query string from the current URI.
        /// </summary>
        /// <param name="navigationManager">The type to be extended.</param>
        /// <param name="key">The key of the query.</param>
        /// <returns>Returns query string value of type <see cref="string"/>.</returns>
        public static string GetQuery(this NavigationManager navigationManager, string key)
        {
            if (navigationManager == null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            string query = new Uri(navigationManager.Uri).Query;

            if (!string.IsNullOrWhiteSpace(query))
            {
                if (QueryHelpers.ParseQuery(query).TryGetValue(key, out StringValues value))
                {
                    return value;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Sets the value of a query string to current URI.
        /// </summary>
        /// <param name="navigationManager">The type to be extended.</param>
        /// <param name="key">The key of the query.</param>
        /// <param name="value">The value of the query string to be set.</param>
        public static void SetQuery(this NavigationManager navigationManager, string key, string value)
        {
            if (navigationManager == null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            string pathWithQueryString = QueryHelpers.AddQueryString(navigationManager.Uri, key, value);

            navigationManager.NavigateTo(pathWithQueryString);
        }
    }
}
