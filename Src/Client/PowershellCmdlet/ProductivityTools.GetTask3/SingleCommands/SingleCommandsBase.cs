using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.SingleCommands
{
     public class SingleCommandsBase : System.Management.Automation.PSCmdlet
    {
        protected SessionManager _sessionManager;
        public SingleCommandsBase()
        {
            _sessionManager = new SessionManager(this);
        }
    }
}
