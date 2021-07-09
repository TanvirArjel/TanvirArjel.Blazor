// <copyright file="HostAuthStateProvider.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace TanvirArjel.Blazor.Authorization
{
    /// <summary>
    ///  Handle and provides information about the authentication state of the current user.
    /// </summary>
    public class HostAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;
        private readonly JwtTokenParser _jwtTokenParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="HostAuthStateProvider"/> class.
        /// </summary>
        /// <param name="localStorage">An instance of the <see cref="LocalStorageService"/>.</param>
        /// <param name="navigationManager">An instance of the <see cref="NavigationManager"/>.</param>
        /// <param name="jwtTokenParser">An instance of the <see cref="JwtTokenParser"/>.</param>
        public HostAuthStateProvider(
            ILocalStorageService localStorage,
            NavigationManager navigationManager,
            JwtTokenParser jwtTokenParser)
        {
            _localStorage = localStorage;
            _navigationManager = navigationManager;
            _jwtTokenParser = jwtTokenParser;
        }

        /// <summary>
        /// Gets the JWT token of the current user.
        /// </summary>
        public string JwtToken { get; private set; }

        /// <summary>
        /// Gets a <see cref="ClaimsPrincipal"/> that describes the current user.
        /// </summary>
        public ClaimsPrincipal User { get; private set; }

        /// <summary>
        /// Gets the JWT token storage key.
        /// </summary>
        private static string JwtTokenStorageKey => "JwtTokenStorageKey";

        /// <summary>
        /// Returns the <see cref="AuthenticationState"/> of the current user.
        /// </summary>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                JwtToken = await _localStorage.GetItemAsync<string>(JwtTokenStorageKey).ConfigureAwait(false);

                if (JwtToken == null)
                {
                    return new AuthenticationState(new ClaimsPrincipal());
                }

                ClaimsPrincipal claimsPrincipal = _jwtTokenParser.Parse(JwtToken);

                AuthenticationState authenticationState = new AuthenticationState(claimsPrincipal);
                User = authenticationState.User;

                return authenticationState;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                AuthenticationState authState = new AuthenticationState(new ClaimsPrincipal());
                User = authState.User;
                return authState;
            }
        }

        /// <summary>
        /// Make current user logged in into the application and update the application state.
        /// </summary>
        /// <param name="jwtToken">A valid jwt token that contains ncecessary user credentials.</param>
        /// <param name="redirectTo">The path to which user will be redirected after logged in.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        public async Task LogInAsync(string jwtToken, string redirectTo = null)
        {
            if (jwtToken == null)
            {
                throw new ArgumentNullException(nameof(jwtToken));
            }

            await _localStorage.SetItemAsync(JwtTokenStorageKey, jwtToken).ConfigureAwait(false);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

            if (redirectTo != null)
            {
                _navigationManager.NavigateTo(redirectTo);
            }
        }

        /// <summary>
        /// Will logout the current user from the application state and update the authentication state.
        /// </summary>
        /// <param name="redirectTo">The path to which user will be redirected after logout.</param>
        /// <returns>Returns <see cref="Task"/>.</returns>
        public async Task LogOutAsync(string redirectTo = null)
        {
            await _localStorage.RemoveItemAsync(JwtTokenStorageKey).ConfigureAwait(false);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

            if (redirectTo != null)
            {
                _navigationManager.NavigateTo(redirectTo);
            }
        }
    }
}
