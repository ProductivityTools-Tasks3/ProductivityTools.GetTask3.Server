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
            AddElement(name, Domain.ElementType.Task, bagId);
        }

        public void AddBag(string bagName, int? bagId = null)
        {
            AddElement(bagName, Domain.ElementType.TaskBag, bagId);
        }

        private void AddElement(string name, Domain.ElementType type, int? bagId = null)
        {
            Domain.Element e = new Domain.Element(name, type);
            e.Name = name;
            e.Type = type;
            e.Created = DateTime.Now;
            e.Deadline = DateTime.Now.AddDays(1);
            e.Status = Status.New;


            _taskUnitOfWork.TaskRepository.Add(e);
            _taskUnitOfWork.Commit();
        }

        public void Finish(int orderId, int? bagId = null)
        {
            var element = _taskUnitOfWork.TaskRepository.GetStructure();
            Domain.Element elemnet = element.Elements.Find(x => (x as Domain.Element).OrderId == orderId) as Domain.Element;
            _taskUnitOfWork.TaskRepository.FinishTask(elemnet.ElementId);

            _taskUnitOfWork.Commit();
        }
    }
}
