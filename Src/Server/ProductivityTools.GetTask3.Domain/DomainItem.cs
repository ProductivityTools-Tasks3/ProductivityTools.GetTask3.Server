using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Domain
{
    public class DomainItem : Component
    {
        public int TaskOrderId { get; set; }

        public DomainItem(string name) : base(name) { }
    }
}
