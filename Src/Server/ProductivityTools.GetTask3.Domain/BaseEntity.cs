using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Domain
{
    public class BaseEntity
    {
        public List<INotification> Notifications { get; set; }

        protected void AddNotification(INotification notification)
        {
            Notifications = Notifications ?? new List<INotification>();
            Notifications.Add(notification);
        }
    }
}
