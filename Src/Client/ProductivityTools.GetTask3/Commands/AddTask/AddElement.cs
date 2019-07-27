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
            string[] elements = this.Cmdlet.Name.Split('\n').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            foreach (var element in elements)
            {
                ts.Add(element, this.Cmdlet.Type);
            }
        }
    }
}
