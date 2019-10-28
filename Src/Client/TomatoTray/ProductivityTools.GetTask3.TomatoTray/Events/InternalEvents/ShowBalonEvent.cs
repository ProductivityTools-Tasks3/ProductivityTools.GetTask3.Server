using ProductivityTools.GetTask3.TomatoTray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomatoesTray.Events
{
    class ShowBalonEvent : BaseEvent
    {
        private ShowBalonEvent()
        { }

        public ShowBalonEvent(string text, TomatoDisplayStatus status)
        {
            this.Text = text;
            this.Status = status;
         
        }

        public string Text { get; set; }
        public TomatoDisplayStatus Status { get; set; }
        public SetTooltipContentEvent time { get; set; }
    }
}
