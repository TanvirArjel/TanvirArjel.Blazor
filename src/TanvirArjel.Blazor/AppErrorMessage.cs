// <copyright file="AppErrorMessage.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

namespace TanvirArjel.Blazor
{
    /// <summary>
    /// Contains common texts for different application errors.
    /// </summary>
    public static class AppErrorMessage
    {
        /// <summary>
        /// Common text for server error.
        /// </summary>
        public const string ServerErrorMessage = "Server Error: There is a problem with the service. Please try again." +
            " If the problem persists then contact with the system admin.";

        /// <summary>
        /// Common text for error occured in the blazor client app.
        /// </summary>
        public const string ClientErrorMessage = "Client Error: There is a problem with the service. Please try again." +
            " If the problem persists then contact with the system admin.";

        /// <summary>
        /// Common text for unauthorized error.
        /// </summary>
        public const string UnAuthorizedErrorMessage = "You are not authorized or don't have enough permission to complete the request.";
    }
}
