using ProductivityTools.GetTask3.Commands;
using ProductivityTools.GetTask3.Commands.GetTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3
{
    [Cmdlet(VerbsCommon.Get, "Task3")]
    public class GetTask3Cmdlet : CommandsBase
    {

        public GetTask3Cmdlet()
        {
            this.AddCommand(new GetTaskList(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
        }
    }
}
