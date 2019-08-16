using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands
{
    public class NewTomatoByIds : PSCmdlet.PSCommandPT<NewTomatoCmdlet>
    {
        public NewTomatoByIds(NewTomatoCmdlet cmdletType) : base(cmdletType)
        {
        }

        protected override bool Condition => this.Cmdlet.Id.NorNullNotEmpty();

        protected override void Invoke()
        {
            var tomato = TaskStructureFactory.Get(this.Cmdlet);
            var items = this.Cmdlet.Id.GetIds();
            tomato.AddToTomato(items);
        }
    }
}
