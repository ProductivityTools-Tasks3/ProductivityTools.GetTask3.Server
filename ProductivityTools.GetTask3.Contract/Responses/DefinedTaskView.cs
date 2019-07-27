using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Contract.Responses
{
    public class DefinedTaskView
    {
        public List<DefinedTaskGroup> DefinedTasks { get; set; }

        public DefinedTaskView()
        {
            this.DefinedTasks = new List<DefinedTaskGroup>();
        }
    }
}
