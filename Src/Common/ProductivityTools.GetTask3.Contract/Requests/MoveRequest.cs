using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Contract.Requests
{
    public class MoveRequest
    {
        public int[] ElementIds { get; set; }
        public int Target { get; set; }
    }
}
