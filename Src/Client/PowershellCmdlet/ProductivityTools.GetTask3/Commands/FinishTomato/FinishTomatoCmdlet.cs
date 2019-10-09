using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.FinishTomato
{
    [Cmdlet("Finish", "Tomato")]
    public class FinishTomatoCmdlet : GT3CmldetsBase
    {
        [Parameter]
        public SwitchParameter FinishAlsoTasks { get; set; }

        public FinishTomatoCmdlet()
        {
            this.AddCommand(new FinishSingleTomato(this));
        }

        protected override void ProcessRecord()
        {
            ProcessCommands();
            base.ProcessRecord();

        }
    }
}
