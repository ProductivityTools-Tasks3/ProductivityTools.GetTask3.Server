using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.DateTimeTools;
using ProductivityTools.GetTask3.CoreObjects.Tomato;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    public interface ITaskRepository : IRepository<Domain.Element, Infrastructure.Element>
    {
        Domain.Element GetStructure(int? root = null);
        List<Domain.Element> GetElements(List<int> elementids);
        //void AddItem(string name);


        List<Element> GetTaskBags(int? rootId);
    }

    public class TaskRepository : Repository<Domain.Element, Infrastructure.Element>, ITaskRepository
    {

        private readonly IDateTimePT _dateTimePT;

        public TaskRepository(TaskContext context, IMapper mapper, IDateTimePT dateTime) : base(context, mapper)
        {
            _dateTimePT = dateTime;
        }

        //pw: make it nice repository
        public Domain.Element GetStructure(int? rootId)
        {
            var result = GetInternal(rootId);
            if (result == null) return null;

            //we take all parents id to know where recursion should be called. Not all parents id will be retriven, as some maybe in wrong date, or have different status
            var parentIds = _taskContext.Element.Where(x => x.ParentId != null).Select(x => x.ParentId.Value).Distinct().ToList();
            List<Element> childElements = GetElementsInfrastructure(parentIds, result.ElementId);
            result.Elements = childElements;
            var r = _mapper.Map<Domain.Element>(result);
            return r;
        }

        public List<Domain.Element> GetElements(List<int> elementids)
        {
            var elements = _dbSet.Where(x => elementids.Contains(x.ElementId)).ToList();
            elements.ForEach(x => { _taskContext.Entry(x).State = EntityState.Detached; });
            var r = _mapper.Map<List<Domain.Element>>(elements);
            return r;
        }

        //private void FillWithtomato(List<Element> childElements, Tomato tomato)
        //{
        //    //pw: change it to mapping
        //    var tomatoelements = childElements.Where(x => tomato.Items.Select(item => item.ElementId).Contains(x.ElementId)).ToList();
        //    tomatoelements.ForEach(x =>
        //    {
        //        //pw: change it to mapper
        //        //x.Tomato = new Domain.Tomato { TomatoId = tomato.TomatoId, Created = tomato.Created, Finished = tomato.Finished };
        //    });
        //}

        private Infrastructure.Element GetInternal(int? id)
        {
            Element element = null;
            if (id == null)
            {
                element = _taskContext.Element.AsNoTracking().SingleOrDefault(x => x.ParentId == null);
            }
            else
            {
                element = _taskContext.Element.AsNoTracking().SingleOrDefault(x => x.ElementId == id);
            }
            return element;
        }


        public List<Element> GetTaskBags(int? rootId)
        {
            var result = new List<Element>();
            GetTaskBagsRecurse(result, rootId);
            return result;
        }

        private void GetTaskBagsRecurse(List<Element> elements, int? rootId)
        {
            var current = GetInternal(rootId);
            if (current == null)
            {
                throw new Exception("No task no bags found");
            }
            elements.Add(current);
            var childBags = _taskContext.Element.AsNoTracking().Where(l => l.ParentId == current.ElementId && l.Type == CoreObjects.ElementType.TaskBag).ToList();

            foreach (var childBag in childBags)
            {
                GetTaskBagsRecurse(elements, childBag.ElementId);
            }
        }

        private List<Element> GetElementsInfrastructure(List<int> allParentsIds, int? rootId = null)
        {
            List<Element> result = new List<Element>();
            var elements = _taskContext.Element.Where(l =>
            (l.ParentId == rootId && l.Status != Status.Finished && l.Start <= _dateTimePT.Now.AddDays(1).Date.AddSeconds(-1)) ||
            (l.ParentId == rootId && l.Status == Status.Finished && l.Finished.Value.Date == _dateTimePT.Now.Date)

            )
            .Include(x => x.TomatoElements).ThenInclude(i => i.Tomato)
            .ToList();

            foreach (var element in elements)
            {
                if (allParentsIds.Contains(element.ElementId))
                {
                    element.Elements = GetElementsInfrastructure(allParentsIds, element.ElementId);
                }
                result.Add(element);
            }
            return result;
        }
    }
}
