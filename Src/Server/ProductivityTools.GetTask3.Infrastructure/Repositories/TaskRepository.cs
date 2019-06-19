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
        public Domain.Element GetStructure(int? rootId)
        {

            var result = GetElement(rootId);
            if (result == null) return null;
            result.SetElements(GetElements(result.ElementId));
            return result;
        }

        private Element GetElement(int? id)
        {
            Element result = null;
            if (id == null)
            {
                result = _taskContext.Element.SingleOrDefault(x => x.ParentId == null);
            }
            else
            {
                result = _taskContext.Element.SingleOrDefault(x => x.ElementId == id);
            }
            return result;
        }

        private List<Element> GetElements(int? rootId = null)
        {
            List<Element> result = new List<Element>();
            var x = _taskContext.Element.Where(l => l.ParentId == rootId).ToList();
            for (int i = 0; i < x.Count(); i++)
            {
                Domain.Element element = new Domain.Element(x[i].ElementId, x[i].Type, x[i].Name, i, x[i].Status);
                if (element.Type == Domain.ElementType.TaskBag)
                {
                    element.SetElements(GetElements(x[i].ElementId));
                }
                result.Add(element);
            }
            return result;
        }

        public void FinishTask(int id)
        {
            var element = _taskContext.Element.First(x => x.ElementId == id);
            element.FinishTask();
        }

    }
}
