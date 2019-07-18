using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands
{
    public class GT3CmldetsBase: PSCmdlet.PSCmdletPT
    {
        public SessionManager SessionManager;
        public GT3CmldetsBase()
        {
            SessionManager = new SessionManager(this);
        }
    }
}
