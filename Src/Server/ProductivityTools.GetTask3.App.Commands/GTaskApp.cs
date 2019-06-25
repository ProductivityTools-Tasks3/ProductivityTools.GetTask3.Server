using ProductivityTools.DateTimeTools;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;

namespace ProductivityTools.GetTask3.App.Commands
{
    public class GTaskApp : IGTaskApp
    {
        ITaskUnitOfWork _taskUnitOfWork;
        IDateTimePT _dateTime;

        public GTaskApp(ITaskUnitOfWork taskUnitOfWork, IDateTimePT datetime)
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
            _taskUnitOfWork.Commit();
        }

        public void Undone(int elementId)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            element.Undone(_dateTime.Now);
            _taskUnitOfWork.Commit();
        }

        public void Delay(int elementId, DateTime startDate)
        {
            var element = _taskUnitOfWork.TaskRepository.Get(elementId);
            element.Delay(startDate);
            _taskUnitOfWork.Commit();
        }
    }
}
