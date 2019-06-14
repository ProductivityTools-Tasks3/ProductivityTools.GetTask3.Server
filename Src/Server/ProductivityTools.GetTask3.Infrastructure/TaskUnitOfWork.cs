using Microsoft.EntityFrameworkCore;
using ProductivityTools.GetTask3.Infrastructure.Base;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure
{
    public class TaskUnitOfWork : ITaskUnitOfWork
    {
        private readonly TaskContext _dbContext;

        public TaskRepository TaskRepository => new TaskRepository(_dbContext);

        public TaskUnitOfWork(TaskContext taskContext)
        {
            _dbContext = taskContext;
        }


        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void RejectChanges()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries()
               .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}
