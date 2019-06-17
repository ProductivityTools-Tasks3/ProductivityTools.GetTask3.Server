using Microsoft.EntityFrameworkCore;
using ProductivityTools.GetTask3.Configuration;
using System;


namespace ProductivityTools.GetTask3.Infrastructure
{
    public class TaskContext : DbContext
    {
        private readonly IConfig _config;
        public DbSet<Domain.Element> Elements { get; set; }

        public TaskContext(IConfig config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.ConnectionString);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("gt");
            modelBuilder.Entity<Domain.Element>(entity =>
            {
                entity.HasKey(e => e.ElementId);
                entity.Ignore(e => e.OrderId);
                entity.Ignore(e => e.Elements);
            });
        }
    }
}
