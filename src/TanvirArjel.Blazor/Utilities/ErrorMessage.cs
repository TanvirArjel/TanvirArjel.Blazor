// <copyright file="ErrorMessage.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

namespace TanvirArjel.Blazor.Utilities
{
    /// <summary>
    /// Contains common texts for different application errors.
    /// </summary>
    public static class ErrorMessage
    {
        /// <summary>
        /// Common error text for http 500 server error.
        /// </summary>
        public const string Http500ErrorMessage = "Server Error: There is a problem with the service. Please try again." +
            " If the problem persists then contact with the system admin.";

        /// <summary>
        /// Common error text for error occured in the blazor client app.
        /// </summary>
        public const string ClientErrorMessage = "Client Error: There is a problem with the service. Please try again." +
            " If the problem persists then contact with the system admin.";

        /// <summary>
        /// Common error text for unauthorized or http 401 error.
        /// </summary>
        public const string Http401ErrorMessage = "You are not authorized to complete the request.";

        /// <summary>
        /// Common error text for access denied or http 403 error.
        /// </summary>
        public const string Http403ErrorMessage = "You don't have sufficient permission to complete the request.";

        /// <summary>
        /// Common error text for requested path not found or 404 error.
        /// </summary>
        public const string Http404ErrorMessage = "The requested HTTP path is not found. The path is either does not exist or removed.";

        /// <summary>
        /// Common error text for server is down or CORS access error.
        /// </summary>
        public const string ServerDownOrCorsErrorMessage = "It seems maybe the server is down or the client app does not have CORS access.";
    }
}
