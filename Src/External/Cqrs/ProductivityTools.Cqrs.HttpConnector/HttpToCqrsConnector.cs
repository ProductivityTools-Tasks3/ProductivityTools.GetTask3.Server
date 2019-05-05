using ProductivityTools.Cqrs.Dispatcher;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.Cqrs.HttpConnector
{
    public class HttpToCqrsConnector: BaseCqrsConnector<HttpToCqrsOptions>
    {
        public HttpToCqrsConnector(
            ConnectorConfiguration<HttpToCqrsOptions> configuration,
            ICqrsDispatcher cqrsDispatcher)
            : base(configuration, cqrsDispatcher)
        {
        }

    }

    public class HttpToCqrsOptions : BaseConnectorOptions
    {
    }
}
