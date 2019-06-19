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

        public void Add(string name, int? bagId)
        {
            AddElement(name, Domain.ElementType.Task, bagId);
        }

        public void AddBag(string bagName, int? bagId)
        {
            AddElement(bagName, Domain.ElementType.TaskBag, bagId);
        }

        private void AddElement(string name, Domain.ElementType type, int? parentId)
        {

            Domain.Element e = new Domain.Element(name, type);
            e.Update(parentId, type);

            _taskUnitOfWork.TaskRepository.Add(e);
            _taskUnitOfWork.Commit();
        }

        public void Finish(int orderId, int? bagId)
        {
            var element = _taskUnitOfWork.TaskRepository.GetStructure(bagId);
            Domain.Element elemnet = element.Elements.Find(x => (x as Domain.Element).OrderId == orderId) as Domain.Element;
            _taskUnitOfWork.TaskRepository.FinishTask(elemnet.ElementId);

            _taskUnitOfWork.Commit();
        }
    }
}
