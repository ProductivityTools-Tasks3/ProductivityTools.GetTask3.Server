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
    class IdleTimer : BaseTimer
    {

        public IdleTimer(E.EventAggregator eventAggregator) : base(eventAggregator)
        {
        }

        protected override void Tick()
        {
            if (this.Tomato.TomatoIdle> Consts.BreakLength)
            {
                EventAggregator.PublishEvent<IdleExceedEvent>(new IdleExceedEvent());
            }
            SetMouseOverTooltipContent();
        }


        private void SetMouseOverTooltipContent()
        {
            EventAggregator.PublishEvent(new SetTooltipContentEvent(TimeSpan.MinValue, this.Tomato.TomatoTimeLength));
        }
    }
}
