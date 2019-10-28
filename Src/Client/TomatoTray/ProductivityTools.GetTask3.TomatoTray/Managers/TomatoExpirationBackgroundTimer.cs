using ProductivityTools.GetTask3.TomatoTray.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomatoesTray.Events;
using E = ProductivityTools.GetTask3.TomatoTray.EventAggregator;

namespace ProductivityTools.GetTask3.TomatoTray.Managers
{
    class BackgroundTimer
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        int tickCount = 0;
        bool baloonShowed = false;
        E.EventAggregator EventAggregator { get; set; }
        int IdleCounter = 0;

        public BackgroundTimer(E.EventAggregator eventAggregator)
        {
            this.EventAggregator = eventAggregator;
            RunServer();
        }
        private void RunServer()
        {
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            tickCount++;
            if (tickCount > 5 && baloonShowed == false)
            {
                //EventAggregator.PublishEvent(new ChangeTaskBarIconPicEvent { IconType = TomatoStatus.Idle });
                ShowBalonIdle();
                baloonShowed = true;
                IdleCounter++;
            }
           // EventAggregator.PublishEvent(new BackgroundTimerTickEvent() { idleId = IdleCounter });
        }

        public void ShowBalonIdle()
        {
            EventAggregator.PublishEvent(new ShowBalonEvent( Resources.IdleMessage,  TomatoDisplayStatus.Idle ));
        }
    }
}
