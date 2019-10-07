using ProductivityTools.GetTask3.App;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.SingleCommands
{
    [Cmdlet("Finish", "Task3")]
    public class FinishTask : SingleCommandsBase
    {
        [Parameter(HelpMessage = "Id or Ids space separated which should be finished", Position = 0)]
        public string Id { get; set; }

        App.Task TaskStructure { get; set; }

        public FinishTask()
        {
            
            TaskStructure = TaskStructureFactory.Get(this);
        }

        protected override void ProcessRecord()
        {
            int[] orderIds = Id.GetIds();
            foreach (var id in orderIds)
            {
                TaskStructure.Finish(id);
            }

            base.ProcessRecord();
        }
    }
}
