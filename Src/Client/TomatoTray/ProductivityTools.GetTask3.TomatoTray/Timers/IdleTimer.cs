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
    class IdleTimer
    {
        TimeSpan tomatoTime = TimeSpan.Zero;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        E.EventAggregator EventAggregator { get; set; }

        public IdleTimer(E.EventAggregator eventAggregator)
        {
            this.EventAggregator = eventAggregator;
        }

        public void Run()
        {
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        //pw:parameter
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (tomatoTime > Consts.BreakLength)
            {
                EventAggregator.PublishEvent<IdleExceedEvent>(new IdleExceedEvent());
            }
            SetMouseOverTooltipContent();
        }

        private void SetMouseOverTooltipContent()
        {
            tomatoTime = tomatoTime.Add(TimeSpan.FromSeconds(1));
            EventAggregator.PublishEvent(new SetTooltipContentEvent(TimeSpan.MinValue, tomatoTime));
        }
    }
}
