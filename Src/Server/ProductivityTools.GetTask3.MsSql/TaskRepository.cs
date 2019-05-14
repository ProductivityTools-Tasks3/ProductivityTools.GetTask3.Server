using ProductivityTools.GetTask3.Domain;
using System;

namespace ProductivityTools.GetTask3.MsSql
{
    public class TaskRepository : ITaskRepository
    {
        private static Bag bag;
        //pw: make it nice repository
        public Bag GetStructure()
        {
            if (bag == null) { bag = new Bag("GetTask3", BagType.GList); }
            return bag;
        }
    }
}
