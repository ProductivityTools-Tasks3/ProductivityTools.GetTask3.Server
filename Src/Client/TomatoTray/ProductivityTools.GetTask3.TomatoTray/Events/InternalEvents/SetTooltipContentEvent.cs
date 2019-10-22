using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomatoesTray.Events
{
    class SetTooltipContentEvent : BaseEvent
    {
        private SetTooltipContentEvent()
        {
        }

        public SetTooltipContentEvent(TimeSpan TomatoTimeLength, TimeSpan LastTomatooReciveLength)
        {
            this.TomatoTimeLength = TomatoTimeLength;
            this.LastTomatooReciveLength = LastTomatooReciveLength;
        }
        public  TimeSpan TomatoTimeLength { get; set; }
        public TimeSpan LastTomatooReciveLength { get; set; }

    }
}
