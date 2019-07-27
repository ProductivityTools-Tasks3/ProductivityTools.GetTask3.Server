using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetDefinedTask
{
    [Cmdlet(VerbsCommon.Get, "DefinedTask")]
    public class GetDefinedTaskCmdlet : GT3CmldetsBase
    {
        [Parameter(HelpMessage ="Will show also task in given bags")]
        public SwitchParameter Details { get; set; }

        public GetDefinedTaskCmdlet()
        {
            this.AddCommand(new GetDefinedTaskList(this));
            this.AddCommand(new GetDefinedTaskListWithDetails(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
        }
    }
}
