using ProductivityTools.DateTimeTools;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
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
        void Undone(int elementId);
        void Delay(int elementId, DateTime dateTime);
        void AddToTomato(int[] elementIds);
        void FinishTomato();
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
            Domain.Element e = new Domain.Element(name, type);
            e.Update(parentId, type);

            _taskUnitOfWork.TaskRepository.Add(e);
            _taskUnitOfWork.Commit();
        }

        public void Finish(int elementId)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            element.Finish(_dateTime.Now);
            _taskUnitOfWork.TaskRepository.Update(element, element.ElementId);
            _taskUnitOfWork.Commit();
        }

        public void Undone(int elementId)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            element.Undone();
            _taskUnitOfWork.Commit();
        }

        public void Delay(int elementId, DateTime startDate)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            element.Delay(startDate);
            _taskUnitOfWork.TaskRepository.Update(element, element.ElementId);
            _taskUnitOfWork.Commit();
        }

        public void AddToTomato(int[] elementIds)
        {
            Domain.Tomato curentTomato = _taskUnitOfWork.TomatoRepository.GetCurrent();
            if (curentTomato == null)
            {
                curentTomato = new Domain.Tomato();
                _taskUnitOfWork.TomatoRepository.Add(curentTomato);
            }

            List<Domain.Element> elements = _taskUnitOfWork.TaskRepository.GetElements(elementIds);
            elements.ForEach(x =>
            {
                x.AddToTomato(curentTomato);
                _taskUnitOfWork.TaskRepository.Update(x,x.ElementId);
             });
            _taskUnitOfWork.Commit();
            //pw: move to repository
            //var tomatoItems = elementIds.ToList().Select(x => new Infrastructure.TomatoElement() { ElementId = x }).ToList();
            //var tomato = new Infrastructure.Tomato() { Status = CoreObjects.Tomato.Status.New, Items = tomatoItems };
            //_taskUnitOfWork.TomatoRepository.Add(tomato);
            //_taskUnitOfWork.Commit();
        }

        public void FinishTomato()
        {
            _taskUnitOfWork.TomatoRepository.Finish();
            _taskUnitOfWork.Commit();
        }
    }
}
