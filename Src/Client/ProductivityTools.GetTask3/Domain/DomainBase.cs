using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Domain
{
    class DomainBase
    {
        protected System.Management.Automation.PSCmdlet cmdlet;
        string _sesisonKey = "ViewMetadata";

        protected SessionMetadata _sessionMetadata
        {
            get
            {
                var r = cmdlet.SessionState.PSVariable.Get(_sesisonKey);
                if (r == null)
                {
                    cmdlet.SessionState.PSVariable.Set(_sesisonKey, new SessionMetadata());
                    r = cmdlet.SessionState.PSVariable.Get(_sesisonKey);
                }
                return (SessionMetadata)r.Value;
            }
        }

        public DomainBase(System.Management.Automation.PSCmdlet pSVariableIntrinsics)
        {
            this.cmdlet = pSVariableIntrinsics;
        }
    }
}
