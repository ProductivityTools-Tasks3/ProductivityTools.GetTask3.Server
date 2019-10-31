using ProductivityTools.GetTask3.CoreObjects.Tomato;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Contract
{
    public class TomatoElementView
    {
        public int ElementId { get; set; }
        public string Name { get; set; }
    }

    public class TomatoView
    {
        public int TomatoId { get; set; }
        public Status Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Finished { get; set; }
        public List<TomatoElementView> Elements { get; set; }
    }
}
