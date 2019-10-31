using MediatR;
using ProductivityTools.GetTask3.App.Queries;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.Domain.Events;
using ProductivityTools.GetTask3.SignalRHubs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Handlers
{
    public class TomatoAddedHandler : INotificationHandler<TomatoAdded>
    {
        TomatoHub Hub;
        ITaskQueries Queries;

        public TomatoAddedHandler(ITaskQueries queries, TomatoHub hub)
        {
            this.Queries = queries;
            this.Hub = hub;
        }

        public Task Handle(TomatoAdded notification, CancellationToken cancellationToken)
        {
            TomatoView tomato = this.Queries.GetTomato();
          //  var s = tomato.TomatoId.ToString();
            Hub.NewTomato(tomato);
            return Task.CompletedTask;
        }
    }
}
