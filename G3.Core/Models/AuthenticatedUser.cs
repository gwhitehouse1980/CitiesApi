using System;
using System.Security.Principal;
using G3.Core.Constants;
using G3.Core.Extensions;
using G3.Core.Interfaces;

namespace G3.Core.Models
{
    public class AuthenticatedUser : IAuthenticatedUser
    {
        private readonly IPrincipal _principal;

        public AuthenticatedUser(IPrincipal principal)
        {
            _principal = principal;
        }

        public Guid UserId => Guid.Parse(_principal.GetValue(CustomClaimTypes.UserId));
    }
}