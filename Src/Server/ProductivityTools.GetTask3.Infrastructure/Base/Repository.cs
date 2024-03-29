﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Base
{
    public class Repository<InfrastructureObject> : IRepository<InfrastructureObject>
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

        public void Add(InfrastructureObject entity)
        {
            InfrastructureObject ifrastructure = _mapper.Map<InfrastructureObject>(entity);
            //pw: here test are failing if add chosen
            _dbSet.Attach(ifrastructure);
           // _dbSet.Add(ifrastructure);
        }

        public InfrastructureObject Get(int? id)
        {
            var x = _dbSet.Find(id);
            _taskContext.Entry(x).State = EntityState.Detached;
            // domain element DomainObject d = _mapper.Map<DomainObject>(x);
            return x;
        }

        //pw: change this id to interface on domain object
        public void Update(InfrastructureObject @object)
        {
            InfrastructureObject ifra = _mapper.Map<InfrastructureObject>(@object);
            //_taskContext.Entry(ifra).State = EntityState.Detached;
            _dbSet.Update(ifra);
        }
    }
}
