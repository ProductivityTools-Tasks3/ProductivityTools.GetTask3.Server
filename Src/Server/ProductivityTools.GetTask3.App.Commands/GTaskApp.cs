using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;

namespace ProductivityTools.GetTask3.App.Commands
{
    public class GTaskApp : IGTaskApp
    {
        ITaskUnitOfWork _taskUnitOfWork;

        public GTaskApp(ITaskUnitOfWork taskUnitOfWork)
        {
            _taskUnitOfWork = taskUnitOfWork;
        }

        public void Add(string name, int? bagId = null)
        {
            AddElement(name, ElementType.Task, bagId);
        }

        public void AddBag(string bagName, int? bagId = null)
        {
            AddElement(bagName, ElementType.Bag, bagId);
        }

        private void AddElement(string name, ElementType type, int? bagId = null)
        {
            Infrastructure.Element e = new Infrastructure.Element();
            e.Name = name;
            e.Type = type;
            e.Created = DateTime.Now;
            e.Deadline = DateTime.Now.AddDays(1);
            e.Status = Status.New;
            e.BagId = bagId;


            _taskUnitOfWork.TaskRepository.Add(e);
            _taskUnitOfWork.Commit();
        }

        public void Finish(int orderId, int? bagId = null)
        {
            var elements = _taskUnitOfWork.TaskRepository.GetStructure();
            Domain.Task elemnet = elements.Components.Find(x => (x as Domain.Task).OrderId == orderId) as Domain.Task;
            _taskUnitOfWork.TaskRepository.FinishTask(elemnet.Id);

            _taskUnitOfWork.Commit();
        }
    }
}
