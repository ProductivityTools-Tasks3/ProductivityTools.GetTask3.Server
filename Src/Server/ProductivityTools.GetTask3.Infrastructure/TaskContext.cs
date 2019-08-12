using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using ProductivityTools.GetTask3.Configuration;
using System;


namespace ProductivityTools.GetTask3.Infrastructure
{
    public class TaskContext : DbContext
    {


        private readonly IConfiguration _configuration;
        public DbSet<Domain.Element> Element { get; set; }
        public DbSet<Domain.DefinedElementGroup> DefinedElementGroup { get; set; }
        //public DbSet<Domain.TomatoItem> TomatoItem { get; set; }
        //public DbSet<Domain.Tomato> Tomato { get; set; }

        public TaskContext(IConfig config, IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("GetTask3"));
            optionsBuilder.UseLoggerFactory(GetLoggerFactory());
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("gt");
            modelBuilder.Entity<Domain.Element>(entity =>
            {
                entity.HasKey(e => e.ElementId);
                entity.Ignore(e => e.Elements);

            });

            modelBuilder.Entity<Domain.DefinedElementGroup>(entity =>
            {
                entity.HasKey(e => e.DefinedElementGroupId);

                // entity.HasMany(e => e.Elements);
            });
        }
    }
}
