using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.GetTask3.Infrastructure.Base;
using ProductivityTools.GetTask3.Infrastructure.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    internal class DebugRepository : Repository<Debug>, IDebugRepository
    {
        public DebugRepository(TaskContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public void Add(Debug entity)
        {
            throw new NotImplementedException();
        }

        public Debug Get(int? id)
        {
           string server=base._taskContext.Database.SqlQuery<string>($"select @@SERVERNAME as value").Single();
            return new Debug() { ServerName = server };
        }

        public void Update(Debug entity)
        {
            throw new NotImplementedException();
        }
    }
}
