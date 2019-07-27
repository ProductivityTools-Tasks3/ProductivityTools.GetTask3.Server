using ProductivityTools.GetTask3.Contract.Responses;
using ProductivityTools.GetTask3.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetDefinedTask
{
    public class GetDefinedTaskListWithDetails : PSCmdlet.PSCommandPT<GetDefinedTaskCmdlet>
    {
        protected override bool Condition => this.Cmdlet.Details.IsPresent;

        public GetDefinedTaskListWithDetails(GetDefinedTaskCmdlet cmdlet) : base(cmdlet) { }

        protected override void Invoke()
        {
            DefinedTaskView definedTasks = new Domain.DefinedTask().Get(true);
            foreach (var item in definedTasks.DefinedTasks)
            {
                WriteOutput($"[{item.BagName}] {item.Name}");
                foreach (var definedTask in item.Items)
                {
                    WriteOutput($"+{definedTask.Name}");
                }
            }
        }
    }
}
