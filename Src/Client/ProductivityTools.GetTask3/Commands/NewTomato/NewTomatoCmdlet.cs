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
    [Cmdlet(VerbsCommon.New, "Tomato")]
    public class NewTomatoCmdlet : GT3CmldetsBase
    {
        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public string Id { get; set; }

        public NewTomatoCmdlet()
        {
            this.AddCommand(new NewTomatoByName(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
        }
    }
}
