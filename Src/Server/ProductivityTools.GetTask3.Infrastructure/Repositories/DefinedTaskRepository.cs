using AutoMapper;using Microsoft.EntityFrameworkCore;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    public interface IDefinedTaskRepository : IRepository<DefinedElementGroup>
    {
        IEnumerable<Domain.DefinedElementGroup> GetForBag(int bagId, bool includeDetails);
    }

    class DefinedTaskRepository : Repository<DefinedElementGroup>, IDefinedTaskRepository
    {
        private readonly IMapper _mapper;

        public DefinedTaskRepository(TaskContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public IEnumerable<Domain.DefinedElementGroup> GetForBag(int bagId, bool includeDetails)
        {
            IEnumerable<Domain.DefinedElementGroup> definedTasks;
            if (includeDetails)
            {
                definedTasks = _taskContext.DefinedElementGroup.Include(i=>i.Items).Where(x => x.Bag.ElementId == bagId).ToList();
            }
            else
            {
             definedTasks= _taskContext.DefinedElementGroup.Where(x => x.Bag.ElementId == bagId).ToList();
            }
            return definedTasks;
        }
    }
}
