using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.MsSql
{
    public static class Services
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<ITaskRepository, TaskRepository>();
            return services;
        }
    }
}
