using Microsoft.Extensions.DependencyInjection;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure
{
    public static class Services
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ITaskUnitOfWork, TaskUnitOfWork>();
            services.AddSingleton<ITaskRepository, TaskRepository>();
            services.AddSingleton<TaskContext>();
            return services;
        }
    }
}
