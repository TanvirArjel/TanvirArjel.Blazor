// <copyright file="JwtTokenParser.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace TanvirArjel.Blazor.Authorization
{
    /// <summary>
    /// Provides necessary helper methods for parsing jwt token to get <see cref="ClaimsPrincipal"/>.
    /// </summary>
    public class JwtTokenParser
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokenParser"/> class.
        /// </summary>
        /// <param name="jwtSecurityTokenHandler">An instance of the <see cref="JwtSecurityTokenHandler"/>.</param>
        public JwtTokenParser(JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }

        /// <summary>
        /// Get the <see cref="ClaimsPrincipal"/> representation fo the jwt token.
        /// </summary>
        /// <param name="token">The token will be parsed.</param>
        /// <returns>Returns <see cref="ClaimsPrincipal"/>.</returns>
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Not applicable here.")]
        public ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            JwtSecurityToken jwtToken = new JwtSecurityToken(token);

            List<Claim> microsoftClaims = new List<Claim>();

            foreach (Claim item in jwtToken.Claims)
            {
                switch (item.Type)
                {
                    case JwtRegisteredClaimNames.NameId:
                        microsoftClaims.Add(new Claim(ClaimTypes.NameIdentifier, item.Value));
                        break;
                    case JwtRegisteredClaimNames.Name:
                        microsoftClaims.Add(new Claim(ClaimTypes.Name, item.Value));
                        break;
                    case JwtRegisteredClaimNames.GivenName:
                        microsoftClaims.Add(new Claim(ClaimTypes.GivenName, item.Value));
                        break;
                    case JwtRegisteredClaimNames.Sub:
                        microsoftClaims.Add(new Claim(ClaimTypes.NameIdentifier, item.Value));
                        break;
                    case JwtRegisteredClaimNames.Email:
                        microsoftClaims.Add(new Claim(ClaimTypes.Email, item.Value));
                        break;
                    case JwtRegisteredClaimNames.Iat:
                        microsoftClaims.Add(new Claim(ClaimTypes.Expiration, item.Value));
                        break;
                    case JwtRegisteredClaimNames.Jti:
                        microsoftClaims.Add(new Claim(ClaimTypes.Sid, item.Value));
                        break;
                    default:
                        break;
                }
            }

            ClaimsIdentity identity = new ClaimsIdentity(microsoftClaims, "ServerAuth");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            return claimsPrincipal;
        }

        /// <summary>
        /// Get the <see cref="ClaimsPrincipal"/> representation fo the jwt token.
        /// </summary>
        /// <param name="token">The token will be parsed.</param>
        /// <returns>Returns <see cref="ClaimsPrincipal"/>.</returns>
        public ClaimsPrincipal Parse(string token)
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = "SampleApp",

                ValidateAudience = false,
                ValidAudience = "SampleApp",

                ValidateIssuerSigningKey = false,

                // IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey)),
                // comment this and add this line to fool the validation logic
                SignatureValidator = (token, parameters) =>
                {
                    JwtSecurityToken jwt = new JwtSecurityToken(token);
                    return jwt;
                },

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero,
            };

            validationParameters.RequireSignedTokens = false;

            _jwtSecurityTokenHandler.InboundClaimTypeMap[JwtRegisteredClaimNames.Name] = ClaimTypes.Name;

            ClaimsPrincipal claimsPrincipal = _jwtSecurityTokenHandler
                .ValidateToken(token, validationParameters, out SecurityToken securityToken);

            return claimsPrincipal;
        }
    }
}
