using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Base
{
    public interface IRepository<DomainElement,InfrastructureElement>
    {
        void Add(DomainElement entity);
        DomainElement Get(int? id);

        void Update(DomainElement entity, int id);
    }
}
