using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.StartTask
{
    public class Start : PSCmdlet.PSCommandPT<StartTaskCmdlet>
    {
        public Start(StartTaskCmdlet cmdletType) : base(cmdletType)
        {
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            throw new NotImplementedException();
        }
    }
}
