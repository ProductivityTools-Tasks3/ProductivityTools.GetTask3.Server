using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.StartTask
{
    [Cmdlet("Start", "Task")]
    public class StartTaskCmdlet : GT3CmldetsBase
    {
        public StartTaskCmdlet()
        {
        }

        protected override void ProcessRecord()
        {
            base.AddCommand(new Start(this));
            base.ProcessRecord();
        }
    }
}
