using ProductivityTools.GetTask3.App;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Commands.DelayTask
{
    class DelayTaskToDate : PSCmdlet.PSCommandPT<DelayTaskCmdlet>
    {
        Task TaskStructure;

        public DelayTaskToDate(DelayTaskCmdlet cmdletType) : base(cmdletType)
        {
            this.TaskStructure = TaskStructureFactory.Get(cmdletType);
        }

        protected override bool Condition => this.Cmdlet.Tommorow.IsPresent == false;

        protected override void Invoke()
        {
            var ids = this.Cmdlet.Id.GetIds();
            foreach (var id in ids)
            {
                this.TaskStructure.Delay(id, this.Cmdlet.Date);
            }

        }
    }
}
