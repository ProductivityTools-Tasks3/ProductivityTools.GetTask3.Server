using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Client
{
    public class LoggingHandler : DelegatingHandler
    {
        Action<string> Log;
        public LoggingHandler(HttpMessageHandler innerHandler, Action<string> log)
            : base(innerHandler)
        {
            this.Log = log;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Log("Request:");
            Log(request.ToString());
            if (request.Content != null)
            {
                Log(await request.Content.ReadAsStringAsync());
            }
            Log("");

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Log("Response:");
            Log(response.ToString());
            if (response.Content != null)
            {
                Log(await response.Content.ReadAsStringAsync());
            }
            Log("");

            return response;
        }
    }
}
