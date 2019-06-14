using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Base
{
    public interface IUnitOfWork
    {
        void Commit();
        void RejectChanges();
        void Dispose();
    }
}
