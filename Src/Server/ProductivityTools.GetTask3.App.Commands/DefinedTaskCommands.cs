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
            var definedTaskGroup=_definedTaskRepository.Get(definedTaskId);
            Domain.Element e =_taskUnitOfWork.TaskRepository.Get(definedTaskGroup.BagId);
            foreach (var definedElement in definedTaskGroup.Items)
            {
                Domain.Element newlement = new Domain.Element(definedElement.Name, CoreObjects.ElementType.Task);
                e.Elements.Add(newlement);
            }
            _taskUnitOfWork.Commit();
        }
    }
}
