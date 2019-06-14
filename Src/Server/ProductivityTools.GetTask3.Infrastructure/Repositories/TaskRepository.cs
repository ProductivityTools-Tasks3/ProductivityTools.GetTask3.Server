using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    public class TaskRepository : Repository<Element>, ITaskRepository
    {
        public TaskRepository(TaskContext context) : base(context) { }

        //pw: make it nice repository
        public Bag GetStructure(int? rootId = null)
        {
            var x = _taskContext.Elements.Where(q=>q.BagId==rootId).ToList();
            var bag = new Bag("GetTask3", BagType.GTask);
            for (int i = 0; i < x.Count; i++)
            {
                Domain.Component element = null;
                if (x[i].Type==ElementType.Bag)
                {
                    element = new Domain.Bag(x[i].Name, BagType.GTask);
                }
                if (x[i].Type==ElementType.Task)
                {
                    element = new Domain.Task(x[i].ElementId, x[i].Name, i, x[i].Status);
                }
                bag.Components.Add(element);
            }
            //x.ForEach(i => bag.Components.Add(new Domain.Element(i.Name)));
            return bag;
        }

        public void FinishTask(int id)
        {
            var element=_taskContext.Elements.First(x => x.ElementId == id);
            element.Status = Status.Finished;
        }

    }
}
