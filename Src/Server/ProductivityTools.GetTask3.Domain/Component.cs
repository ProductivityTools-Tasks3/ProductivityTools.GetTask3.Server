using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Domain
{
    public class xxx
    {
        public int pawel { get; set; }
        public string marcin { get; set; }
    }

    public abstract class Component
    {
        public string Name { get; set; }

        public xxx xxx { get; set; }

        public Component(string name)
        {
            this.Name = name;
        }
    }
}
