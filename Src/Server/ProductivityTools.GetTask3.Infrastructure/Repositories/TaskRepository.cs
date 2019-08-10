using AutoMapper;
using ProductivityTools.DateTimeTools;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    public interface ITaskRepository : IRepository<Domain.Element>
    {
        Domain.Element GetStructure(int? root = null);
        //void AddItem(string name);

        Element Get(int? id);
        List<Element> GetTaskBags(int? rootId);
    }

    public class TaskRepository : Repository<Domain.Element>, ITaskRepository
    {
        private readonly IMapper _mapper;
        private readonly IDateTimePT _dateTimePT;

        public TaskRepository(TaskContext context, IMapper mapper, IDateTimePT dateTime) : base(context)
        {
            _mapper = mapper;
            _dateTimePT = dateTime;
        }

        //pw: make it nice repository
        public Domain.Element GetStructure(int? rootId)
        {
            var result = Get(rootId);
            if (result == null) return null;
            result.SetElements(GetElements(result.ElementId));
            return result;
        }

        public Element Get(int? id)
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

        public List<Element> GetTaskBags(int? rootId)
        {
            var result = new List<Element>();
            GetTaskBagsRecurse(result, rootId);
            return result;
        }

        private void GetTaskBagsRecurse(List<Element> elements, int? rootId)
        {
            var current = Get(rootId);
            if (current == null)
            {
                throw new Exception("No task no bags found");
            }
            elements.Add(current);
            var childBags = _taskContext.Element.Where(l => l.ParentId == current.ElementId && l.Type == CoreObjects.ElementType.TaskBag).ToList();

            foreach (var childBag in childBags)
            {
                GetTaskBagsRecurse(elements, childBag.ElementId);
            }
        }

        private void GetTomato()
        {
            //_taskContext.
        }

        private List<Element> GetElements(int? rootId = null)
        {
            List<Element> result = new List<Element>();
            var x = _taskContext.Element.Where(l => 
            (l.ParentId == rootId && l.Status != Status.Finished && l.Start<=_dateTimePT.Now.AddDays(1).Date.AddSeconds(-1)) ||
            (l.ParentId == rootId && l.Status == Status.Finished && l.Finished.Value.Date == _dateTimePT.Now.Date)

            ).ToList();
            for (int i = 0; i < x.Count(); i++)
            {
                Domain.Element element = x[i];// new Domain.Element(x[i].ElementId, x[i].Type, x[i].Name, i, x[i].Status);
                if (element.Type == CoreObjects.ElementType.TaskBag)
                {
                    element.SetElements(GetElements(x[i].ElementId));
                }
                result.Add(element);
            }
            return result;
        }

        //public void FinishTask(int id)
        //{
        //    var element = _taskContext.Element.First(x => x.ElementId == id);
        //    element.Finish();
        //}
    }
}
