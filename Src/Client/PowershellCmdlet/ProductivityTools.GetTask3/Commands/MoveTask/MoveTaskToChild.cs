using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.MoveTask
{
    class MoveTaskToChild : PSCmdlet.PSCommandPT<MoveTaskCmdlet>
    {

        protected override bool Condition => this.Cmdlet.Target.HasValue && this.Cmdlet.Id.NorNullNotEmpty();

        public MoveTaskToChild(MoveTaskCmdlet cmdletType) : base(cmdletType)
        {
        }

        protected override void Invoke()
        {
            throw new NotImplementedException();
        }
    }
}
