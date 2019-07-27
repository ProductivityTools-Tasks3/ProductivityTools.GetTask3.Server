using ProductivityTools.GetTask3.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.SingleCommands
{
    [Cmdlet("Add", "PredefinedTask3")]
    public class AddPredefinedTask3 : SingleCommandsBase
    {
        [Parameter(HelpMessage = "Id or Ids space separated which should be finished", Position = 0)]
        public string Name { get; set; }

        TaskStructure TaskStructure { get; set; }

        public AddPredefinedTask3()
        {
            TaskStructure = new TaskStructure(this);
        }

        protected override void ProcessRecord()
        {
            
            base.ProcessRecord();
        }
    }
}
