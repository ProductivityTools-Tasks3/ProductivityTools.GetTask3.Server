using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Contract.Requests
{
    public class GetDefinedTaskGroupRequest
    {
        public int BagId { get; set; }
        public string DefinedTaskGroupName { get; set; }
    }
}
