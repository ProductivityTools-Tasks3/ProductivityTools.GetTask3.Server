using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.SignalRHubs
{
   public static class Services
    {
        public static IServiceCollection ConfigureSingalRServices(this IServiceCollection services)
        {
            services.AddScoped<TomatoHub>();
            return services;
        }
    }
}
