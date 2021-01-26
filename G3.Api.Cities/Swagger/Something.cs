using System;
using System.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;

namespace G3.Api.Cities.Swagger
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class SwaggerParameterAttribute : Attribute
    {
        public SwaggerParameterAttribute(string name, string description, ParameterLocation parameterLocation)
        {
            ParameterLocation = parameterLocation;
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }
        public ParameterLocation ParameterLocation { get; private set; }
        public string Description { get; private set; }
        public bool Required { get; set; } = false;
    }
    
    public class SwaggerParameterAttributeFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<SwaggerParameterAttribute>();

            foreach (var attribute in attributes)
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = attribute.Name,
                    Description = attribute.Description,
                    In = attribute.ParameterLocation,
                    Required = attribute.Required
                });    
        }
    }

    public class NonBodyParameter
    {
    }
}