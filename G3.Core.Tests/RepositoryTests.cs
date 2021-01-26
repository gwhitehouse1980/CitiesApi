using System.Threading.Tasks;
using G3.Core.Implementations;
using G3.Core.Interfaces;
using G3.Core.Tests.Mocks;
using G3.Core.Tests.Mocks.Entities;
using G3.Core.Tests.Mocks.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FluentAssertions;
using G3.Core.Enums;

namespace G3.Core.Tests
{
    public class RepositoryTests
    {
        private readonly ServiceProvider _serviceProvider;

        public RepositoryTests()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            // Add mocks
            serviceCollection.AddScoped<IContext, MockContext>();

            // Add repository entries using the test entity
            serviceCollection.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            serviceCollection.AddScoped<IValidator<DataModel>, MockModelValidator>();
            serviceCollection.AddScoped<IAuthenticatedUser, MockAuthenticatedUser>();

            // Add reference to auto mapper implementation
            Services.AutoMapper.Services.ConfigureServices(serviceCollection);
            
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
        
        [Fact]
        public async Task CanCreateEntityFromModel()
        {
            var repository = _serviceProvider.GetService<IRepository<DataEntity, DataModel>>();

            var theDataModel = new DataModel
            {
                Name = "My new entity"
            };

            var result = await repository.Add(theDataModel);

            // Test the data in the response
            result.ResultType.Should().Be(ResultTypeEnum.Success);
            result.CurrentVersion.Should().NotBeNull();
            result.PreviousVersion.Should().BeNull();
            result.CurrentVersion.Name.Should().Be(theDataModel.Name);
            result.CurrentVersion.Id.Should().NotBeNull();
        }
        
        [Fact]
        public async Task CanUpdateEntityFromModel()
        {
            var repository = _serviceProvider.GetService<IRepository<DataEntity, DataModel>>();

            var theDataModel = new DataModel
            {
                Name = "My new entity"
            };
            
            // Need to add it first
            var addResult = await repository.Add(theDataModel);
            
            // Not testing the result, this should be done in another test, here we can assume all is good
            //  However we will do one check for null on the new version
            addResult.CurrentVersion.Should().NotBeNull();
            addResult.CurrentVersion.Id.Should().NotBeNull();

            // Take the Id thats been created 
            theDataModel.Id = addResult.CurrentVersion.Id.Value;
            theDataModel.Name = "My updated entity";
            
            // Now we can do an update
            var result = await repository.Update(theDataModel.Id.Value, theDataModel);

            // Test the data in the response
            result.ResultType.Should().Be(ResultTypeEnum.Success);
            result.CurrentVersion.Should().NotBeNull();
            result.PreviousVersion.Should().NotBeNull();
            
            result.CurrentVersion.Id.Should().NotBeNull();
            result.CurrentVersion.Id.Should().Be(theDataModel.Id);

            result.CurrentVersion.Name.Should().Be(theDataModel.Name);
            result.PreviousVersion.Name.Should().Be("My new entity");
        }
        
        [Fact]
        public async Task CanGetModel()
        {
            var repository = _serviceProvider.GetService<IRepository<DataEntity, DataModel>>();

            var theDataModel = new DataModel
            {
                Name = "My new entity"
            };
            
            // Need to add it first
            var addResult = await repository.Add(theDataModel);
            
            // Not testing the result, this should be done in another test, here we can assume all is good
            //  However we will do a check for null on the new version
            addResult.CurrentVersion.Should().NotBeNull();
            addResult.CurrentVersion.Id.Should().NotBeNull();
            
            // Now we can do an update
            var result = await repository.Get(addResult.CurrentVersion.Id.Value);

            // Test the data in the response
            result.ResultType.Should().Be(ResultTypeEnum.Success);
            result.CurrentVersion.Should().NotBeNull();
            result.CurrentVersion.Id.Should().NotBeNull();
            result.CurrentVersion.Id.Should().Be(addResult.CurrentVersion.Id.Value);
            result.CurrentVersion.Name.Should().Be(theDataModel.Name);
        }
        
        [Fact]
        public async Task CanDeleteModel()
        {
            var repository = _serviceProvider.GetService<IRepository<DataEntity, DataModel>>();

            var theDataModel = new DataModel
            {
                Name = "My new entity"
            };
            
            // Need to add it first
            var addResult = await repository.Add(theDataModel);
            
            // Not testing the result, this should be done in another test, here we can assume all is good
            //  However we will do a check for null on the new version
            addResult.CurrentVersion.Should().NotBeNull();
            addResult.CurrentVersion.Id.Should().NotBeNull();
            
            // Now we can do an update
            var getResult = await repository.Get(addResult.CurrentVersion.Id.Value);

            // Test the data in the response
            getResult.ResultType.Should().Be(ResultTypeEnum.Success);
            getResult.CurrentVersion.Should().NotBeNull();
            
            // Now delete the entity
            var deleteResult = await repository.Delete(addResult.CurrentVersion.Id.Value);
            
            // Test that the data on the returned set is all ok
            deleteResult.ResultType.Should().Be(ResultTypeEnum.Success);
            deleteResult.CurrentVersion.Should().BeNull();
            deleteResult.PreviousVersion.Should().NotBeNull();
            deleteResult.PreviousVersion.Id.Should().Be(addResult.CurrentVersion.Id.Value);
            
            // Now finally do another get and make sure the result is not found
            var secondGetResult = await repository.Get(addResult.CurrentVersion.Id.Value);
            
            // Test the data in the response
            secondGetResult.ResultType.Should().Be(ResultTypeEnum.NotFound);
            secondGetResult.CurrentVersion.Should().BeNull();
            secondGetResult.PreviousVersion.Should().BeNull();
        }
    }
}