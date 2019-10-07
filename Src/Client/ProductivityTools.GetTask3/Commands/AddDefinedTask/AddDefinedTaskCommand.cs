using ProductivityTools.GetTask3.App;
using ProductivityTools.GetTask3.Contract.Responses;
using ProductivityTools.GetTask3.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.AddDefinedTask
{
    public class AddDefinedTaskCommand : PSCmdlet.PSCommandPT<AddDefinedTaskCmdlet>
    {
        protected override bool Condition => true;

        public AddDefinedTaskCommand(AddDefinedTaskCmdlet cmdlet) : base(cmdlet) { }

        protected override void Invoke()
        {
            ISessionMetaDataProvider sessionMetaDataProvider = new SessionMetaDataProvider(Cmdlet);
            var d = new Domain.DefinedTask(sessionMetaDataProvider);
            //DefinedTaskView definedTasks = d.Get(false);
            //string definedTaskName = this.Cmdlet.Name;
            //var id = definedTasks.DefinedTasks.SingleOrDefault(x => x.Name ==definedTaskName);
            //if (id==null)
            //{
            //    throw new Exception("No defined task group with given name");
            //}
            //else
            //{
                d.AddDefinedTask(this.Cmdlet.Name);
            //}
            //foreach (var item in definedTasks.DefinedTasks)
            //{
            //    WriteOutput($"[Bag name:{item.BagName}] {item.Name}");
            //}
        }
    }
}