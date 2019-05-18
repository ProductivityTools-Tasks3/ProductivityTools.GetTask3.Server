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

        public Bag(string name, BagType type) :base(name)
        {
            this.Type = type;
        }

        public void Add(Component component)
        {
            this.Components.Add(component);
        }
    }
}
