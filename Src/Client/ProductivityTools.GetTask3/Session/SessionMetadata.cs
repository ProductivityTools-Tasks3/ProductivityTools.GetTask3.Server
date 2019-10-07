using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.View
{
    public class StructureMetadata
    {
        public Dictionary<int, ElementMetadata> ElementOrder { get; set; }

        public StructureMetadata()
        {
            this.ElementOrder = new Dictionary<int, ElementMetadata>();
        }

        public int? SelectedNodeOrder { get; set; }
        public int? SelectedNodeElementId { get; set; }
    }
}
