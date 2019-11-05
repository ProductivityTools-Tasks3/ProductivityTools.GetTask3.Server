using ProductivityTools.GetTask3.App;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Commands.MoveTask
{
    class MoveTaskToChild : PSCmdlet.PSCommandPT<MoveTaskCmdlet>
    {
        Task TaskStructure;

        protected override bool Condition => this.Cmdlet.Target.HasValue && this.Cmdlet.Id.NorNullNotEmpty();

        public MoveTaskToChild(MoveTaskCmdlet cmdletType) : base(cmdletType)
        {
            this.TaskStructure = TaskStructureFactory.Get(cmdletType);
        }

        protected override void Invoke()
        {
            var ids = this.Cmdlet.Id.GetIds();
            if (ids.Any(x=>x==this.Cmdlet.Target.Value))
            {
                throw new Exception("Target cannot be the same as source");
            }
            this.TaskStructure.Move(ids, this.Cmdlet.Target.Value);
        }
    }
}
