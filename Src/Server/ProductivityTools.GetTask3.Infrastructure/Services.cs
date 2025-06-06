using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace ProductivityTools.GetTask3.Infrastructure
{
    public static class Services
    {

        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskUnitOfWork, TaskUnitOfWork>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITomatoRepository, TomatoRepository>();
            services.AddScoped<IDefinedTaskRepository, DefinedTaskRepository>();
            services.AddScoped<IDebugRepository, DebugRepository>();
            services.AddScoped<TaskContext>();

            //services.AddLogging(builder => builder
            //    .AddConsole()
            //    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information));
            //var loggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();
            return services;
        }
    }
}
