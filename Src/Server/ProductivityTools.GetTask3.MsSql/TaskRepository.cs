using ProductivityTools.GetTask3.Domain;
using System;

namespace ProductivityTools.GetTask3.MsSql
{
    public class TaskRepository : ITaskRepository
    {
        //pw: make it nice repository
        public Bag GetStructure()
        {
            Bag root = new Bag("GetTask3", BagType.GList);
            return root;
        }
    }
}
