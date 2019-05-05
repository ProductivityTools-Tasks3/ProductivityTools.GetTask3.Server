using ProductivityTools.Cqrs.Dispatcher;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.Cqrs
{
    public abstract class BaseCqrsConnector<TOptions>
         where TOptions : BaseConnectorOptions, new()
    {
        private readonly ICqrsDispatcher _cqrsDispatcher;
        protected readonly ConnectorConfiguration<TOptions> Configuration;

        protected BaseCqrsConnector(
            ConnectorConfiguration<TOptions> configuration,
            ICqrsDispatcher cqrsDispatcher)
        {
            Configuration = configuration;
            _cqrsDispatcher = cqrsDispatcher;
        }
    }
}
