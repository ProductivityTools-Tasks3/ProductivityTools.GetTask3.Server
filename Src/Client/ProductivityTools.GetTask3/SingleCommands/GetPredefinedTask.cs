using ProductivityTools.GetTask3.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.SingleCommands
{
    [Cmdlet("Get", "PredefinedTask3")]
    public class GetPredefinedTask : SingleCommandsBase
    {
        TaskStructure TaskStructure { get; set; }

        public GetPredefinedTask()
        {
            TaskStructure = new TaskStructure(this);
        }

        protected override void ProcessRecord()
        {
            TaskStructure.GetPredefinedTask();
            base.ProcessRecord();
        }
    }
}
