using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    public class TaskRepository : Repository<Domain.Element>, ITaskRepository
    {
        public TaskRepository(TaskContext context) : base(context) { }

        //pw: make it nice repository
        public Domain.Element GetStructure(int? rootId = null)
        {
            
            var bag = new Domain.Element("GetTask3", Domain.ElementType.TaskBag);
            var elements = GetElements(rootId);
            bag.Elements = elements;


            //x.ForEach(i => bag.Components.Add(new Domain.Element(i.Name)));
            return bag;
        }

        private List<Element> GetElements(int? rootId = null)
        {
            List<Element> result = new List<Element>();
            var x = _taskContext.Elements.Where(l => l.BagId == rootId).ToList();
            for (int i = 0; i < x.Count(); i++)
            {
                Domain.Element element = new Domain.Element(x[i].ElementId, x[i].Type, x[i].Name, i, x[i].Status);
                if (element.Type == Domain.ElementType.TaskBag)
                {
                    element.Elements = GetElements(x[i].ElementId);
                }
                result.Add(element);
            }
            return result;
        }

        public void FinishTask(int id)
        {
            var element = _taskContext.Elements.First(x => x.ElementId == id);
            element.Status = Status.Finished;
        }

    }
}
