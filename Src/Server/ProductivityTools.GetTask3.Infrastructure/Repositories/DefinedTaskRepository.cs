using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.GetTask3.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    public interface IDefinedTaskRepository : IRepository<Infrastructure.DefinedElementGroup>
    {
        IEnumerable<DefinedElementGroup> GetForBag(int bagId, bool includeDetails);
        // Domain.DefinedElementGroup Get(int definedElementGroupId);
        DefinedElementGroup GetByName(int bagid, string name);
        DefinedElementGroup GetWithDetails(int definedElementGroupId);
    }

    class DefinedTaskRepository : Repository<Infrastructure.DefinedElementGroup>, IDefinedTaskRepository
    {

        public DefinedTaskRepository(TaskContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<DefinedElementGroup> GetForBag(int bagId, bool includeDetails)
        {
            IEnumerable<DefinedElementGroup> definedTasks;
            if (includeDetails)
            {
                definedTasks = _taskContext.DefinedElementGroup.Include(i => i.Items).Where(x => x.Bag.ElementId == bagId).ToList();
            }
            else
            {
                definedTasks = _taskContext.DefinedElementGroup.Where(x => x.Bag.ElementId == bagId).ToList();
            }


            var r1 = _mapper.Map<IEnumerable<DefinedElementGroup>>(definedTasks);
            return r1;
        }

        public DefinedElementGroup GetByName(int bagid, string name)
        {
            var r = _taskContext.DefinedElementGroup.SingleOrDefault(x => x.BagId == bagid && x.Name == name);
            var r1 = _mapper.Map<DefinedElementGroup>(r);
            return r1;
        }

        public DefinedElementGroup GetWithDetails(int definedElementGroupId)
        {
            var result = _taskContext.DefinedElementGroup.AsNoTracking()
                .Include(x => x.Items)
                .FirstOrDefault(x => x.DefinedElementGroupId == definedElementGroupId);

            return result;
        }
    }
}
