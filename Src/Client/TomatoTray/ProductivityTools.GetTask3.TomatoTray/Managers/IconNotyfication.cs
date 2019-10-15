using Hardcodet.Wpf.TaskbarNotification;
using ProductivityTools.GetTask3.TomatoTray;
using ProductivityTools.GetTask3.TomatoTray.EventAggregator;
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

    class IconNotyfication : IEvent<ChangeTaskBarIconPicEvent>, IEvent<ShowBalonEvent>, IEvent<SetTooltipContentEvent>, IEvent<CloseBalonEvent>
    {
        public TaskbarIcon TaskbarIcon { get; private set; }
        EventAggregator EventAggregator { get; set; }
        private Balloon baloon { get; set; }

        public IconNotyfication(EventAggregator EventAggregator)
        {
            TaskbarIcon = new TaskbarIcon();
            TaskbarIcon.Icon = new System.Drawing.Icon(@".\Icons\TomatoGray.ico");
            TaskbarIcon.ContextMenu = new System.Windows.Controls.ContextMenu();
            TaskbarIcon.ContextMenu.Items.Add(new MenuItem() { Header = "Close", Command = new DelegateCommand { CommandAction = () => Application.Current.Shutdown() } });
            TaskbarIcon.DoubleClickCommand = DoubleClick;

            this.EventAggregator = EventAggregator;
            this.EventAggregator.Subscribe(this);
        }

        ICommand DoubleClick
        {
            get
            {
                return new DelegateCommand() { CommandAction = () => this.EventAggregator.PublishEvent(new ClearTomatoDisplayList()) };
            }
        }

        private void ChangeIconPic(TomatoStatus iconType)
        {
            string iconPath = string.Empty;
            switch (iconType)
            {
                case TomatoStatus.Work:
                    iconPath = @".\Icons\TomatoGreen.ico";
                    break;
                case TomatoStatus.Idle:
                    iconPath = @".\Icons\TomatoGray.ico";
                    break;
                case TomatoStatus.WorkExceed:
                    iconPath = @".\Icons\TomatoRed.ico";
                    break;
                case TomatoStatus.IdleExceed:
                    iconPath = @".\Icons\TomatoDarkGray.ico";
                    break;
                default:
                    break;
            }

            TaskbarIcon.Icon = new System.Drawing.Icon(iconPath);
        }

        private void ShowBallon(string text, TomatoStatus status)
        {
            this.baloon =new Balloon(this.EventAggregator, text, status);
            TaskbarIcon.ShowCustomBalloon(baloon, PopupAnimation.Fade, 15000);
        }

        void IEvent<ChangeTaskBarIconPicEvent>.OnEvent(ChangeTaskBarIconPicEvent @event)
        {
            ChangeIconPic(@event.IconType);
        }

        void IEvent<ShowBalonEvent>.OnEvent(ShowBalonEvent @event)
        {
            ShowBallon(@event.Text, @event.Status);
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
    }
}
