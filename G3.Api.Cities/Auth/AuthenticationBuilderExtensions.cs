using System;
using Microsoft.AspNetCore.Authentication;

namespace G3.Api.Cities.Auth
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddApiKeyAuth(this AuthenticationBuilder builder, Action<ApiKeyAuthenticationSchemeOptions> configureOptions)
        {
            return builder.AddScheme<ApiKeyAuthenticationSchemeOptions, ApiKeyAuthHandler>(ApiKeyAuthenticationSchemeOptions.ApiKeyScheme, configureOptions);
        }
    }
}