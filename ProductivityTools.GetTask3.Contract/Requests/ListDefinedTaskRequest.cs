using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Contract.Requests
{
    public class ListDefinedTaskRequest
    {
        public int? BagId { get; set; }
        public bool IncudeDetails { get; set; }
    }
}
