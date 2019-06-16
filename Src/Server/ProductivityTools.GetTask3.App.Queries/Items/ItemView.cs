using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Queries
{
    public class ItemView
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int OrderId { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime Finished { get; set; }

        public List<ItemView> Items { get; set; }
    }
}
