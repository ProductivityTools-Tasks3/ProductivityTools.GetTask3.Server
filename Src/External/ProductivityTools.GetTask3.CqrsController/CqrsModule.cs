using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.CqrsController
{
    public abstract class CqrsModule
    {
        public string Name;

        public CqrsModule(string module)
        {
            Name = module.ToLower();
        }
    }
}
