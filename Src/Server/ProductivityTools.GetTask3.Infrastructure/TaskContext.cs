using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using ProductivityTools.GetTask3.Configuration;
using ProductivityTools.GetTask3.Infrastructure.Objects;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static ProductivityTools.GetTask3.Infrastructure.TaskContext;

namespace ProductivityTools.GetTask3.Infrastructure
{
    public class TaskContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Infrastructure.Element> Element { get; set; }
        public DbSet<Infrastructure.DefinedElementGroup> DefinedElementGroup { get; set; }
        public DbSet<Infrastructure.Tomato> Tomato { get; set; }
        private DbSet<Ownership> Ownership { get; set; }
        //  public DbSet<Infrastructure.TomatoElement> TomatoItems { get; set; }
        //public DbSet<Domain.Tomato> Tomato { get; set; }


        public bool ValidateOwnership(int treeId, string userName)
        {
            var r = this.Ownership.FromSqlRaw($"select gt.ValidateOwnership({treeId},'{userName}') AS HasAccess").First();
            return r.HasAccess;
        }

        public TaskContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

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
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("GetTask3"));
                optionsBuilder.UseLoggerFactory(GetLoggerFactory());
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("gt");
            modelBuilder.Entity<Ownership>().HasNoKey();
            modelBuilder.Entity<TomatoElement>().HasKey(x => new { x.TomatoId, x.ElementId });

            //modelBuilder.Entity<Element>().ToTable("Element");

            modelBuilder.Entity<Element>(entity =>
            {
                entity.HasKey(e => e.ElementId);
                entity.Ignore(e => e.Elements);
                entity.Ignore(x => x.Notifications);
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
                entity.Ignore(x => x.Notifications);
            });

            //modelBuilder.Entity<Infrastructure.TomatoElement>()
            //    .HasKey(k => new { k.ElementId, k.TomatoId });
            //modelBuilder.Entity<Infrastructure.TomatoElement>()
            //    .HasOne(x => x.Tomato).WithMany(x => x.TomatoElements).HasForeignKey(x => x.ElementId);
            //modelBuilder.Entity<Infrastructure.TomatoElement>()
            //    .HasOne(x => x.Element).WithMany(x => x.TomatoElements).HasForeignKey(x => x.TomatoId);

            //modelBuilder.HasDbFunction(typeof(TaskContext).GetMethod(nameof(ValidateOwnership), new[] { typeof(int), typeof(string) })).HasName("ValidateOwnership");
            //modelBuilder
            //    .HasDbFunction(typeof(TaskContext).GetMethod(nameof(ValidateOwnership), new[] { typeof(int), typeof(string) })).HasSchema("gt").HasName("ValidateOwnership");
        }
    }
}
