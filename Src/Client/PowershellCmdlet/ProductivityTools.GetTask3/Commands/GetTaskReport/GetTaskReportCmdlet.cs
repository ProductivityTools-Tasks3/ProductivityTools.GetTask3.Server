using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetTaskReport
{
    [Cmdlet(VerbsCommon.Get, "TaskReport")]
    public class GetTaskReportCmdlet : GT3CmldetsBase, IFromElementPath
    {
        [Parameter]
        public string Path { get; set; }

        protected override void ProcessRecord()
        {
            this.AddCommand(new MultiLevel(this));
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
