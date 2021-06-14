// <copyright file="CustomValidatorExtensions.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TanvirArjel.Blazor
{
    /// <summary>
    /// Contains extension methods on <see cref="CustomValidator"/>.
    /// </summary>
    public static class CustomValidatorExtensions
    {
        /// <summary>
        /// Add and diplay errors from the <see cref="HttpResponseMessage"/> object.
        /// </summary>
        /// <param name="customValidator">The <see cref="CustomValidator"/> object.</param>
        /// <param name="httpResponseMessage">The <see cref="HttpResponseMessage"/> object.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="customValidator"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="httpResponseMessage"/> is <see langword="null"/>.</exception>
        public static async Task AddErrorsAndDisplayAsync(this CustomValidator customValidator, HttpResponseMessage httpResponseMessage)
        {
            await AddErrorsAsync(customValidator, httpResponseMessage).ConfigureAwait(false);
            customValidator.DisplayErrors();
        }

        /// <summary>
        /// Add errors from the <see cref="HttpResponseMessage"/> object to the current form context.
        /// </summary>
        /// <param name="customValidator">The <see cref="CustomValidator"/> object.</param>
        /// <param name="httpResponseMessage">The <see cref="HttpResponseMessage"/> object.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="customValidator"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="httpResponseMessage"/> is <see langword="null"/>.</exception>
        public static async Task AddErrorsAsync(this CustomValidator customValidator, HttpResponseMessage httpResponseMessage)
        {
            if (customValidator == null)
            {
                throw new ArgumentNullException(nameof(customValidator));
            }

            if (httpResponseMessage == null)
            {
                throw new ArgumentNullException(nameof(httpResponseMessage));
            }

            if (httpResponseMessage.IsSuccessStatusCode == false)
            {
                Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

                if ((int)httpResponseMessage.StatusCode == 400)
                {
                    JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    string responseString = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    try
                    {
                        Dictionary<string, List<string>> modelErrors = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(responseString, jsonSerializerOptions);
                        if (modelErrors.Any())
                        {
                            foreach (KeyValuePair<string, List<string>> error in modelErrors)
                            {
                                string fieldName = error.Key;
                                List<string> errorMessages = error.Value;
                                errors.Add(fieldName, errorMessages);
                            }
                        }
                        else
                        {
                            errors.Add(string.Empty, new List<string>() { "Invalid request. One or more validations failed." });
                        }
                    }
                    catch (Exception)
                    {
                        errors.Add(string.Empty, new List<string>() { "Invalid request. One or more validations failed." });
                    }
                }
                else if ((int)httpResponseMessage.StatusCode == 401)
                {
                    errors.Add(string.Empty, new List<string>() { AppErrorMessage.UnAuthorizedErrorMessage });
                }
                else
                {
                    errors.Add(string.Empty, new List<string> { AppErrorMessage.ServerErrorMessage });
                }

                customValidator.AddErrors(errors);
            }
        }
    }
}
