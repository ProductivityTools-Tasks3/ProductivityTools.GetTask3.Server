using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetTomatoReport
{
     [Cmdlet(VerbsCommon.Get, "TomatoReport")]
    public class GetTomatoReportCmdlet : GT3CmldetsBase, IFromElementPath
    {
        [Parameter]
        public string Path { get; set; }

        [Parameter]
        public int Ago { get; set; }

        protected override void ProcessRecord()
        {
            this.AddCommand(new Report(this));
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
