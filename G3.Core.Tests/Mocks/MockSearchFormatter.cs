using System.Threading.Tasks;
using G3.Core.Interfaces;
using G3.Core.Tests.Mocks.Models;

namespace G3.Core.Tests.Mocks
{
    public class MockSearchFormatter : IEntitySearchFormatter<DataModel>
    {
        public async Task<DataModel> FormatModel(DataModel newModel)
        {
            // Just append a bit of text on the end of the name
            newModel.Name = $"{newModel.Name} FORMATTED";

            return await Task.FromResult(newModel);
        }
    }
}