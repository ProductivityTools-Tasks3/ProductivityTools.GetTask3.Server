using ProductivityTools.Cqrs.Registration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.Cqrs.Dispatcher
{
    public interface ICqrsDispatcher
    {
       Task<object> ExecuteOperation(CqrsOperationRegistration registration, object request);
    }
}
