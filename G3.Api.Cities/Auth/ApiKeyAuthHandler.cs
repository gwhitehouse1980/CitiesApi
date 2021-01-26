using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using G3.Core.Constants;
using G3.Core.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace G3.Api.Cities.Auth
{
    public class ApiKeyAuthHandler : AuthenticationHandler<ApiKeyAuthenticationSchemeOptions>
    {
        private readonly IApiKeyValidator _apiKeyValidator;

        public ApiKeyAuthHandler(IOptionsMonitor<ApiKeyAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IApiKeyValidator apiKeyValidator)
            : base(options, logger, encoder, clock)
        {
            _apiKeyValidator = apiKeyValidator;
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            return base.HandleChallengeAsync(properties);
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Query.ContainsKey("ApiKey"))
            {
                return AuthenticateResult.Fail("Api key not supplied");
            }

            var apiKey = Request.Query.FirstOrDefault(o => o.Key == "ApiKey");
                
            // Lookup the api key in the api key store, add the user Id to the principal
            var userId = await _apiKeyValidator.GetUserIdForApiKey(apiKey.Value);

            if (!userId.HasValue)
            {
                return AuthenticateResult.Fail("Api key invalid");
            }

            // Build claims, at the moment this is just the user id
            var claims = new List<Claim>
            {
                new Claim(CustomClaimTypes.UserId, userId.ToString())
            };

            var identities = new List<ClaimsIdentity>
            {
                new ClaimsIdentity(claims, ApiKeyAuthenticationSchemeOptions.ApiKeyScheme)
            };
            
            var principal = new ClaimsPrincipal(identities);

            return AuthenticateResult.Success(new AuthenticationTicket(principal, ApiKeyAuthenticationSchemeOptions.ApiKeyScheme));
        }
    }
}