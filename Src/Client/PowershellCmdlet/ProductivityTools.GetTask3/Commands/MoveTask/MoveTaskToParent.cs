using ProductivityTools.GetTask3.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.MoveTask
{
    class MoveTaskToParent : PSCmdlet.PSCommandPT<MoveTaskCmdlet>
    {
        protected override bool Condition => this.Cmdlet.Id.NorNullNotEmpty() && this.Cmdlet.Parent.IsPresent;

        public MoveTaskToParent(MoveTaskCmdlet cmdletType) : base(cmdletType)
        {
        }

        protected override void Invoke()
        {
            throw new NotImplementedException();
        }
    }
}
