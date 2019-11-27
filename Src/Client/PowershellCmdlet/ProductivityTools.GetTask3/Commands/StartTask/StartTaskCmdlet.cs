using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.StartTask
{
    [Cmdlet("Start", "Task")]
    public class StartTaskCmdlet : GT3CmldetsBase, IFromElementPath
    {
        [Parameter(HelpMessage = "Id or Ids space separated which should be finished", Position = 0)]
        public string Id { get; set; }

        [Parameter]
        public string Path { get; set; }


        public StartTaskCmdlet()
        {
            base.AddCommand(new Start(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
