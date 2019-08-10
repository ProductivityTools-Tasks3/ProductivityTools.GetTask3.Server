using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands
{
    class NewTomatoByName : PSCmdlet.PSCommandPT<NewTomatoCmdlet>
    {
        protected override bool Condition => this.Cmdlet.Name.NorNullNotEmpty();

        public NewTomatoByName(NewTomatoCmdlet cmdletType) : base(cmdletType) { }


        protected override void Invoke()
        {
            var x = new Tomato();
            x.CreateNew(this.Cmdlet.Name);
        }
    }
}
