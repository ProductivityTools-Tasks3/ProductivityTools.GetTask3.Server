using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Domain
{
    public class Bag : Component
    {
        BagType Type;
        //pw: make it private and add external contract
        public List<Component> Components = new List<Component>();
        public string Name => throw new NotImplementedException();

        public Bag(BagType type)
        {
            this.Type = type;
        }

        internal void Add(Component component)
        {
            this.Components.Add(component);
        }
    }
}
