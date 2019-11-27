using ProductivityTools.GetTask3.App;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Commands.StartTask
{
    public class Start : PSCmdlet.PSCommandPT<StartTaskCmdlet>
    {
        Task TaskStructure;

        public Start(StartTaskCmdlet cmdletType) : base(cmdletType)
        {
            this.TaskStructure = TaskStructureFactory.Get(this.Cmdlet);
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
           
            int[] orderIds = this.Cmdlet.Id.GetIds();
            foreach (var id in orderIds)
            {
                TaskStructure.Start(id);
            }
        }
    }
}
