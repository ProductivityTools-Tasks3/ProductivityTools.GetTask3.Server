using ProductivityTools.GetTask3.App;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.SingleCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.SelectCurrentRoot
{
    public class DirectItem : PSCmdlet.PSCommandPT<SelectCurrentRootCmdlet>
    {
        App.Task TaskStructure; 
        public DirectItem(SelectCurrentRootCmdlet selectCurrentRootCmdlet) : base(selectCurrentRootCmdlet)
        {
            TaskStructure = TaskStructureFactory.Get(this.Cmdlet);
        }

        protected override bool Condition => Cmdlet.Root.HasValue;

        protected override void Invoke()
        {
            TaskStructure.SelectNodeByOrder(Cmdlet.Root.Value);
            //this.Cmdlet.SessionManager.ViewMetadata.SelectNodeByOrder(Cmdlet.Root.Value);
        }
    }
}
