using Microsoft.EntityFrameworkCore;
using ProductivityTools.GetTask3.Configuration;
using System;


namespace ProductivityTools.GetTask3.Infrastructure
{
    public class TaskContext : DbContext
    {
        private readonly IConfig _config;
        public DbSet<Element> Elements { get; set; }

        public TaskContext(IConfig config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("gt");
            modelBuilder.Entity<Element>(entity =>
            {
                entity.HasKey(e => e.ElementId);

            });
        }
    }
}
