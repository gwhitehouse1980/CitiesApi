using AutoMapper;
using G3.Core.Interfaces;
using G3.Services.AutoMapper.Extensions;
using G3.Services.AutoMapper.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace G3.Services.AutoMapper
{
    public static class Services
    {
        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(typeof(IModelMapper<>), typeof(AutoMapperModelMapper<>));
            
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.IgnoreUnmapped();     
            });

            var mapper = mappingConfig.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }
    }
}