using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using System;

namespace ProductivityTools.GetTask3.App.Commands
{
    public class GTaskApp
    {
        ITaskRepository _taskRepository;
        public GTaskApp(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void Add(string name)
        {
            Bag root = _taskRepository.GetStructure();
            DomainItem item = new DomainItem(name);
            root.Add(item);
        }

        public void AddBag(string bagName)
        {
            Bag root = _taskRepository.GetStructure();
            Bag bag = new Bag(bagName, BagType.GTask);
            root.Add(bag);
        }

        public void FinishTask(int TaskOrderId)
        {

        }
    }
}
