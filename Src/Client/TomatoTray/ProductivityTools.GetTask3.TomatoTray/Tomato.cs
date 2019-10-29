using ProductivityTools.GetTask3.CoreObjects.Tomato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.TomatoTray
{
    public class Tomato
    {
        public DateTime CreatedDate { get; internal set; }
        public DateTime? FinishedDate { get; set; }
        public string Name { get; internal set; }
        public int TaskId { get; internal set; }
        public Status Status { get; set; }

        public TimeSpan TomatoTimeLength
        {
            get
            {
                return DateTime.Now.Subtract(CreatedDate);
            }
        }
    }
}
