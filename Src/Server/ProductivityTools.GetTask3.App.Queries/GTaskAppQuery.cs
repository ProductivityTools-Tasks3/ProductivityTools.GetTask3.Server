using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using System;

namespace ProductivityTools.GetTask3.App.Queries
{
    public class GTaskAppQuery
    {

        ITaskRepository _taskRepository;
        public GTaskAppQuery(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        //pw:change it to handlers
        public StructureView GetTaskList()
        {
            Bag bag = _taskRepository.GetStructure();
            StructureView st = new StructureView();

            return st;
        }
    }
}
