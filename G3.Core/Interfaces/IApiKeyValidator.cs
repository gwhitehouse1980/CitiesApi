using System;
using System.Threading.Tasks;

namespace G3.Core.Interfaces
{
    /// <summary>
    /// Interface defining the Api Key Validator
    /// </summary>
    public interface IApiKeyValidator
    {
        /// <summary>
        /// Lookup a particular UserId for a given ApiKey
        /// </summary>
        /// <param name="apiKey">The ApiKey being used in calls to the Api</param>
        /// <returns>A nullable Guid, null implies not found, a value would imply it has been found</returns>
        Task<Guid?> GetUserIdForApiKey(string apiKey);
    }
}