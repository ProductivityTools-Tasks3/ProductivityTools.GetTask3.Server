using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.Cqrs
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCqrsConnector<TConnector, TOptions>(this IServiceCollection services)
           where TConnector : BaseCqrsConnector<TOptions>
           where TOptions : BaseConnectorOptions, new()
        {
            services.AddSingleton<ConnectorConfiguration<TOptions>>();
            services.AddScoped<TConnector>();
            return services;
        }
    }
}
