using System;
using System.Threading.Tasks;
using G3.Core.Interfaces;

namespace G3.Core.Implementations
{
    /// <summary>
    /// This is a very simple hard coded version of an Api Key validator. In production use
    /// you would have an Api Key management process which this would hook into
    /// </summary>
    public class HardCodedApiKeyValidator : IApiKeyValidator
    {
        private const string ApiKey = "citiesapi";
        private static readonly Guid UserId = Guid.Parse("39629f6b-b839-4481-a905-5172f818b1d4");

        public async Task<Guid?> GetUserIdForApiKey(string apiKey)
        {
            if (apiKey.Equals(ApiKey))
            {
                return await Task.FromResult(UserId);
            }

            return null;
        }
    }
}