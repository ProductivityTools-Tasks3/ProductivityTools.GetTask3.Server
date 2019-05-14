using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.MsSql;
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

        //pw:change it to handlers
        public Bag GetTaskList()
        {
            Bag bag = _taskRepository.GetStructure();
            return bag;
        }

        public void Add(string name)
        {
            Bag root = _taskRepository.GetStructure();
            Item item = new Item();
            item.Name = name;
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
