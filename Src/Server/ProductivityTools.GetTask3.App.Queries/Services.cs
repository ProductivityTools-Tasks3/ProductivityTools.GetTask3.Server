﻿using Microsoft.Extensions.DependencyInjection;
using ProductivityTools.GetTask3.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Queries
{
    public static class Services
    {
        public static IServiceCollection ConfigureServicesQueries(this IServiceCollection services)
        {
            services.AddSingleton<ITaskQueries, TaskQueries>();
            services.AddSingleton<IDefinedTaskQueries,DefinedTaskQueries > ();
            
            services.ConfigureInfrastructureServices();
            return services;
        }
    }
}
