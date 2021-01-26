using System;
using System.Security.Claims;
using System.Security.Principal;

namespace G3.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetValue(this IPrincipal principal, string claimType)
        {
            var claimValue = (principal as ClaimsPrincipal)?.FindFirst(claimType)?.Value;

            if (claimValue == null)
            {
                throw new Exception("Invalid claim type");
            }
            
            return claimValue;
        }
    }
}