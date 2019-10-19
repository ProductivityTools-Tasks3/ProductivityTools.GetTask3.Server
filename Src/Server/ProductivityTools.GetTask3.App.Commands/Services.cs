using Microsoft.Extensions.DependencyInjection;
using ProductivityTools.DateTimeTools;
using ProductivityTools.GetTask3.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace ProductivityTools.GetTask3.App.Commands
{
    public static class Services
    {
        public static IServiceCollection ConfigureServicesComandsInternal(this IServiceCollection services)
        {
            services.AddScoped<IGTaskCommands, TaskCommands>();
            services.AddScoped<IDateTimePT, DateTimePT>();
            services.AddScoped<IDefinedTaskCommands, DefinedTaskCommands>();
            return services;
        }

        public static IServiceCollection ConfigureServicesCommands(this IServiceCollection services)
        {
            services.ConfigureServicesComandsInternal();
            services.ConfigureInfrastructureServices();
            return services;
        }
    }
}
