using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Configuration
{
    public static class Services
    {
        public static IServiceCollection ConfigureServicesConfig(this IServiceCollection services)
        {
            services.AddSingleton<IConfig, Config>();
            return services;
        }
    }
}
