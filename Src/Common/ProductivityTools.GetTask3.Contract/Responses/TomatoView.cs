using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Contract
{
    public class TomatoView
    {
        public int TomatoId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Finished { get; set; }
    }
}
