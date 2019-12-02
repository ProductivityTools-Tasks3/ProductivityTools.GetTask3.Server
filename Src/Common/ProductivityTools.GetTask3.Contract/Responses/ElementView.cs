using ProductivityTools.GetTask3.CoreObjects;
using System;
using System.Collections.Generic;

namespace ProductivityTools.GetTask3.Contract
{
    public class ElementView 
    {
        public string Name { get; set; }
        public ElementType Type { get; set; }
        public int ElementId { get; set; }
        public int? ParentId { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Initialization { get; set; }
        public DateTime Started { get; set; }
        public DateTime? Finished { get; set; }
        public string Category { get; set; }

        public List<ElementView> Elements { get; set; }
        public List<TomatoView> Tomatoes {get;set;}
    }
}
