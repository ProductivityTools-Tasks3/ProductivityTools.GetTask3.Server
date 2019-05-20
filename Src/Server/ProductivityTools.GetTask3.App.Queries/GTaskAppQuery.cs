using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using System;

namespace ProductivityTools.GetTask3.App.Queries
{
    public class GTaskAppQuery : IGTaskAppQuery
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
            bag.Components.ForEach(x => st.Items.Add(new ItemView() { Name = x.Name }));
            return st;
        }
    }
}
