using ProductivityTools.GetTask3.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    public interface ITomatoRepository : IRepository<Infrastructure.Tomato>
    {
        Infrastructure.Tomato Get();
    }

    public class TomatoRepository : Repository<Infrastructure.Tomato>, ITomatoRepository
    {
        public TomatoRepository(TaskContext taskContext) : base(taskContext) { }

        public Tomato Get()
        {
            var z = _taskContext.Tomato.Single();
            return z;
        }
    }
}
