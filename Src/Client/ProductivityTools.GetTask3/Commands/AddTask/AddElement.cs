using ProductivityTools.GetTask3.App;
using ProductivityTools.GetTask3.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.AddTask
{
    class AddElement : PSCmdlet.PSCommandPT<AddTaskCmdlet>
    {
        protected override bool Condition => true;

        public AddElement(AddTaskCmdlet cmdlet) : base(cmdlet)
        {
            
            
        }

        protected override void Invoke()
        {
            TaskStructure ts = TaskStructureFactory.Get(this.Cmdlet);
            ts.Add(this.Cmdlet.Name, this.Cmdlet.Type);
        }
    }
}
