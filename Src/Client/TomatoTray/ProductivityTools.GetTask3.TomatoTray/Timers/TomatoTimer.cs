using ProductivityTools.GetTask3.CommonConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomatoesTray.Events;
using E = ProductivityTools.GetTask3.TomatoTray.EventAggregator;

namespace ProductivityTools.GetTask3.TomatoTray.Timers
{
    class TomatoTimer: BaseTimer
    {
        public TomatoTimer(E.EventAggregator eventAggregator) :base(eventAggregator)
        {
        }

        protected override void Tick()
        {
            SetMouseOverTooltipContent();
            if (tomatoTime > Consts.TomatoLength)
            {
                EventAggregator.PublishEvent<TomatoExceedEvent>(new TomatoExceedEvent());
            }
        }

        private void SetMouseOverTooltipContent()
        {
            tomatoTime = tomatoTime.Add(TimeSpan.FromSeconds(1));
            EventAggregator.PublishEvent(new SetTooltipContentEvent(tomatoTime, tomatoTime));
        }
    }
}
