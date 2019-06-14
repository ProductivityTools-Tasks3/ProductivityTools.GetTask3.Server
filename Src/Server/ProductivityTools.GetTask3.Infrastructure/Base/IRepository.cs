using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Base
{
    public interface IRepository<T>
    {
        void Add(T entity);
    }
}
