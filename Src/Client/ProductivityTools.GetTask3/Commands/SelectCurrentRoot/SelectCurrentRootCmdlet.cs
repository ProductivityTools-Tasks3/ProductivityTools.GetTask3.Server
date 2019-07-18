using ProductivityTools.GetTask3.Commands;
using ProductivityTools.GetTask3.Commands.SelectCurrentRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.SingleCommands
{
    [Cmdlet(VerbsCommon.Select, "CurrentRoot")]
    public class SelectCurrentRootCmdlet : GT3CmldetsBase
    {
        [Parameter(Position = 0)]
        public int? Root { get; set; }

        [Parameter(Position = 1)]
        public SwitchParameter Parent { get; set; }

        public SelectCurrentRootCmdlet()
        {
            AddCommand(new DirectItem(this));
            AddCommand(new Parent(this));
        }

        protected override void BeginProcessing()
        {
            ProcessCommands();
            base.BeginProcessing();
        }
    }
}
