using ProductivityTools.GetTask3.TomatoTray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomatoesTray.Events
{
    class ChangeTaskBarIconPicEvent : BaseEvent
    {
       public TomatoStatus IconType { get; set; }
    }
}
