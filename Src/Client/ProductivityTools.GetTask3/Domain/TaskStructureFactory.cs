using ProductivityTools.GetTask3.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProductivityTools.GetTask3.Domain
{
    class TaskStructureFactory
    {
        public static Task Get(System.Management.Automation.PSCmdlet cmdlet)
        {
            return new App.Task(cmdlet);
        }
    }
}
