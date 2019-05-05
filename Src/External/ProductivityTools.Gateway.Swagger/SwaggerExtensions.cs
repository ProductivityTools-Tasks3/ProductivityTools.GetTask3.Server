using Microsoft.Extensions.DependencyInjection;
using ProductivityTools.Gateway.Swagger.Settings;
using System;

namespace ProductivityTools.Gateway.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, SwaggerSettings settings)
        {
            return services;
        }
    }
}
