using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.DelayTask
{
    [Cmdlet("Delay", "Task")]
    public class DelayTaskCmdlet : GT3CmldetsBase
    {
        [Parameter(HelpMessage ="It will dealay task to tommorow")]
        public SwitchParameter Tommorow { get; set; }

        [Parameter]
        public DateTime Date { get; set; }

        [Parameter]
        public string Id { get; set; }

        public DelayTaskCmdlet()
        {
            this.AddCommand(new DelayTaskToDate(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
        }
    }
}
