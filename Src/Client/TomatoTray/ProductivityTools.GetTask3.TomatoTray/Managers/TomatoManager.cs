using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomatoesTray.Events;
using E = ProductivityTools.GetTask3.TomatoTray.EventAggregator;

namespace ProductivityTools.GetTask3.TomatoTray.Managers
{
    class TomatoManager : E.IEvent<TomatoInfoFlyInEvent>, E.IEvent<BackgroundTimerTickEvent>, E.IEvent<ClearTomatoDisplayList>
    {
        E.EventAggregator EventAggregator { get; set; }
        // BaloonNotyfication BaloonNotyfication { get; set; }
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        //int tickCount = 0;

        private static TomatoStatic TomatoStatic = new TomatoStatic();
        BackgroundTimer timer;

        public TomatoManager(E.EventAggregator eventAggregator)
        {
            this.EventAggregator = eventAggregator;
            // BaloonNotyfication = new BaloonNotyfication(this.EventAggregator);
            //RunTimer();
            timer = new BackgroundTimer(eventAggregator);
            this.EventAggregator.Subscribe(this);
        }


        void E.IEvent<TomatoInfoFlyInEvent>.OnEvent(TomatoInfoFlyInEvent @event)
        {
            this.timer.ResetLastTomatoFlyIn();
            SetTooltipContent(@event.Tomato);
            this.ShowDialog(@event.Tomato);
        }
        private List<KeyValuePair<TomatoStatus, int>> ShowedElements = new List<KeyValuePair<TomatoStatus, int>>();

        public void ShowDialog(TomatoContract.Tomato tomato)
        {

            if (TomatoStatic.TomatoTimeLength > Consts.TomatoLength)
            {
                TomatoExpired(tomato);
            }
            else
            {
                TomatoUpToDate(tomato);
            }

        }

        private KeyValuePair<TomatoStatus, int> CreateKV(TomatoStatus status, int taskId)
        {
            return new KeyValuePair<TomatoStatus, int>(status, taskId);
        }

        private void TomatoUpToDate(TomatoContract.Tomato tomato)
        {
            if (ShowedElements.Contains(CreateKV(TomatoStatus.Work, tomato.TaskId)) == false)
            {
                EventAggregator.PublishEvent(new ShowBalonEvent { Text = tomato.Name, Status = TomatoStatus.Work });
                EventAggregator.PublishEvent(new ChangeTaskBarIconPicEvent { IconType = TomatoStatus.Work });
                ShowedElements.Add(CreateKV(TomatoStatus.Work, tomato.TaskId));
            }
        }

        private void TomatoExpired(TomatoContract.Tomato tomato)
        {
            if (ShowedElements.Contains(CreateKV(TomatoStatus.WorkExceed, tomato.TaskId)) == false)
            {
                EventAggregator.PublishEvent(new ShowBalonEvent { Text = tomato.Name, Status = TomatoStatus.WorkExceed });
                EventAggregator.PublishEvent(new ChangeTaskBarIconPicEvent { IconType = TomatoStatus.WorkExceed });
                ShowedElements.Add(CreateKV(TomatoStatus.WorkExceed, tomato.TaskId));
            }
        }

        private void SetTooltipContent(TomatoContract.Tomato tomato)
        {
            TomatoStatic.LastTomatooReciveDate = DateTime.Now;
            TomatoStatic.TomatoTimeLength = TomatoStatic.LastTomatooReciveDate - tomato.CreatedDate;

            EventAggregator.PublishEvent(new SetTooltipContentEvent(TomatoStatic.TomatoTimeLength, TomatoStatic.LastTomatooReciveLength));
        }

        void E.IEvent<BackgroundTimerTickEvent>.OnEvent(BackgroundTimerTickEvent @event)
        {
            EventAggregator.PublishEvent(new SetTooltipContentEvent(TomatoStatic.TomatoTimeLength, TomatoStatic.LastTomatooReciveLength));

            if (ShowedElements.Contains(CreateKV(TomatoStatus.IdleExceed, @event.idleId)) == false)
            {
                var last = TomatoStatic.LastTomatooReciveLength;
                if (last > Consts.BreakLength)
                {
                    EventAggregator.PublishEvent(new ShowBalonEvent { Text = Properties.Resources.FinishIdle, Status = TomatoStatus.IdleExceed });
                    EventAggregator.PublishEvent(new ChangeTaskBarIconPicEvent { IconType = TomatoStatus.IdleExceed });
                    ShowedElements.Add(CreateKV(TomatoStatus.IdleExceed, @event.idleId));
                }
            }
        }

        void E.IEvent<ClearTomatoDisplayList>.OnEvent(ClearTomatoDisplayList @event)
        {
            this.ShowedElements.Clear();
        }
    }
}
