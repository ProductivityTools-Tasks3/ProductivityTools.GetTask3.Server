using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Contract
{
    public class AddRequest
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
