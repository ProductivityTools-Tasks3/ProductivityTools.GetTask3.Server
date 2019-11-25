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
        [Parameter(Position = 0)]
        public string Id { get; set; }

        [Parameter(Position = 1)]
        public DateTime Date { get; set; }

        [Parameter(HelpMessage = "It will dealay task to tommorow")]
        public SwitchParameter Tommorow { get; set; }

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
