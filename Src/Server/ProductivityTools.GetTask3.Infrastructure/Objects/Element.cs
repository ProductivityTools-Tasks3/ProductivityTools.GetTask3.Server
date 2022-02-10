using ProductivityTools.GetTask3.CoreObjects;
using ProductivityTools.GetTask3.CoreObjects.Tomato;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure
{
    public class Element :BaseObject
    {
        public int ElementId { get;  set; }

        public string Name { get; set; }    
        public string Details { get; set; }
        public int? ParentId { get; protected set; }
        public ElementType Type { get; protected set; }
        public Status Status { get; protected set; }
        public DateTime Created { get; protected set; }
        //pw:change to started
        public DateTime? Initialization { get; protected set; }
        public DateTime? Started { get; protected set; }
        public DateTime? Finished { get; protected set; }
        public string Category { get; protected set; }
        public bool Cleared { get; protected set; }

        public List<Element> Elements { get; set; }

        //pw: change this public/
        //public List<Tomato> Tomato { get; set; }

        public List<TomatoElement> TomatoElements { get; protected set; }
    }
}
