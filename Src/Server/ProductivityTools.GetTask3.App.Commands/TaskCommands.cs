using AutoMapper;
using ProductivityTools.DateTimeTools;
using ProductivityTools.GetTask3.CoreObjects.Tomato;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using ProductivityTools.GetTask3.SignalRHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ProductivityTools.GetTask3.App.Commands
{
    public interface IGTaskCommands
    {
        int Add(string name, string details, string detailsType, int? bagId, bool finished);
        void AddBag(string bagName, string details, string detailsType, int? bagId);
        void Finish(int elementId);
        void Start(int elementId);
        void Undone(int elementId);
        void Delay(int elementId, DateTime dateTime);
        void Delete(int elementId);
        void AddToTomato(List<int> elementIds);
        void AddToTomato(string name, string details, int parentId);
        void FinishTomato(bool finishAlsoTasks);
        void Move(int[] elementIds, int target);

        void Save(int parentId, int elementId, string name, string details, string detailsType);
    }


    public class TaskCommands : IGTaskCommands
    {
        ITaskUnitOfWork _taskUnitOfWork;
        IDateTimePT _dateTime;
        protected readonly IMapper _mapper;


        public TaskCommands(ITaskUnitOfWork taskUnitOfWork, IDateTimePT datetime, IMapper mapper)
        {
            _taskUnitOfWork = taskUnitOfWork;
            _dateTime = datetime;
            _mapper = mapper;
        }

        public int Add(string name, string details, string detailsType, int? bagId, bool finished)
        {
            var r = AddElement(name, details, detailsType, CoreObjects.ElementType.Task, bagId, finished);
            return r;
        }

        public void AddBag(string bagName, string detailsType, string details, int? bagId)
        {
            AddElement(bagName, details, detailsType, CoreObjects.ElementType.TaskBag, bagId, false);
        }

        private int AddElement(string name, string details, string detailsType, CoreObjects.ElementType type, int? parentId, bool finished)
        {

            Domain.Element e = new Domain.Element(name, details, detailsType, type, parentId);

            if (finished)
            {
                e.Finish(_dateTime.Now);
            }

            Infrastructure.Element d = _mapper.Map<Infrastructure.Element>(e);
            _taskUnitOfWork.TaskRepository.Add(d);
            _taskUnitOfWork.Commit();
            return d.ElementId;
        }

        public void Finish(int elementId)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            var domain = _mapper.Map<Domain.Element>(element);
            domain.Finish(_dateTime.Now);
            element = _mapper.Map<Infrastructure.Element>(domain);
            _taskUnitOfWork.TaskRepository.Update(element);
            _taskUnitOfWork.Commit();
        }

        public void Start(int elementId)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            Domain.Element d = _mapper.Map<Domain.Element>(element);

            d.Start(_dateTime.Now);
            element = _mapper.Map<Infrastructure.Element>(d);

            _taskUnitOfWork.TaskRepository.Update(element);
            _taskUnitOfWork.Commit();
        }

        public void Undone(int elementId)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            Domain.Element d = _mapper.Map<Domain.Element>(element);
            d.Undone();
            element = _mapper.Map<Infrastructure.Element>(d);
            _taskUnitOfWork.TaskRepository.Update(element);
            _taskUnitOfWork.Commit();
        }

        public void Delay(int elementId, DateTime initializationDate)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            Domain.Element d = _mapper.Map<Domain.Element>(element);

            d.Delay(initializationDate);
            element = _mapper.Map<Infrastructure.Element>(d);

            _taskUnitOfWork.TaskRepository.Update(element);
            _taskUnitOfWork.Commit();
        }

        public void Delete(int elementId)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            Domain.Element d = _mapper.Map<Domain.Element>(element);

            d.Delete();
            element = _mapper.Map<Infrastructure.Element>(d);

            _taskUnitOfWork.TaskRepository.Update(element);
            _taskUnitOfWork.Commit();
        }

        public void AddToTomato(List<int> elementIds)
        {
            Infrastructure.Tomato curentTomato = _taskUnitOfWork.TomatoRepository.GetCurrent();
            if (curentTomato == null)
            {
                curentTomato = new Infrastructure.Tomato();
                curentTomato.Status = Status.New;
            }

            List<Infrastructure.Element> elements = _taskUnitOfWork.TaskRepository.GetElements(elementIds);
            elements.ForEach(x =>
            {
                //I removed this functionality for now
                //x.AddToTomato(curentTomato);
                //Infrastructure.Element d = _mapper.Map<Infrastructure.Element>(x);
                //_taskUnitOfWork.TaskRepository.Update(d);
            });
            _taskUnitOfWork.Commit();


            //pw: move to repository
            //var tomatoItems = elementIds.ToList().Select(x => new Infrastructure.TomatoElement() { ElementId = x }).ToList();
            //var tomato = new Infrastructure.Tomato() { Status = CoreObjects.Tomato.Status.New, Items = tomatoItems };
            //_taskUnitOfWork.TomatoRepository.Add(tomato);
            //_taskUnitOfWork.Commit();
        }

        public void AddToTomato(string name, string details, int parentId)
        {
            Infrastructure.Tomato infrastructure = _taskUnitOfWork.TomatoRepository.GetCurrent();
            var curentTomato = _mapper.Map<Domain.Tomato>(infrastructure);

            if (curentTomato == null)
            {
                curentTomato = new Domain.Tomato();
                curentTomato.Status = Status.New;
            }

            var element = new Domain.Element(name, details, "", CoreObjects.ElementType.Task, parentId);
            element.AddToTomato(curentTomato);
            Infrastructure.Element d = _mapper.Map<Infrastructure.Element>(element);

            _taskUnitOfWork.TaskRepository.Add(d);
            // _taskUnitOfWork.TomatoRepository.Update(curentTomato);
            _taskUnitOfWork.Commit();
        }

        public void FinishTomato(bool finishAlsoTask)
        {
            Infrastructure.Tomato itomato = _taskUnitOfWork.TomatoRepository.GetCurrent();
            var tomato = _mapper.Map<Domain.Tomato>(itomato);

            tomato.Finish();
            Infrastructure.Tomato d = _mapper.Map<Infrastructure.Tomato>(tomato);
            _taskUnitOfWork.TomatoRepository.Update(d);

            if (finishAlsoTask)
            {
                List<Infrastructure.Element> ielements = _taskUnitOfWork.TaskRepository.GetElements(tomato.Elements.Select(x => x.ElementId).ToList());
                List<Domain.Element> elements = _mapper.Map<List<Domain.Element>>(ielements);
                //pw: chage it
                elements.ForEach(x =>
                {
                    x.Finish(DateTime.Now);
                    Infrastructure.Element d = _mapper.Map<Infrastructure.Element>(x);
                    _taskUnitOfWork.TaskRepository.Update(d);
                });
            }

            _taskUnitOfWork.Commit();
        }

        public void Move(int[] elementIds, int target)
        {
            var elements = _taskUnitOfWork.TaskRepository.GetElements(elementIds.ToList());
            foreach (Infrastructure.Element element in elements)
            {
                var domain = _mapper.Map<Domain.Element>(element);
                domain.ChangeParent(target);
                var dbupdate = _mapper.Map<Infrastructure.Element>(domain);
                _taskUnitOfWork.TaskRepository.Update(dbupdate);
            }

            _taskUnitOfWork.Commit();

        }

        public void Save(int parentId, int elementId, string name, string details, string detailsType)
        {

            Infrastructure.Element element = _taskUnitOfWork.TaskRepository.GetElements(new List<int> { elementId }).Single(); ;
            Domain.Element el = _mapper.Map<Domain.Element>(element);
            el.Update(parentId, name, details, detailsType);
            element = _mapper.Map<Infrastructure.Element>(el);
            Infrastructure.Element d = _mapper.Map<Infrastructure.Element>(element);
            _taskUnitOfWork.TaskRepository.Update(d);
            _taskUnitOfWork.Commit();
        }
    }
}
