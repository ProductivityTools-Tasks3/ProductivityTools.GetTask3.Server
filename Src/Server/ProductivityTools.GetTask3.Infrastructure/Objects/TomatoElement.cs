using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure
{
    public class TomatoElement
    {
        public int TomatoId { get; set; }
        public int ElementId { get; set; }

        public Tomato Tomato { get; set; }
        public Element Element { get; set; }
    }
}
