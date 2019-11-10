using ProductivityTools.GetTask3.CoreObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.AddTask
{
    [Cmdlet(VerbsCommon.Add, "Task3")]
    public class AddTaskCmdlet: GT3CmldetsBase, IFromElementPath
    {
        [Parameter(Position = 0)]
        public string Name { get; set; }

        [Parameter(Position = 1)]
        public ElementType Type { get; set; }

        [Parameter(Position = 2)]
        public string From { get; set; }

        public AddTaskCmdlet()
        {
            this.AddCommand(new AddElement(this));
        }

        protected override void ProcessRecord()
        {
            this.ProcessCommands();
            base.ProcessRecord();
        }

    }
}
