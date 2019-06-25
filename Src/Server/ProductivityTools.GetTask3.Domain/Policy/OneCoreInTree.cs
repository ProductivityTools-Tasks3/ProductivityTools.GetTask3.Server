using ProductivityTools.GetTask3.CoreObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Domain.Policy
{
    public class OneCoreInTree : IOneCoreInTree
    {
        public void Evaluate(int? parentId, ElementType type)
        {
            if (parentId.HasValue==false && type != ElementType.TaskBag)
            {
                throw new Exception("Core of hierarchy has to be of Bag type");
            }
        }
    }
}
