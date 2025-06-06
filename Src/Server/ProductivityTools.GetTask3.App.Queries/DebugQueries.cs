using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using ProductivityTools.GetTask3.Infrastructure.Objects;

namespace ProductivityTools.GetTask3.App.Queries
{
    public interface IDebugQueries
    {
        string GetServerName();
    }
    internal class DebugQueries : IDebugQueries
    {
        private readonly IDebugRepository _debugRepository;

        public DebugQueries(IDebugRepository debugRepository)
        {
            _debugRepository = debugRepository;
        }

        public string GetServerName()
        {
            Debug debugInfo = _debugRepository.Get(null);
            return debugInfo.ServerName;
        }
    }
}
