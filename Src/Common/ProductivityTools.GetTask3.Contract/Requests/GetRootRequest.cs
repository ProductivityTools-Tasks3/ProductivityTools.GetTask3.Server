using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Contract.Requests
{
    public class GetRootRequest
    {
        public int? ElementId { get; set; }
        public string Path { get; set; }
    }
}
