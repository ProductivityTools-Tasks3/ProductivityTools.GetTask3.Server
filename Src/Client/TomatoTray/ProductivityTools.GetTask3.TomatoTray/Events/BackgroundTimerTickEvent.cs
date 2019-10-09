using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomatoesTray.Events
{
    class BackgroundTimerTickEvent : BaseEvent
    {
        public int idleId { get; set; }
    }
}
