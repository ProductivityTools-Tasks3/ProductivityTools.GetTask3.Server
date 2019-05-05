using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.Cqrs.HttpConnector
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpToCqrsConnector(this IServiceCollection services)
        {
            //services.AddSingleton<JObjectDtoParser>();
            services.AddMvc().AddApplicationPart(typeof(CqrsMediatorController).Assembly);
            services.RegisterCqrsConnector<HttpToCqrsConnector, HttpToCqrsOptions>();
            return services;
        }
    }
}
