using ProductivityTools.GetTask3.CoreObjects.Tomato;
using ProductivityTools.GetTask3.Infrastructure.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure
{
    public class Tomato : BaseObject
    {
        public int TomatoId { get; set; }
        public Status Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Finished { get; set; }

        public List<TomatoElement> TomatoElements { get; set; }
    }
}
