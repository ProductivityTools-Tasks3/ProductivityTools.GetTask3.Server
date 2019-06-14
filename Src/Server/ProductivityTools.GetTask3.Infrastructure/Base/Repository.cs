using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TaskContext _taskContext;

        private DbSet<T> _dbSet => _taskContext.Set<T>();

        public Repository(TaskContext taskContext)
        {
            _taskContext = taskContext;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
    }
}
