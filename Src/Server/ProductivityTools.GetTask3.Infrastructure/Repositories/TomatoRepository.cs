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
        Domain.Tomato GetCurrent();
        void Finish();
    }

    public class TomatoRepository : Repository<Domain.Tomato, Infrastructure.Tomato>, ITomatoRepository
    {
        public TomatoRepository(TaskContext taskContext, IMapper mapper) : base(taskContext, mapper) { }

        public Domain.Tomato GetCurrent()
        {
            var z = _taskContext.Tomato.SingleOrDefault();
            Domain.Tomato result = _mapper.Map<Domain.Tomato>(z);
            return result;
        }

        public void Finish()
        {
            var tomato = _taskContext.Tomato.SingleOrDefault(x => x.Status == CoreObjects.Tomato.Status.New);
            if (tomato != null)
            {
                tomato.Status = CoreObjects.Tomato.Status.Finished;
                //pw: to be changed
                tomato.Finished = DateTime.Now;
            }
        }
    }
}
