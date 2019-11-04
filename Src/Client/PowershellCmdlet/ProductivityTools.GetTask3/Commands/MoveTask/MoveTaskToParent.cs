using ProductivityTools.GetTask3.App;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Commands.MoveTask
{
    class MoveTaskToParent : PSCmdlet.PSCommandPT<MoveTaskCmdlet>
    {
        Task TaskStructure;

        protected override bool Condition => this.Cmdlet.Id.NorNullNotEmpty() && this.Cmdlet.Parent.IsPresent;

        public MoveTaskToParent(MoveTaskCmdlet cmdletType) : base(cmdletType)
        {
            this.TaskStructure = TaskStructureFactory.Get(cmdletType);
        }

        protected override void Invoke()
        {
            var ids = this.Cmdlet.Id.GetIds();
            this.TaskStructure.MoveToParent(ids);
        }
    }
}
