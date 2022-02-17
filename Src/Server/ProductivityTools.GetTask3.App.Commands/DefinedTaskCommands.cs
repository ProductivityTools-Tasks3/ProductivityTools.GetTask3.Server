using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Commands
{
    public interface IDefinedTaskCommands
    {
        void AddDefinedTask(int definedTaskId);
    }

    class DefinedTaskCommands : IDefinedTaskCommands
    {
        private readonly ITaskUnitOfWork _taskUnitOfWork;
        private readonly IDefinedTaskRepository _definedTaskRepository;
        // private readonly ITaskRepository _taskRepository;

        //IDateTimePT _dateTime;

        public DefinedTaskCommands(ITaskUnitOfWork taskUnitOfWork, IDefinedTaskRepository definedTaskRepository, ITaskRepository taskRepository)
        {
            _taskUnitOfWork = taskUnitOfWork;
            _definedTaskRepository = definedTaskRepository;

        }

        public void AddDefinedTask(int definedTaskId)
        {
            var definedTaskGroup = _definedTaskRepository.GetWithDetails(definedTaskId);
            Domain.Element e = _taskUnitOfWork.TaskRepository.Get(definedTaskGroup.BagId);
            foreach (var definedElement in definedTaskGroup.Items)
            {
                Domain.Element newElement = new Domain.Element(definedElement.Name,string.Empty,string.Empty, CoreObjects.ElementType.Task, e.ElementId, definedTaskGroup.Name);
                //newElement.Update(CoreObjects.ElementType.Task);

                _taskUnitOfWork.TaskRepository.Add(newElement);
            }
            _taskUnitOfWork.Commit();
        }
    }
}
