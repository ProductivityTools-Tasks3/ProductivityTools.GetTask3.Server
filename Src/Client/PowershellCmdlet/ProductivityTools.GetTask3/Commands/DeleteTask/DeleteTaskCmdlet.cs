using ProductivityTools.GetTask3.Commands.DelayTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.DeleteTask
{
    [Cmdlet("Delete", "Task")]
    public class DeleteTaskCmdlet : GT3CmldetsBase
    {
        [Parameter(Position = 0)]
        public string Id { get; set; }

    
        public DeleteTaskCmdlet()
        {
            this.AddCommand(new Delete(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
        }
    }
}
