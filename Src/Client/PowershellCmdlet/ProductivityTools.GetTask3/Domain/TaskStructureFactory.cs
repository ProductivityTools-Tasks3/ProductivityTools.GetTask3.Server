using ProductivityTools.GetTask3.App;
using ProductivityTools.GetTask3.Commands;
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
            IFromElementPath fromElementPath = cmdlet as IFromElementPath;
            if (fromElementPath == null)
            {
                return new App.Task(sessionMetaDataProvider, new TaskRepositoryCmd(), string.Empty);
            }
            else
            {
                return new App.Task(sessionMetaDataProvider, new TaskRepositoryCmd(), fromElementPath.From);
            }
        }
    }
}
