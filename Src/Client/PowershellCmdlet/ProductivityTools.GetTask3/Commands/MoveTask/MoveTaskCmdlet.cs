using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.MoveTask
{
    [Cmdlet("Move", "Task")]
    public class MoveTaskCmdlet : GT3CmldetsBase
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public int? Target { get; set; }

        [Parameter]
        public SwitchParameter Parent { get; set; }

        public MoveTaskCmdlet()
        {
            this.AddCommand(new MoveTaskToChild(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
        }
    }
}
