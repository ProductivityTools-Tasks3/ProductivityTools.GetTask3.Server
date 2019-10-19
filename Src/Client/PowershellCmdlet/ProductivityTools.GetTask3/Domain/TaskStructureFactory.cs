using ProductivityTools.GetTask3.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace ProductivityTools.GetTask3.Domain
{
    public class TaskStructureFactory
    {
        internal static Task Get(System.Management.Automation.PSCmdlet cmdlet)
        {
            ISessionMetaDataProvider sessionMetaDataProvider = new SessionMetaDataProvider(cmdlet);
            return new App.Task(sessionMetaDataProvider, new TaskRepositoryCmd());
        }
    }
}
