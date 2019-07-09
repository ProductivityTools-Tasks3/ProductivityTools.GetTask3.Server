using ProductivityTools.GetTask3.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Domain
{
    class TaskStructureFactory
    {
        public static TaskStructure Get(System.Management.Automation.PSCmdlet cmdlet)
        {
            return new TaskStructure(cmdlet);
        }
    }
}
