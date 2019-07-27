using System.Collections.Generic;

namespace ProductivityTools.GetTask3.Contract.Responses
{
    public class DefinedTaskGroup
    {
        public string BagName { get; set; }
        public string Name { get; set; }

        public List<DefinedTask> Items { get; set; }
    }
}