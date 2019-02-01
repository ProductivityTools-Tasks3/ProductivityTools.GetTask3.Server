using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.CqrsController.Registration
{
    public abstract class CqrsOperationRegistration
    {
        public Type RequestedType;
        public Roles Roles;

        public CqrsOperationRegistration(Type requestedType, Roles roles)
        {
            RequestedType = requestedType;
            Roles = roles ?? Roles.AnyRole;
        }
    }
}
