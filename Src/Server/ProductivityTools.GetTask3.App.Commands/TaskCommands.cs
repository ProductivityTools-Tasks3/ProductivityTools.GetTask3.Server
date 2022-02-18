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


        public TaskCommands(ITaskUnitOfWork taskUnitOfWork, IDateTimePT datetime)
        {
            _taskUnitOfWork = taskUnitOfWork;
            _dateTime = datetime;
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
            return e.ElementId;
        }

        public void Finish(int elementId)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            element.Finish(_dateTime.Now);
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
            Domain.Tomato curentTomato = _taskUnitOfWork.TomatoRepository.GetCurrent();
            if (curentTomato == null)
            {
                curentTomato = new Domain.Tomato();
                curentTomato.Status = Status.New;
            }

            List<Domain.Element> elements = _taskUnitOfWork.TaskRepository.GetElements(elementIds);
            elements.ForEach(x =>
            {
                x.AddToTomato(curentTomato);
                Infrastructure.Element d = _mapper.Map<Infrastructure.Element>(x);
                _taskUnitOfWork.TaskRepository.Update(d);
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
            Domain.Tomato curentTomato = _taskUnitOfWork.TomatoRepository.GetCurrent();
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
            Domain.Tomato tomato = _taskUnitOfWork.TomatoRepository.GetCurrent();
            tomato.Finish();
            Infrastructure.Tomato d = _mapper.Map<Infrastructure.Tomato>(tomato);
            _taskUnitOfWork.TomatoRepository.Update(d);

            if (finishAlsoTask)
            {
                List<Domain.Element> elements = _taskUnitOfWork.TaskRepository.GetElements(tomato.Elements.Select(x => x.ElementId).ToList());
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
            foreach (Domain.Element element in elements)
            {
                element.ChangeParent(target);
                _taskUnitOfWork.TaskRepository.Update(element);
            }

            _taskUnitOfWork.Commit();

        }

        public void Save(int parentId, int elementId, string name, string details, string detailsType)
        {

            Domain.Element element = _taskUnitOfWork.TaskRepository.GetElements(new List<int> { elementId }).Single(); ;

            element.Update(parentId, name, details, detailsType);
            Infrastructure.Element d = _mapper.Map<Infrastructure.Element>(element);
            _taskUnitOfWork.TaskRepository.Update(d);
            _taskUnitOfWork.Commit();
        }
    }
}
