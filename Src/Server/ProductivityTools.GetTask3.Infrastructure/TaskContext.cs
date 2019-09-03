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
        public DbSet<Infrastructure.Element> Element { get; set; }
        public DbSet<Infrastructure.DefinedElementGroup> DefinedElementGroup { get; set; }
        public DbSet<Infrastructure.Tomato> Tomato { get; set; }
      //  public DbSet<Infrastructure.TomatoElement> TomatoItems { get; set; }
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

            modelBuilder.Entity<TomatoElement>().HasKey(x => new { x.TomatoId, x.ElementId });

            //modelBuilder.Entity<Element>().ToTable("Element");

            modelBuilder.Entity<Element>(entity =>
            {
                entity.HasKey(e => e.ElementId);
                entity.Ignore(e => e.Elements);
               // entity.Ignore(e => e.Tomato);
            });

            //modelBuilder.Entity<Domain.DefinedElementGroup>(entity =>
            //{
            //    entity.HasKey(e => e.DefinedElementGroupId);
            //});

            modelBuilder.Entity<Tomato>(entity =>
            {
                entity.HasKey(e => e.TomatoId);
                entity.Property(x => x.Created).HasDefaultValue(DateTime.Now);
            });

            //modelBuilder.Entity<Infrastructure.TomatoElement>()
            //    .HasKey(k => new { k.ElementId, k.TomatoId });
            //modelBuilder.Entity<Infrastructure.TomatoElement>()
            //    .HasOne(x => x.Tomato).WithMany(x => x.TomatoElements).HasForeignKey(x => x.ElementId);
            //modelBuilder.Entity<Infrastructure.TomatoElement>()
            //    .HasOne(x => x.Element).WithMany(x => x.TomatoElements).HasForeignKey(x => x.TomatoId);
        }
    }
}
