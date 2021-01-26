using System;
using G3.Core.Interfaces;

namespace G3.Core.Tests.Mocks
{
    public class MockAuthenticatedUser : IAuthenticatedUser
    {
        public Guid UserId { get; } = new Guid();
    }
}