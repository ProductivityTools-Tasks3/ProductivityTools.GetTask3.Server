using ProductivityTools.GetTask3.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure
{
    public class TaskRepository : ITaskRepository
    {

        public TaskRepository() { }

        private static Bag bag;
        //pw: make it nice repository
        public Bag GetStructure()
        {
            if (bag == null) { bag = new Bag("GetTask3", BagType.GList); }
            return bag;
        }
    }
}
