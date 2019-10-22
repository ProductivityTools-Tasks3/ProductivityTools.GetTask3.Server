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
        public string Text { get; set; }
        public TomatoStatus Status { get; set; }
        public SetTooltipContentEvent time { get; set; }
    }
}
