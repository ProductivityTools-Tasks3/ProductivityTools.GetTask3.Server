using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Queries
{
    public class TaskView : ItemView
    {
        public DateTime Deadline { get; set; }
        public DateTime Finished { get; set; }

    }
}
