using Microsoft.AspNetCore.Authentication;

namespace G3.Api.Cities.Auth
{
    public class ApiKeyAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public static string ApiKeyScheme = "ApiKeyAuthScheme";
    }
}