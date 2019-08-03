using ProductivityTools.GetTask3.Contract.Responses;
using ProductivityTools.GetTask3.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetDefinedTask
{
    public class GetDefinedTaskList : PSCmdlet.PSCommandPT<GetDefinedTaskCmdlet>
    {
        protected override bool Condition => this.Cmdlet.Details.IsPresent == false;

        public GetDefinedTaskList(GetDefinedTaskCmdlet cmdlet) : base(cmdlet) { }

        protected override void Invoke()
        {
            DefinedTaskView definedTasks = new Domain.DefinedTask(Cmdlet).Get(false);
            foreach (var item in definedTasks.DefinedTasks)
            {
                WriteOutput($"[Bag name:{item.BagName}] {item.Name}");
            }
        }
    }
}
