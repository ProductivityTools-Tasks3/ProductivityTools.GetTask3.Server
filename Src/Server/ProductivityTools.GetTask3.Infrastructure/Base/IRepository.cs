using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Base
{
    public interface IRepository<InfrastructureElement>
    {
        void Add(InfrastructureElement entity);
        InfrastructureElement Get(int? id);

        void Update(InfrastructureElement entity);
    }
}
