using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
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
            services.AddSingleton<ITomatoRepository, TomatoRepository>();
            services.AddSingleton<IDefinedTaskRepository, DefinedTaskRepository>();
            services.AddSingleton<TaskContext>();
            //services.AddLogging(builder => builder
            //    .AddConsole()
            //    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information));
            //var loggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();
            return services;
        }
    }
}
