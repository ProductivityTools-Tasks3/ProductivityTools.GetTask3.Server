using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.GetTask3.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    public interface ITomatoRepository : IRepository<Infrastructure.Tomato>
    {
        Infrastructure.Tomato GetCurrent();
        List<Infrastructure.Tomato> GetTomatoReport(DateTime date);
    }

    public class TomatoRepository : Repository<Infrastructure.Tomato>, ITomatoRepository
    {
        public TomatoRepository(TaskContext taskContext, IMapper mapper) : base(taskContext, mapper) { }

        public Infrastructure.Tomato GetCurrent()
        {
            var z = _taskContext.Tomato.AsNoTracking()
                .Include(x=>x.TomatoElements).ThenInclude(x=>x.Element)
                .SingleOrDefault(x=>x.Status==CoreObjects.Tomato.Status.New);
            //Domain.Tomato result = _mapper.Map<Domain.Tomato>(z);
            return z;
        }

        public List<Infrastructure.Tomato> GetTomatoReport(DateTime date)
        {
            var q = _taskContext.Tomato.AsNoTracking()
                .Include(x => x.TomatoElements).ThenInclude(x => x.Element)
                .Where(x => x.Created > date.Date);
            //List<Domain.Tomato> result = _mapper.Map<List<Domain.Tomato>>(q);
            return q.ToList();
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
