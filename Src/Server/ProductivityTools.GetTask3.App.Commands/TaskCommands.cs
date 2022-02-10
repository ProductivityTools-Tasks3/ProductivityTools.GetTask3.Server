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
        void Add(string name, string details, int? bagId, bool finished);
        void AddBag(string bagName, string details, int? bagId);
        void Finish(int elementId);
        void Start(int elementId);
        void Undone(int elementId);
        void Delay(int elementId, DateTime dateTime);
        void Delete(int elementId);
        void AddToTomato(List<int> elementIds);
        void AddToTomato(string name, string details, int parentId);
        void FinishTomato(bool finishAlsoTasks);
        void Move(int[] elementIds, int target);
         
        void Save(int parentId, int elementId, string name);
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

        public void Add(string name, string details, int? bagId, bool finished)
        {
            AddElement(name, details, CoreObjects.ElementType.Task, bagId, finished);
        }

        public void AddBag(string bagName, string details, int? bagId)
        {
            AddElement(bagName, details, CoreObjects.ElementType.TaskBag, bagId, false);
        }

        private void AddElement(string name, string details, CoreObjects.ElementType type, int? parentId, bool finished)
        {

            Domain.Element e = new Domain.Element(name, details, type, parentId);

            if (finished)
            {
                e.Finish(_dateTime.Now);
            }

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
            element.Start(_dateTime.Now);
            _taskUnitOfWork.TaskRepository.Update(element);
            _taskUnitOfWork.Commit();
        }

        public void Undone(int elementId)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            element.Undone();
            _taskUnitOfWork.TaskRepository.Update(element);
            _taskUnitOfWork.Commit();
        }

        public void Delay(int elementId, DateTime initializationDate)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            element.Delay(initializationDate);
            _taskUnitOfWork.TaskRepository.Update(element);
            _taskUnitOfWork.Commit();
        }

        public void Delete(int elementId)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            element.Delete();
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

        public void AddToTomato(string name, string details, int parentId)
        {
            Domain.Tomato curentTomato = _taskUnitOfWork.TomatoRepository.GetCurrent();
            if (curentTomato == null)
            {
                curentTomato = new Domain.Tomato();
                curentTomato.Status = Status.New;
            }

            var element = new Domain.Element(name, details, CoreObjects.ElementType.Task, parentId);
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
            var elements = _taskUnitOfWork.TaskRepository.GetElements(elementIds.ToList());
            foreach (Domain.Element element in elements)
            {
                element.ChangeParent(target);
                _taskUnitOfWork.TaskRepository.Update(element);
            }

            _taskUnitOfWork.Commit();

        }

        public void Save(int parentId, int elementId, string name)
        {

            Domain.Element element = _taskUnitOfWork.TaskRepository.GetElements(new List<int> { elementId }).Single(); ;

            element.Update(parentId, name);
            _taskUnitOfWork.TaskRepository.Update(element);
            _taskUnitOfWork.Commit();
        }
    }
}
