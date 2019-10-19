using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Handlers
{
    public static class Services
    {
        public static IServiceCollection ConfigureServicesHandlers(this IServiceCollection services)
        {
            services.AddMediatR(System.Reflection.Assembly.GetAssembly(typeof(Services)));
            return services;
        }
    }
}
