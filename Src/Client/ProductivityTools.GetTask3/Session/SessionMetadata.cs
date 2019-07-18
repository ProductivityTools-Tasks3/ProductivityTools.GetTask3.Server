using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.View
{
    public class SessionMetadata
    {
        public Dictionary<int, SessionElementMetadata> ItemOrder { get; set; }

        public SessionMetadata()
        {
            this.ItemOrder = new Dictionary<int, SessionElementMetadata>();
        }

        public int? SelectedNodeOrder { get; set; }
        public int? SelectedNodeElementId { get; set; }
    }
}
