using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.CqrsController.Registration
{
    public class QueryRegistration : CqrsOperationRegistration
    {
        public Type QueryType { get; }
        public Type ResponseType { get; }
        public Type QueryResultType { get; }

        public QueryRegistration(
            Type requestedType,
            Type queryType,
            Type responseType,
            Type queryResultType,
            Roles roles
            ) : base(requestedType, roles)
        {
            this.QueryType = queryType;
            this.ResponseType = responseType;
            this.QueryResultType = queryResultType;
        }
    }
}
