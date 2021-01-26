using System.Threading.Tasks;
using G3.Core.Interfaces;
using G3.Core.Models;
using G3.Core.Tests.Mocks.Models;

namespace G3.Core.Tests.Mocks
{
    public class MockModelValidator : IValidator<DataModel>
    {
        private const int EntityNameMaxLength = 50;
        
        public async Task<ValidationResultModel> ValidateAdd(DataModel model)
        {
            var result = new ValidationResultModel();
            
            // Simple validation of string length and such like, assumes 
            //  we are not using standard MVC model validation

            if (model.Name == null)
            {
                // Failed as it is required
                result.AddMessage("EntityName", $"Value is required");
            }
            
            if (model.Name?.Length > EntityNameMaxLength)
            {
                // Failed as it is required
                result.AddMessage("EntityName", $"Maximum length of {EntityNameMaxLength}");
            }

            return await Task.FromResult(result);
        }

        public async Task<ValidationResultModel> ValidateUpdate(DataModel model)
        {
            return await ValidateAdd(model);
        }
    }
}