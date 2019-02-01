using ProductivityTools.GetTask3.CqrsController.Registration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.CqrsController
{
    public abstract class CqrsModule
    {
        public string Name;

        Dictionary<string, CqrsOperationRegistration> registartions = new Dictionary<string, CqrsOperationRegistration>();

        public CqrsModule(string module)
        {
            Name = module.ToLower();
        }

        protected void WireUpQuery<TRequest,TQuery,TResponse,TQueryResult>(Roles roles=null)
        {
            string queryName = GetOperationName<TRequest>();
            var queryRegistration = new QueryRegistration(
                typeof(TRequest),
                typeof(TQuery),
                typeof(TQueryResult),
                typeof(TQueryResult),
                roles);
            registartions.Add(queryName, queryRegistration);
        }

        private string GetOperationName<TRequest>()
        {
            string r=nameof(TRequest).Replace("Request", string.Empty).ToLower();
            return r;
        }
    }
}
