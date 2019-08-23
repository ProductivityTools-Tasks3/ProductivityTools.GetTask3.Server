using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure
{
    public class DefinedElementGroup
    {
        public string Name { get; protected set; }
        public int DefinedElementGroupId { get; protected set; }
        public Element Bag { get; protected set; }
        //public string ElementName { get; protected set; }
        public int BagId { get; protected set; }

        public List<DefinedElement> Items { get; set; }
    }
}
