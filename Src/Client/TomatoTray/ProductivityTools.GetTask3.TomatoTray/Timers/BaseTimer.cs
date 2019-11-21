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
    abstract class BaseTimer
    {
        protected Tomato Tomato;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        protected E.EventAggregator EventAggregator { get; set; }

        public BaseTimer(E.EventAggregator eventAggregator)
        {
            this.EventAggregator = eventAggregator;
        }

        //public void Run()
        //{
        //    tomatoTime = TimeSpan.Zero;
        //    dispatcherTimer.Tick += dispatcherTimer_Tick;
        //    dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        //    dispatcherTimer.Start();
        //}

        public void Run(Tomato tomato)
        {
            Tomato = tomato;
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        public void Stop()
        {
            dispatcherTimer.Stop();
        }

        protected abstract void Tick();


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Tick();
        }
    }
}
