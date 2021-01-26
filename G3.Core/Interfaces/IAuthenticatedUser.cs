using System;

namespace G3.Core.Interfaces
{
    /// <summary>
    /// A simple interface defining what an Authenticated User would look like
    /// </summary>
    public interface IAuthenticatedUser
    {
        /// <summary>
        /// The Authenticated Users UserId
        /// </summary>
        Guid UserId { get; }
    }
}