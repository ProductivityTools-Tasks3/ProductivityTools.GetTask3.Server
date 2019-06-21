using System;
using System.Collections.Generic;

namespace ProductivityTools.GetTask3.Contract
{
    public class ItemView
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int OrderId { get; set; }
        public int ElementId { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime Finished { get; set; }

        public List<ItemView> Elements { get; set; }
    }
}
