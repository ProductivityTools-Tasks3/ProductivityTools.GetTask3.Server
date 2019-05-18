using ProductivityTools.GetTask3.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure
{
    public interface ITaskRepository
    {
        Bag GetStructure();
    }
}
