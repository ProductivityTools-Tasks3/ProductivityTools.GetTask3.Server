using ProductivityTools.GetTask3.CoreObjects.Tomato;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure
{
    public class Tomato
    {
        public int TomatoId { get; set; }
        public Status Status { get; set; }
        public DateTime Created { get; set; }
        public List<TomatoItem> Items { get; set; }
    }
}
