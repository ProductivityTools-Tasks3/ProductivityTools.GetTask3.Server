using AutoMapper;
using ProductivityTools.GetTask3.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    public interface ITomatoRepository : IRepository<Domain.Tomato, Infrastructure.Tomato>
    {
        Infrastructure.Tomato Get();
        void Finish();
    }

    public class TomatoRepository : Repository<Domain.Tomato, Infrastructure.Tomato>, ITomatoRepository
    {
        public TomatoRepository(TaskContext taskContext, IMapper mapper) : base(taskContext, mapper) { }

        public Tomato Get()
        {
            var z = _taskContext.Tomatos.Single();
            return z;
        }

        public void Finish()
        {
            var tomato = _taskContext.Tomatos.SingleOrDefault(x => x.Status == CoreObjects.Tomato.Status.New);
            if (tomato != null)
            {
                tomato.Status = CoreObjects.Tomato.Status.Finished;
                //pw: to be changed
                tomato.Finished = DateTime.Now;
            }
        }
    }
}
