using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Domain
{
    public class Element 
    {
        public string Name { get; set; }
        //pw:change it to Id  
        public int ElementId { get; set; }
        public int? BagId { get; set; }
        public int OrderId { get; set; }
        public ElementType Type { get; set; }
        public Status Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? Finished { get; set; }

        public List<Element> Elements { get; set; }

        public Element(string name, ElementType type)
        {
            this.Name = name;
            this.Type = type;
            this.Elements = new List<Element>();
        }

        public Element(int id, ElementType type, string name, int orderId, Status status)
        {
            this.ElementId = id;
            this.Type = type;
            this.Name = name;
            this.OrderId = orderId;
            this.Status = status;
        }
    }
}
