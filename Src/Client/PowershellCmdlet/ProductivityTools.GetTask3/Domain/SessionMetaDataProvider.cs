using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Domain
{
    class SessionMetaDataProvider : ISessionMetaDataProvider
    {
        protected System.Management.Automation.PSCmdlet Cmdlet;
        string _sesisonKey = "ViewMetadata";

        public SessionMetaDataProvider(System.Management.Automation.PSCmdlet cmdlet)
        {
            this.Cmdlet = cmdlet;
        }

        public StructureMetadata SessionMetadata
        {
            get
            {
                var r = Cmdlet.SessionState.PSVariable.Get(_sesisonKey);
                if (r == null)
                {
                    Cmdlet.SessionState.PSVariable.Set(_sesisonKey, new StructureMetadata());
                    r = Cmdlet.SessionState.PSVariable.Get(_sesisonKey);
                }
                return (StructureMetadata)r.Value;
            }
        }
    }
}
