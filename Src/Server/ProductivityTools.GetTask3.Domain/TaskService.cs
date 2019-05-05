using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Domain
{
    public class TaskService
    {
        //pw:change it
        Bag root = new Bag(BagType.GList);
        public TaskService()
        {
            
            
        }
        
        public Bag GetStructure()
        {
            return this.root;
        }

        public void Add(string name)
        {
            Item item = new Item();
            item.Name = name;
            root.Add(item);
        }
    }
}
