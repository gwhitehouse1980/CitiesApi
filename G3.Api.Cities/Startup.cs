using System.Security.Principal;
using G3.Api.Cities.Auth;
using G3.Api.Cities.Swagger;
using G3.Core.Implementations;
using G3.Core.Interfaces;
using G3.Core.Models;
using G3.Modules.Cities.EntityFramework.InMemory.Context;
using G3.Modules.Cities.EntityFramework.SqlServer.Settings;
using G3.Modules.Cities.Implementations;
using G3.Modules.Cities.Models;
using G3.Modules.Cities.Validators;
using G3.Services.AutoMapper.Extensions;
using G3.Services.OpenWeatherMap.Extensions;
using G3.Services.OpenWeatherMap.Settings;
using G3.Services.RestCountries.Extensions;
using G3.Services.RestCountries.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace G3.Api.Cities
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            // *** To use In Memory context, just comment out SQL Server below and uncomment here ***
            services.AddScoped<IContext, InMemoryContext>();

            // *** For SQL Server use this, but make sure the appsettings.json file has the correct connection string for your environment ***
            // services.AddScoped<IContext, SqlServerContext>();

            // Add settings and configuration
            services.Configure<DatabaseOptions>(Configuration.GetSection("SqlConnection"));
            services.Configure<OwmSettings>(Configuration.GetSection("OwmSettings"));
            services.Configure<RestCountriesSettings>(Configuration.GetSection("RestCountriesSettings"));
            
            // Add repository entries using the test entity
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IValidator<CityModel>, CityModelValidator>();
            services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
            
            // Generic entity search mappings
            services.AddScoped(typeof(IEntitySearch<,>), typeof(EntitySearch<,>));
            services.AddScoped<IEntitySearchFormatter<CityListModel>, CitySearchFormatter>();

            // Add reference to auto mapper implementation
            services.AddAutoMapper();
            
            // Add reference to rest countries implementation
            services.AddRestCountries();
            
            // Add reference to open weather map implementation
            services.AddOpenWeatherMap();
            
            // Add custom authentication for Api Key based authentication
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = ApiKeyAuthenticationSchemeOptions.ApiKeyScheme;
                    options.DefaultChallengeScheme = ApiKeyAuthenticationSchemeOptions.ApiKeyScheme;
                })
                .AddApiKeyAuth(options => {});
            
            // Add mapping to hard coded ApiKey validator, this could be added as an api key service implementation
            services.AddSingleton<IApiKeyValidator, HardCodedApiKeyValidator>();
            
            // Map IPrincipal to the current one within the HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
            
            // Add logging service 
            services.AddLogging();
            
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerParameterAttributeFilter>();
                //c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "Cities Demo API", Version = "v1.0" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}