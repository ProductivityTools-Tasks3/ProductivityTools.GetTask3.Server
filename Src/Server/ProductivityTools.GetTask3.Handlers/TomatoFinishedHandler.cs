using MediatR;
using ProductivityTools.GetTask3.App.Queries;
using ProductivityTools.GetTask3.Domain.Events;
using ProductivityTools.GetTask3.SignalRHubs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Handlers
{
    public class TomatoFinishedHandler : INotificationHandler<TomatoFinished>
    {
        TomatoHub Hub;
        ITaskQueries Queries;

        public TomatoFinishedHandler(ITaskQueries queries, TomatoHub hub)
        {
            this.Queries = queries;
            this.Hub = hub;
        }

        public Task Handle(TomatoFinished notification, CancellationToken cancellationToken)
        {
            Hub.FinishTomato();
            return Task.CompletedTask;
        }
    }
}
