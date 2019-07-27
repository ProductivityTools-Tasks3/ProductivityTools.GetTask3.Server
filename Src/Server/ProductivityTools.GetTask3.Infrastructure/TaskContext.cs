using Microsoft.EntityFrameworkCore;
using ProductivityTools.GetTask3.Configuration;
using System;


namespace ProductivityTools.GetTask3.Infrastructure
{
    public class TaskContext : DbContext
    {
        private readonly IConfig _config;
        public DbSet<Domain.Element> Element { get; set; }
        public DbSet<Domain.DefinedElementGroup> DefinedElementGroup { get; set; }

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
