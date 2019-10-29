using Hardcodet.Wpf.TaskbarNotification;
using ProductivityTools.GetTask3.CommonConfiguration;
using ProductivityTools.GetTask3.CoreObjects.Tomato;
using ProductivityTools.GetTask3.TomatoTray;
using ProductivityTools.GetTask3.TomatoTray.EventAggregator;
using ProductivityTools.GetTask3.TomatoTray.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using TomatoesTray.Events;

namespace TomatoesTray
{

    class IconNotyfication :
        IEvent<TomatoInfoFlyInEvent>,
        IEvent<TomatoFinishEvent>,

        IEvent<SetTooltipContentEvent>,
        IEvent<CloseBalonEvent>,
        IEvent<IdleExceedEvent>,
        IEvent<TomatoExceedEvent>
    //, IEvent<ChangeTaskBarIconPicEvent>,
    {
        public TaskbarIcon TaskbarIcon { get; private set; }
        EventAggregator EventAggregator { get; set; }
        private Balloon baloon { get; set; }
        private Tomato Tomato { get; set; }
        private TomatoTimer TomatoTimer;
        private IdleTimer IdleTimer;

        bool TomatoExceedEventShowed = false;
        bool IdleExceedEventShowed = false;

        private TomatoDisplayStatus TomatoStatus
        {
            get
            {
                if (this.Tomato == null) { return TomatoDisplayStatus.Idle; }

                if (Tomato.Status == Status.New
                    && this.Tomato.CreatedDate.Add(Consts.TomatoLength) > DateTime.Now)
                {
                    return TomatoDisplayStatus.Work;
                }

                if (Tomato.Status == Status.New
                    && this.Tomato.CreatedDate.Add(Consts.TomatoLength) < DateTime.Now)
                {
                    return TomatoDisplayStatus.WorkExceed;
                }

                if (this.Tomato.Status == Status.Finished
                    && this.Tomato.FinishedDate.Add(Consts.BreakLength) > DateTime.Now)
                {
                    return TomatoDisplayStatus.Idle;
                }

                if (this.Tomato.Status == Status.Finished
                    && this.Tomato.FinishedDate.Add(Consts.BreakLength) < DateTime.Now)
                {
                    return TomatoDisplayStatus.IdleExceed;
                }

                throw new Exception("Wrong state");
            }
        }

        public IconNotyfication(EventAggregator EventAggregator)
        {
            TaskbarIcon = new TaskbarIcon();
            TaskbarIcon.Icon = new System.Drawing.Icon(@".\Icons\TomatoGray.ico");
            TaskbarIcon.ContextMenu = new System.Windows.Controls.ContextMenu();
            TaskbarIcon.ContextMenu.Items.Add(new MenuItem() { Header = "Close", Command = new DelegateCommand { CommandAction = () => Application.Current.Shutdown() } });
            TaskbarIcon.DoubleClickCommand = DoubleClick;

            this.EventAggregator = EventAggregator;
            this.EventAggregator.Subscribe(this);

            this.TomatoTimer = new TomatoTimer(EventAggregator);
            this.IdleTimer = new IdleTimer(EventAggregator);

            //  dispatcherTimer.Start();
        }


        ICommand DoubleClick
        {
            get
            {
                return new DelegateCommand() { CommandAction = () => ShowBallon() };
            }
        }

        private void ChangeIconPic(TomatoDisplayStatus iconType)
        {
            string iconPath = string.Empty;
            switch (iconType)
            {
                case TomatoDisplayStatus.Work:
                    iconPath = @".\Icons\TomatoGreen.ico";
                    break;
                case TomatoDisplayStatus.Idle:
                    iconPath = @".\Icons\TomatoGray.ico";
                    break;
                case TomatoDisplayStatus.WorkExceed:
                    iconPath = @".\Icons\TomatoRed.ico";
                    break;
                case TomatoDisplayStatus.IdleExceed:
                    iconPath = @".\Icons\TomatoDarkGray.ico";
                    break;
                default:
                    break;
            }

            TaskbarIcon.Icon = new System.Drawing.Icon(iconPath);
        }

        private void ShowBallon()
        {
            this.baloon = new Balloon(this.EventAggregator, this.Tomato.Name, this.TomatoStatus);
            TaskbarIcon.ShowCustomBalloon(baloon, PopupAnimation.Fade, 15000);
        }

        void IEvent<TomatoInfoFlyInEvent>.OnEvent(TomatoInfoFlyInEvent @event)
        {
            this.Tomato = @event.Tomato;
            ShowBallon();
            this.TomatoTimer.Run();
            ChangeIconPic(this.TomatoStatus);
        }

        private void SetTooltipContent(TimeSpan tomatoTimeLength, TimeSpan last)
        {

            string format = @"hh\:mm\:ss";
            TaskbarIcon.ToolTipText = $"Tomato time: {tomatoTimeLength.ToString(format)} \nTomato last: {last.ToString(format)}";
            if (this.baloon != null)
            {
                this.baloon.TomatoTime = $"Tomato time: {tomatoTimeLength.ToString(format)} Tomato last: {last.ToString(format)}";
            }
        }

        void IEvent<SetTooltipContentEvent>.OnEvent(SetTooltipContentEvent @event)
        {
            var last = @event.LastTomatooReciveLength;
            //if (last > Consts.BreakLength)
            //{
            //    ShowBallon(Properties.Resources.FinishIdle, TomatoStatus.Red);
            //}
            SetTooltipContent(@event.TomatoTimeLength, last);
        }

        void IEvent<CloseBalonEvent>.OnEvent(CloseBalonEvent @event)
        {
            TaskbarIcon.CloseBalloon();
        }

        public void OnEvent(TomatoExceedEvent @event)
        {
            if (TomatoExceedEventShowed == false)
            {
                ShowBallon();
                ChangeIconPic(TomatoDisplayStatus.WorkExceed);
                TomatoExceedEventShowed = true;
            }
        }

        public void OnEvent(IdleExceedEvent @event)
        {
            if (IdleExceedEventShowed == false)
            {
                IdleExceedEventShowed = true;
                ChangeIconPic(TomatoDisplayStatus.IdleExceed);
                ShowBallon();
            }
        }

        public void OnEvent(TomatoFinishEvent @event)
        {
            this.Tomato = @event.Tomato;
            ChangeIconPic(TomatoDisplayStatus.Idle);
            ShowBallon();
            this.IdleTimer.Run();
            this.TomatoTimer.Stop();
        }   
    }
}
