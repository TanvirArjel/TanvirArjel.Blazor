﻿// <copyright file="NavigationManagerExtensions.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Components;

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
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="navigationManager"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="key"/> is <see langword="null"/> or empty.</exception>
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

            return navigationManager.GetQuery<string>(key);
        }

        /// <summary>
        /// Gets the value of a query string from the current URI.
        /// </summary>
        /// <typeparam name="T">The type of the return value.</typeparam>
        /// <param name="navigationManager">The type to be extended.</param>
        /// <param name="key">The key of the query.</param>
        /// <returns>Returns query string value of type <typeparamref name="T"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="navigationManager"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="key"/> is <see langword="null"/> or empty.</exception>
        public static T GetQuery<T>(this NavigationManager navigationManager, string key)
        {
            if (navigationManager == null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            string queryString = new Uri(navigationManager.Uri).Query;

            if (string.IsNullOrWhiteSpace(queryString))
            {
                return default;
            }

            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(queryString);
            string value = nameValueCollection[key];

            if (string.IsNullOrWhiteSpace(value))
            {
                return default;
            }

            T t = (T)Convert.ChangeType(value.ToString(), typeof(T), CultureInfo.InvariantCulture);
            return t;
        }

        /// <summary>
        /// Sets the value of a query string to current URI.
        /// </summary>
        /// <typeparam name="T">The type of the passed value.</typeparam>
        /// <param name="navigationManager">The type to be extended.</param>
        /// <param name="key">The name of the query string.</param>
        /// <param name="value">The value of the query string.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="navigationManager"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="key"/> is <see langword="null"/> or empty.</exception>
        public static void SetQuery<T>(
            this NavigationManager navigationManager,
            string key,
            T value)
        {
            if (navigationManager == null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                [key] = value?.ToString(),
            };

            navigationManager.SetQuery(dictionary);
        }

        /// <summary>
        /// Sets the value of the query strings to current URI.
        /// </summary>
        /// <param name="navigationManager">The type to be extended.</param>
        /// <param name="keyValuePairs">The value of the query strings to be set.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="navigationManager"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="keyValuePairs"/> is <see langword="null"/> or empty.</exception>
        public static void SetQuery(
            this NavigationManager navigationManager,
            IDictionary<string, object> keyValuePairs)
        {
            if (navigationManager == null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            if (keyValuePairs == null)
            {
                throw new ArgumentNullException(nameof(keyValuePairs));
            }

            Dictionary<string, string> queryStringsPairs = keyValuePairs.ToDictionary(kv => kv.Key, kv => kv.Value?.ToString());
            navigationManager.SetQuery(queryStringsPairs);
        }

        /// <summary>
        /// Sets the value of the query strings to current URI.
        /// </summary>
        /// <param name="navigationManager">The type to be extended.</param>
        /// <param name="keyValuePairs">The value of the query strings to be set.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="navigationManager"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="keyValuePairs"/> is <see langword="null"/> or empty.</exception>
        public static void SetQuery(
            this NavigationManager navigationManager,
            IDictionary<string, string> keyValuePairs)
        {
            if (navigationManager == null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            if (keyValuePairs == null)
            {
                throw new ArgumentNullException(nameof(keyValuePairs));
            }

            Uri currentFullUri = new Uri(navigationManager.Uri);

            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(currentFullUri.Query);

            foreach (KeyValuePair<string, string> item in keyValuePairs)
            {
                if (!string.IsNullOrWhiteSpace(item.Value))
                {
                    nameValueCollection[item.Key] = item.Value;
                }
                else
                {
                    nameValueCollection.Remove(item.Key);
                }
            }

            UriBuilder uri = new UriBuilder(currentFullUri.AbsoluteUri)
            {
                Query = nameValueCollection.ToString(),
            };

            navigationManager.NavigateTo(uri.ToString());
        }

        /// <summary>
        /// Sets the value of a query string to the specified URI and navigate to the that udpated URI.
        /// </summary>
        /// <typeparam name="T">The type of the passed value.</typeparam>
        /// <param name="navigationManager">The type to be extended.</param>
        /// <param name="uri">The URI to which navigation will be done.</param>
        /// <param name="key">The name of the query string.</param>
        /// <param name="value">The value of the query string.</param>
        /// <param name="forceLoad">If true, bypasses client-side routing and forces the browser to load the new page from the server,
        /// whether or not the URI would normally be handled by the client-side router.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="navigationManager"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="uri"/> is <see langword="null"/> or empty.</exception>
        public static void NavigateTo<T>(
            this NavigationManager navigationManager,
            string uri,
            string key,
            T value,
            bool forceLoad = false)
        {
            if (navigationManager == null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                [key] = value?.ToString(),
            };

            navigationManager.NavigateTo(uri, dictionary, forceLoad);
        }

        /// <summary>
        /// Sets the value of the query strings to the specified URI and navigate to the that udpated URI.
        /// </summary>
        /// <param name="navigationManager">The type to be extended.</param>
        /// <param name="uri">The URI to which navigation will be done.</param>
        /// <param name="keyValuePairs">The value of the query strings to be set.</param>
        /// <param name="forceLoad">If true, bypasses client-side routing and forces the browser to load the new page from the server,
        /// whether or not the URI would normally be handled by the client-side router.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="navigationManager"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="keyValuePairs"/> is <see langword="null"/> or empty.</exception>
        public static void NavigateTo(
            this NavigationManager navigationManager,
            string uri,
            IDictionary<string, object> keyValuePairs,
            bool forceLoad = false)
        {
            if (navigationManager == null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            if (keyValuePairs == null)
            {
                throw new ArgumentNullException(nameof(keyValuePairs));
            }

            Dictionary<string, string> queryStringsPairs = keyValuePairs.ToDictionary(kv => kv.Key, kv => kv.Value?.ToString());

            navigationManager.NavigateTo(uri, queryStringsPairs, forceLoad);
        }

        /// <summary>
        /// Sets the value of the query strings to the specified URI and navigate to the that udpated URI.
        /// </summary>
        /// <param name="navigationManager">The type to be extended.</param>
        /// <param name="uri">The URI to which navigation will be done.</param>
        /// <param name="keyValuePairs">The value of the query strings to be set.</param>
        /// <param name="forceLoad">If true, bypasses client-side routing and forces the browser to load the new page from the server,
        /// whether or not the URI would normally be handled by the client-side router.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="navigationManager"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="keyValuePairs"/> is <see langword="null"/> or empty.</exception>
        public static void NavigateTo(
            this NavigationManager navigationManager,
            string uri,
            IDictionary<string, string> keyValuePairs,
            bool forceLoad = false)
        {
            if (navigationManager == null)
            {
                throw new ArgumentNullException(nameof(navigationManager));
            }

            if (keyValuePairs == null)
            {
                throw new ArgumentNullException(nameof(keyValuePairs));
            }

            Uri fullUri = new Uri(navigationManager.BaseUri + uri);

            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(fullUri.Query);

            foreach (KeyValuePair<string, string> item in keyValuePairs)
            {
                if (!string.IsNullOrWhiteSpace(item.Value))
                {
                    nameValueCollection[item.Key] = item.Value;
                }
                else
                {
                    nameValueCollection.Remove(item.Key);
                }
            }

            UriBuilder updatedUri = new UriBuilder(fullUri.AbsoluteUri)
            {
                Query = nameValueCollection.ToString(),
            };

            navigationManager.NavigateTo(updatedUri.ToString(), forceLoad);
        }
    }
}
