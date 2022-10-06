using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductivityTools.DateTimeTools;
using ProductivityTools.GetTask3.CoreObjects.Tomato;
using ProductivityTools.GetTask3.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    public interface ITaskRepository : IRepository<Infrastructure.Element>
    {
        Infrastructure.Element GetStructure(string filter, int? root = null);
        Infrastructure.Element GetNode(string filter, int? node);
        List<Infrastructure.Element> GetElements(List<int> elementids);
        //void AddItem(string name);


        List<Element> GetTaskBags(int? rootId);
    }

    public class TaskRepository : Repository<Infrastructure.Element>, ITaskRepository
    {

        private readonly IDateTimePT _dateTimePT;

        public TaskRepository(TaskContext context, IMapper mapper, IDateTimePT dateTime) : base(context, mapper)
        {
            _dateTimePT = dateTime;
        }

        public Infrastructure.Element GetNode(string filter, int? nodeId)
        {
            var result = GetInternal(nodeId);
            result.Elements = GetChildElements(filter,result.ElementId);
            //var r = _mapper.Map<Domain.Element>(result);
            return result;
        }

        //pw: make it nice repository
        public Infrastructure.Element GetStructure(string filter, int? rootId)
        {
            var result = GetInternal(rootId);
            if (result == null) return null;

            //we take all parents id to know where recursion should be called. Not all parents id will be retriven, as some maybe in wrong date, or have different status
            var parentIds = _taskContext.Element.Where(x => x.ParentId != null).Select(x => x.ParentId.Value).Distinct().ToList();
            List<Element> childElements = GetElementsInfrastructure(parentIds, filter, result.ElementId);
            result.Elements = childElements;
            //var r = _mapper.Map<Domain.Element>(result);
            return result;
        }

        public List<Infrastructure.Element> GetElements(List<int> elementids)
        {
            var elements = _dbSet.Where(x => elementids.Contains(x.ElementId)).ToList();
            elements.ForEach(x => { _taskContext.Entry(x).State = EntityState.Detached; });
            //var r = _mapper.Map<List<Domain.Element>>(elements);
            return elements;
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
                var x = _taskContext.Element.AsNoTracking().ToList();
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

        private List<Element> GetChildElements(string filter, int? rootId)
        {
            if (filter == SearchConditions.GetTodaysList)
            {
                var elements = _taskContext.Element.Where(l => l.Status != Status.Deleted &&

                        (l.ParentId == rootId && l.Status != Status.Finished && l.Initialization <= _dateTimePT.Now.AddDays(1).Date.AddSeconds(-1)) ||
                        (l.ParentId == rootId && l.Status == Status.Finished && l.Finished.Value.Date == _dateTimePT.Now.Date))
                   .Include(x => x.TomatoElements).ThenInclude(i => i.Tomato)
               .ToList();
                return elements;
            }

            if (filter == SearchConditions.GetFinshedThisWeek)
            {
                var elements = _taskContext.Element.Where(l => l.Status != Status.Deleted &&
                 (
                     (l.ParentId == rootId && l.Status != Status.Finished && l.Initialization <= DateTime.Now.AddDays(1).Date.AddSeconds(-1)) ||
                     (l.ParentId == rootId && l.Status == Status.Finished && DateTime.Now.AddDays(-1 * DateTime.Now.DayOfYear).Date < l.Finished.Value.Date)
                 )).ToList();
                return elements;
            }
            throw new Exception();
        }

        private List<Element> GetElementsInfrastructure(List<int> allParentsIds, string filter, int? rootId = null)
        {
            List<Element> result = new List<Element>();
            var elements = GetChildElements(filter,rootId);

            foreach (var element in elements)
            {
                if (allParentsIds.Contains(element.ElementId))
                {
                    element.Elements = GetElementsInfrastructure(allParentsIds, filter, element.ElementId);
                }
                result.Add(element);
            }
            return result;
        }
    }
}
