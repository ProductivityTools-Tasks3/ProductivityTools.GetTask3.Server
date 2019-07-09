﻿using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    public interface ITaskRepository: IRepository<Domain.Element>
    {
        Domain.Element GetStructure(int? root = null);
        //void AddItem(string name);

        Element Get(int? id);
    }
}
