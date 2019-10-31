using ProductivityTools.GetTask3.TomatoTray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomatoesTray.Events
{
    class TomatoInfoFlyInEvent : BaseEvent
    {
        public Tomato Tomato { get; set; }

        private TomatoInfoFlyInEvent()
        {

        }

        public TomatoInfoFlyInEvent(Tomato tomato)
        {
            this.Tomato = tomato;
        }
    }
}
