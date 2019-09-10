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

        //pw: this is poor, should be rewritten to take whole element with tomato
        //private Domain.Tomato GetTomato(List<int> elementsId)
        //{
        //    var tomatoes = from t in _taskContext.TomatoItem
        //                   where elementsId.Contains(t.ElementId)
        //                   select t;

        //    //pw: remove when  many-to-many
        //    //var xxx = (from t in _taskContext.Tomato
        //    //           join ti in _taskContext.TomatoItem
        //    //           on t.TomatoId equals ti.TomatoId


        //    //           where elementsId.Contains(ti.ElementId)
        //    //           select t).Include(x => x.Items).ToList();


        //    var z = _taskContext.Tomato.Include(t => t.Items).SingleOrDefault(t => t.Status != CoreObjects.Tomato.Status.Finished);
        //    return z;
        //}

        //pw: make it nice repository
        public Domain.Element GetStructure(int? rootId)
        {
            var result = GetInternal(rootId);
            if (result == null) return null;

            List<Element> childElements = GetElementsInfrastructure(result.ElementId);
            //get tomato
            //pw: change it to mapping
            //Tomato tomato = GetTomato(childElements.Select(x => x.ElementId).ToList());

            //if (tomato != null)
            //{
            //    FillWithtomato(childElements, tomato);
            //}
            result.Elements = childElements;
            var r= _mapper.Map<Domain.Element>(result);
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

        private List<Element> GetElementsInfrastructure(int? rootId = null)
        {
            List<Element> result = new List<Element>();
            var elements = _taskContext.Element.Where(l =>
            (l.ParentId == rootId && l.Status != Status.Finished && l.Start <= _dateTimePT.Now.AddDays(1).Date.AddSeconds(-1)) ||
            (l.ParentId == rootId && l.Status == Status.Finished && l.Finished.Value.Date == _dateTimePT.Now.Date)

            )
            .Include(x => x.TomatoElements).ThenInclude(i => i.Tomato)
            .ToList();

            for (int i = 0; i < elements.Count(); i++)
            {
                Element element = elements[i];// new Domain.Element(x[i].ElementId, x[i].Type, x[i].Name, i, x[i].Status);
                if (element.Type == CoreObjects.ElementType.TaskBag)
                {
                    element.Elements = GetElementsInfrastructure(elements[i].ElementId);
                }
                result.Add(element);
            }
            return result;
        }

        private List<Domain.Element> GetElements(int? rootId = null)
        {
            var result = GetElementsInfrastructure(rootId);
            var r = _mapper.Map<List<Domain.Element>>(result);
            return r;
        }
    }
}
