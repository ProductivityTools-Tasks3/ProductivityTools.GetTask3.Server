using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Domain
{
    public class DomainBase
    {
        private readonly ISessionMetaDataProvider SessionMetaDataProvider;

        internal StructureMetadata session
        {
            get
            {
                return this.SessionMetaDataProvider.SessionMetadata;
            }
        }

        public DomainBase(ISessionMetaDataProvider sessionMetaDataProvider)
        {
            this.SessionMetaDataProvider = sessionMetaDataProvider;
        }
    }
}
