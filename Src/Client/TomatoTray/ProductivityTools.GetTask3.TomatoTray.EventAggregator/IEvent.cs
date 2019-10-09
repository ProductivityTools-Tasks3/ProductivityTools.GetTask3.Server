using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.TomatoTray.EventAggregator
{
    public interface IEvent<T>
    {
        void OnEvent(T @event);
    }
}
