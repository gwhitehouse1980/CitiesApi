using System.Collections.Generic;
using System.Threading.Tasks;
using G3.Core.Implementations;
using G3.Core.Interfaces;
using G3.Core.Tests.Mocks;
using G3.Core.Tests.Mocks.Entities;
using G3.Core.Tests.Mocks.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FluentAssertions;
using G3.Core.Models;

namespace G3.Core.Tests
{
    public class SimpleEntitySearchTests
    {
        private readonly ServiceProvider _serviceProvider;

        public SimpleEntitySearchTests()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            // Add mocks
            serviceCollection.AddScoped<IContext, MockContext>();

            // Add search and repository entries using the test entity
            serviceCollection.AddScoped(typeof(IEntitySearch<,>), typeof(EntitySearch<,>));
            serviceCollection.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            serviceCollection.AddScoped<IValidator<DataModel>, MockModelValidator>();
            serviceCollection.AddScoped<IAuthenticatedUser, MockAuthenticatedUser>();

            // Add reference to auto mapper implementation
            Services.AutoMapper.Services.ConfigureServices(serviceCollection);
            
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private async Task _prepareTestData()
        {
            var repository = _serviceProvider.GetService<IRepository<DataEntity, DataModel>>();

            (await repository.Add(new DataModel { Name = "My new entity" })).Should().NotBeNull();
            (await repository.Add(new DataModel { Name = "Another new entity" })).Should().NotBeNull();
            (await repository.Add(new DataModel { Name = "And Another new entity" })).Should().NotBeNull();
            (await repository.Add(new DataModel { Name = "One More new entity" })).Should().NotBeNull();
            (await repository.Add(new DataModel { Name = "Maybe One More new entity" })).Should().NotBeNull();
            (await repository.Add(new DataModel { Name = "Last new entity" })).Should().NotBeNull();
        }
        
        [Fact]
        public async Task CanSearchForEntity()
        {
            // Prepare the test data
            await _prepareTestData();
            
            var entitySearch = _serviceProvider.GetService<IEntitySearch<DataEntity, DataModel>>();

            // Now search with no filters
            var searchResultsNoFilter = await entitySearch.GetSearchResults(null);
            
            // Test the results
            searchResultsNoFilter.Should().NotBeNull();
            searchResultsNoFilter.Should().HaveCount(6);
            searchResultsNoFilter[0].Name.Should().Be("My new entity");
            searchResultsNoFilter[1].Name.Should().Be("Another new entity");
            searchResultsNoFilter[2].Name.Should().Be("And Another new entity");
            searchResultsNoFilter[3].Name.Should().Be("One More new entity");
            searchResultsNoFilter[4].Name.Should().Be("Maybe One More new entity");
            searchResultsNoFilter[5].Name.Should().Be("Last new entity");
        }
        
        [Fact]
        public async Task CanSearchForEntityByName()
        {
            // Prepare the test data
            await _prepareTestData();
            
            var entitySearch = _serviceProvider.GetService<IEntitySearch<DataEntity, DataModel>>();

            // Now search with no filters
            var searchResultsNoFilter = await entitySearch.GetSearchResults(new List<SearchFilterModel>
            {
                new SearchFilterModel
                {
                    FieldName = "Name",
                    FieldValue = "Another"
                }
            });
            
            // Test the results
            searchResultsNoFilter.Should().NotBeNull();
            searchResultsNoFilter.Should().HaveCount(1);
            searchResultsNoFilter[0].Name.Should().Be("Another new entity");
        }
    }
}