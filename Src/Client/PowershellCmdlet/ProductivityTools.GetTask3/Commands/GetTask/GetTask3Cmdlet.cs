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
    public class GetTask3Cmdlet : GT3CmldetsBase, IFromElementPath
    {
        [Parameter]
        public string From { get; set; }

        public GetTask3Cmdlet()
        {
            
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            this.AddCommand(new GetTaskList(this));
            base.ProcessCommands();
        }
    }
}
