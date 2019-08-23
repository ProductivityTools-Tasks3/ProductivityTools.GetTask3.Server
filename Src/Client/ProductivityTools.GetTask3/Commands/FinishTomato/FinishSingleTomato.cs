using ProductivityTools.GetTask3.App;
using ProductivityTools.GetTask3.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Commands.FinishTomato
{
    class FinishSingleTomato : PSCmdlet.PSCommandPT<FinishTomatoCmdlet>
    {
        Task TaskStructure;

        public FinishSingleTomato(FinishTomatoCmdlet cmdletType) : base(cmdletType)
        {
            this.TaskStructure = TaskStructureFactory.Get(cmdletType);
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            this.TaskStructure.FinishTomato(); 
        }
    }
}
