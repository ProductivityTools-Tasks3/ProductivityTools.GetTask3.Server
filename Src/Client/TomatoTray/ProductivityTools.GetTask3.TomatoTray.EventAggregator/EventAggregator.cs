using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.TomatoTray.EventAggregator
{
    public class EventAggregator
    {
        Dictionary<Type, List<object>> EventsList = new Dictionary<Type, List<object>>();

        public void Subscribe(object o)
        {
            var eventTypes = o.GetType().GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEvent<>));
            foreach (Type eventType in eventTypes)
            {
                List<object> objectList;
                if (this.EventsList.TryGetValue(eventType, out objectList))
                {
                    objectList.Add(o);
                }
                else
                {
                    this.EventsList.Add(eventType.GenericTypeArguments[0], new List<object> { o });
                }
            }
        }

        public void PublishEvent<T>(T @event)
        {
            Type typeOfEventToPublish = typeof(T);
            List<object> objectList;
            if (this.EventsList.TryGetValue(typeOfEventToPublish, out objectList))
            {
                foreach (IEvent<T> @object in objectList)
                {
                    @object.OnEvent(@event);
                }
            }
        }
    }
}
