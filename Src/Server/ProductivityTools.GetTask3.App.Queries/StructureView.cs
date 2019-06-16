using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Queries
{
    public class xxx
    {
        public int pawel { get; set; }
        public string marcin { get; set; }
    }

    public class StructureView
    {
        public List<ItemView> Items { get; set; }

        public StructureView()
        {
            this.Items = new List<ItemView>();
        }

    }
}
