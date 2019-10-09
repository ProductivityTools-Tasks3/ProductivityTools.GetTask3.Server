using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.AddDefinedTask
{

    [Cmdlet(VerbsCommon.Add, "DefinedTask")]
    public class AddDefinedTaskCmdlet : GT3CmldetsBase
    {
        [Parameter(Position = 0,Mandatory =true)]
        public string Name { get; set; }

        public AddDefinedTaskCmdlet()
        {
            AddCommand(new AddDefinedTaskCommand(this));
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            this.ProcessCommands();
        }
    }
}
