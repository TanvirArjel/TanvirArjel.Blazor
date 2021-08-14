// <copyright file="CustomValidationMessagesExtensions.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using TanvirArjel.Blazor.Utilities;

namespace TanvirArjel.Blazor.Components
{
    /// <summary>
    /// Contains extension methods on <see cref="CustomValidationMessages"/>.
    /// </summary>
    public static class CustomValidationMessagesExtensions
    {
        /// <summary>
        /// Add and diplay errors from the <see cref="HttpResponseMessage"/> object to the current <see cref="EditContext"/>.
        /// </summary>
        /// <param name="validationMessages">The <see cref="CustomValidationMessages"/> object.</param>
        /// <param name="httpResponseMessage">The <see cref="HttpResponseMessage"/> object.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="validationMessages"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="httpResponseMessage"/> is <see langword="null"/>.</exception>
        public static async Task AddAndDisplayAsync(this CustomValidationMessages validationMessages, HttpResponseMessage httpResponseMessage)
        {
            if (validationMessages == null)
            {
                throw new ArgumentNullException(nameof(validationMessages));
            }

            await validationMessages.AddAsync(httpResponseMessage).ConfigureAwait(false);
            validationMessages.Display();
        }

        /// <summary>
        /// Add errors from the <see cref="HttpResponseMessage"/> object to the current <see cref="EditContext"/>.
        /// </summary>
        /// <param name="validationMessages">The <see cref="CustomValidationMessages"/> object.</param>
        /// <param name="httpResponseMessage">The <see cref="HttpResponseMessage"/> object.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="validationMessages"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="httpResponseMessage"/> is <see langword="null"/>.</exception>
        public static async Task AddAsync(this CustomValidationMessages validationMessages, HttpResponseMessage httpResponseMessage)
        {
            if (validationMessages == null)
            {
                throw new ArgumentNullException(nameof(validationMessages));
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

                    try
                    {
                        string responseString = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                        Dictionary<string, List<string>> modelErrors = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(responseString, jsonSerializerOptions);
                        if (modelErrors.Any())
                        {
                            foreach (KeyValuePair<string, List<string>> error in modelErrors)
                            {
                                string errorKey = error.Key;
                                List<string> errorMessages = error.Value;

                                if (string.IsNullOrWhiteSpace(errorKey))
                                {
                                    errors.Add(Guid.NewGuid().ToString(), errorMessages);
                                }
                                else
                                {
                                    errors.Add(errorKey, errorMessages);
                                }
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
                    errors.Add(string.Empty, new List<string>() { ErrorMessages.Http401ErrorMessage });
                }
                else if ((int)httpResponseMessage.StatusCode == 403)
                {
                    errors.Add(string.Empty, new List<string>() { ErrorMessages.Http403ErrorMessage });
                }
                else if ((int)httpResponseMessage.StatusCode == 404)
                {
                    errors.Add(string.Empty, new List<string>() { ErrorMessages.Http404ErrorMessage });
                }
                else if ((int)httpResponseMessage.StatusCode == 500)
                {
                    errors.Add(string.Empty, new List<string>() { ErrorMessages.Http500ErrorMessage });
                }
                else
                {
                    string responseMessage = await httpResponseMessage.Content.ReadAsStringAsync();
                    errors.Add(string.Empty, new List<string> { $"{httpResponseMessage.StatusCode}-{responseMessage}" });
                }

                validationMessages.Add(errors);
            }
        }
    }
}
