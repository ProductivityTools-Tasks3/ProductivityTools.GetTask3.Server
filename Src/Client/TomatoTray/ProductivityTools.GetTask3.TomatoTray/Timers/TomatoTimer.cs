using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomatoesTray.Events;
using E = ProductivityTools.GetTask3.TomatoTray.EventAggregator;

namespace ProductivityTools.GetTask3.TomatoTray.Timers
{
    class TomatoTimer
    {
        TimeSpan tomatoTime = TimeSpan.Zero;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        E.EventAggregator EventAggregator { get; set; }

        

        public TomatoTimer(E.EventAggregator eventAggregator)
        {
            this.EventAggregator = eventAggregator;
            //Run();
        }

        public void Run()
        {
            tomatoTime = TimeSpan.Zero;
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        public void Stop()
        {
            dispatcherTimer.Stop();
        }

       //pw:parameter
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            SetMouseOverTooltipContent();
            if (this.tomatoTime>TimeSpan.FromMinutes(1))
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
