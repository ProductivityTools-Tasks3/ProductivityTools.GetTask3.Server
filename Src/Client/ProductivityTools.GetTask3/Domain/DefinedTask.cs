using ProductivityTools.GetTask3.Contract.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Domain
{
    class DefinedTask
    {
        public DefinedTaskView Get(bool withDetails)
        {
            DefinedTaskRepository definedTaskRepository = new DefinedTaskRepository();
            DefinedTaskView defineddatsk;
            if (withDetails)
            {
                defineddatsk = definedTaskRepository.GetWithDetails();
            }
            else
            {
                defineddatsk = definedTaskRepository.Get();
            }
            return defineddatsk;
        }
    }
}
