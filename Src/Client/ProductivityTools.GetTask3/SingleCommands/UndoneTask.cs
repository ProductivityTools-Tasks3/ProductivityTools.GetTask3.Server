using ProductivityTools.GetTask3.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.SingleCommands
{
    [Cmdlet("Undone", "Task3")]
    public class UndoneTask : SingleCommandsBase
    {
        [Parameter(HelpMessage = "Id or Ids space separated which should be undone", Position = 0)]
        public string Id { get; set; }

        App.Task TaskStructure { get; set; }

        public UndoneTask()
        {
            TaskStructure = new App.Task(this);
        }

        protected override void ProcessRecord()
        {
            int[] orderIds = Id.Split(' ').Select(x => int.Parse(x)).ToArray();
            foreach (var id in orderIds)
            {
                TaskStructure.Undone(id);
            }

            base.ProcessRecord();
        }
    }
}
