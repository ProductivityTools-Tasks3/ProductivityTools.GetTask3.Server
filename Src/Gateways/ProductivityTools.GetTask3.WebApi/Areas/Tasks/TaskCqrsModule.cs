using ProductivityTools.GetTask3.CqrsController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.WebApi.Areas.Tasks
{
    public class TaskCqrsModule: CqrsModule
    {
        public TaskCqrsModule() : base(Areas.Task)
        {
            WireUpQuery<TaskDetailsRequest,>();
        }
    }
}
