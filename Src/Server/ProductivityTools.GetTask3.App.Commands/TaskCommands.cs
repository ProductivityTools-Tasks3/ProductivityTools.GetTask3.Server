using ProductivityTools.DateTimeTools;
using ProductivityTools.GetTask3.CoreObjects.Tomato;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using ProductivityTools.GetTask3.SignalRHubs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductivityTools.GetTask3.App.Commands
{
    public interface IGTaskCommands
    {
        void Add(string name, int? bagId);
        void AddBag(string bagName, int? bagId);
        void Finish(int elementId);
        void Start(int elementId);
        void Undone(int elementId);
        void Delay(int elementId, DateTime dateTime);
        void AddToTomato(List<int> elementIds);
        void AddToTomato(string name, int parentId);
        void FinishTomato(bool finishAlsoTasks);
        void Move(int[] elementIds, int target);
    }


    public class TaskCommands : IGTaskCommands
    {
        ITaskUnitOfWork _taskUnitOfWork;
        IDateTimePT _dateTime;


        public TaskCommands(ITaskUnitOfWork taskUnitOfWork, IDateTimePT datetime)
        {
            _taskUnitOfWork = taskUnitOfWork;
            _dateTime = datetime;
        }

        public void Add(string name, int? bagId)
        {
            AddElement(name, CoreObjects.ElementType.Task, bagId);
        }

        public void AddBag(string bagName, int? bagId)
        {
            AddElement(bagName, CoreObjects.ElementType.TaskBag, bagId);
        }

        private void AddElement(string name, CoreObjects.ElementType type, int? parentId)
        {
            Domain.Element e = new Domain.Element(name, type, parentId);

            _taskUnitOfWork.TaskRepository.Add(e);
            _taskUnitOfWork.Commit();
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
            element.Finish(_dateTime.Now);
            _taskUnitOfWork.TaskRepository.Update(element);
            _taskUnitOfWork.Commit();
        }

        public void Undone(int elementId)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            element.Undone();
            _taskUnitOfWork.Commit();
        }

        public void Delay(int elementId, DateTime initializationDate)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            element.Delay(initializationDate);
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
                _taskUnitOfWork.TaskRepository.Update(x);
            });
            _taskUnitOfWork.Commit();


            //pw: move to repository
            //var tomatoItems = elementIds.ToList().Select(x => new Infrastructure.TomatoElement() { ElementId = x }).ToList();
            //var tomato = new Infrastructure.Tomato() { Status = CoreObjects.Tomato.Status.New, Items = tomatoItems };
            //_taskUnitOfWork.TomatoRepository.Add(tomato);
            //_taskUnitOfWork.Commit();
        }

        public void AddToTomato(string name, int parentId)
        {
            Domain.Tomato curentTomato = _taskUnitOfWork.TomatoRepository.GetCurrent();
            if (curentTomato == null)
            {
                curentTomato = new Domain.Tomato();
                curentTomato.Status = Status.New;
            }

            var element = new Domain.Element(name, CoreObjects.ElementType.Task, parentId);
            element.AddToTomato(curentTomato);

            _taskUnitOfWork.TaskRepository.Add(element);
            // _taskUnitOfWork.TomatoRepository.Update(curentTomato);
            _taskUnitOfWork.Commit();
        }

        public void FinishTomato(bool finishAlsoTask)
        {
            Domain.Tomato tomato = _taskUnitOfWork.TomatoRepository.GetCurrent();
            tomato.Finish();
            _taskUnitOfWork.TomatoRepository.Update(tomato);

            if (finishAlsoTask)
            {
                List<Domain.Element> elements = _taskUnitOfWork.TaskRepository.GetElements(tomato.Elements.Select(x => x.ElementId).ToList());
                //pw: chage it
                elements.ForEach(x =>
                {
                    x.Finish(DateTime.Now);
                    _taskUnitOfWork.TaskRepository.Update(x);
                });
            }

            _taskUnitOfWork.Commit();
        }

        public void Move(int[] elementIds, int target)
        {
            var elements=_taskUnitOfWork.TaskRepository.GetElements(elementIds.ToList());
            foreach (Domain.Element element in elements)
            {
                element.ChangeParent(target);
                _taskUnitOfWork.TaskRepository.Update(element);
            }

            _taskUnitOfWork.Commit();

        }
    }
}
