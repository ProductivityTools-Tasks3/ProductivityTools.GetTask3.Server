using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.TomatoTray.Managers
{
    class TomatoStatic
    {
        public DateTime LastTomatooReciveDate { get; set; }

        public TimeSpan LastTomatooReciveLength
        {
            get
            {
                return DateTime.Now - LastTomatooReciveDate;
            }
        }
        public TimeSpan TomatoTimeLength { get; set; }
    }
}
