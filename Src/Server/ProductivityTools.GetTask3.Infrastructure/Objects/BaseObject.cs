using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.Objects
{
    public class BaseObject
    {
        public List<INotification> Notifications { get; set; }
    }
}
