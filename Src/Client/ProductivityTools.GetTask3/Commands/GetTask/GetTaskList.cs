using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetTask
{
    public class GetTaskList : PSCmdlet.PSCommandPT<GetTask3Cmdlet>
    {
        protected override bool Condition => true;

        public GetTaskList(GetTask3Cmdlet cmdlet) : base(cmdlet) { }

        protected override void Invoke()
        {
            WriteOutput("GetTaskList");
            var x = GetTaskHttpClient.Get<ItemView>("List", string.Empty);

        }
    }
}
