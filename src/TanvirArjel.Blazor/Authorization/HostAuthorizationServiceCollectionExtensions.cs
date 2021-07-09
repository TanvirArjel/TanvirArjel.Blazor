// <copyright file="HostAuthorizationServiceCollectionExtensions.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

using System;
using System.IdentityModel.Tokens.Jwt;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace TanvirArjel.Blazor.Authorization
{
    /// <summary>
    /// Contains extension methods for setting up necessary authorization services in <see cref="IServiceCollection"/>.
    /// </summary>
    public static class HostAuthorizationServiceCollectionExtensions
    {
        /// <summary>
        /// Add the necessary authorization services in <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The type to be extened.</param>
        public static void AddHostAuthorization(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<JwtSecurityTokenHandler>();
            services.AddScoped<JwtTokenParser>();

            services.AddBlazoredLocalStorage();

            services.AddOptions();
            services.AddAuthorizationCore();
            services.AddScoped<HostAuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<HostAuthStateProvider>());
        }
    }
}
