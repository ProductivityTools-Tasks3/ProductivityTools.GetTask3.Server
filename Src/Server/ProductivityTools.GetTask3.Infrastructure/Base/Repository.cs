using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Base
{
    public class Repository<DomainObject, InfrastructureObject> : IRepository<DomainObject, InfrastructureObject>
        where DomainObject : class
        where InfrastructureObject : class
    {
        protected readonly TaskContext _taskContext;

        protected readonly IMapper _mapper;

        protected DbSet<InfrastructureObject> _dbSet => _taskContext.Set<InfrastructureObject>();

        public Repository(TaskContext taskContext, IMapper mapper)
        {
            _taskContext = taskContext;
            _mapper = mapper;
        }

        public void Add(DomainObject entity)
        {
            InfrastructureObject ifrastructure = _mapper.Map<InfrastructureObject>(entity);
            _dbSet.Add(ifrastructure);
        }

        public DomainObject Get(int? id)
        {
            var x = _dbSet.Find(id);
            _taskContext.Entry(x).State = EntityState.Detached;
            DomainObject d = _mapper.Map<DomainObject>(x);
            return d;
        }

        //pw: change this id to interface on domain object
        public void Update(DomainObject @object)
        {
            InfrastructureObject ifra = _mapper.Map<InfrastructureObject>(@object);
            //_taskContext.Entry(ifra).State = EntityState.Detached;
            _dbSet.Update(ifra);
        }
    }
}
