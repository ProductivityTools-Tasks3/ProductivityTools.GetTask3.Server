using ProductivityTools.GetTask3.Contract.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Domain
{
    class DefinedTask : DomainBase
    {
        public DefinedTask(ISessionMetaDataProvider pSVariableIntrinsics) : base(pSVariableIntrinsics) { }

        public DefinedTaskView Get(bool withDetails)
        {
            DefinedTaskRepository definedTaskRepository = new DefinedTaskRepository();
            DefinedTaskView defineddatsk;
            if (withDetails)
            {
                defineddatsk = definedTaskRepository.GetWithDetailsForbagId(session.SelectedNodeElementId);
            }
            else
            {
                defineddatsk = definedTaskRepository.GetForBag(base.session.SelectedNodeElementId);
            }
            return defineddatsk;
        }

        public void AddDefinedTask(string name)
        {
            //here bagid is needed to fill defined task repository
            //here need to be change because unecessairy id we could send name
            DefinedTaskRepository definedTaskRepository = new DefinedTaskRepository();
            DefinedTaskView definedTask;
            definedTask = definedTaskRepository.GetForBag(session.SelectedNodeElementId);
            DefinedTaskGroupView definedTaskGroup = definedTask.DefinedTasks.SingleOrDefault(x => x.Name == name);
            if (definedTask == null)
            {
                throw new Exception("No defined task with given name exists");
            }
            else
            {
                definedTaskRepository.AddDefinedTasks(definedTaskGroup.DefinedElementGroupId);
            }
        }
    }
}
