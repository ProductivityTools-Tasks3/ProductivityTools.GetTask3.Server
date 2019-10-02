using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Contract.Requests
{
    public class AddToTomatoByNameRequest
    {
        public int ParentId { get; set; }
        public string TaskName { get; set; }
    }
}
