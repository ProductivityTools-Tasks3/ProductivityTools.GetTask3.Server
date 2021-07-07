using AutoMapper;
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
    public class TomatoFinishedHandler : INotificationHandler<TomatoFinished>
    {
        TomatoHub Hub;
        ITaskQueries Queries;
        private readonly IMapper Mapper;

        public TomatoFinishedHandler(ITaskQueries queries, TomatoHub hub, IMapper mapper)
        {
            this.Queries = queries;
            this.Hub = hub;
            this.Mapper = mapper;
        }

        public Task Handle(TomatoFinished notification, CancellationToken cancellationToken)
        {
            TomatoView tomatoView = Mapper.Map<Domain.Tomato,TomatoView>(notification.Tomato);
            Hub.FinishTomato(tomatoView);
            return Task.CompletedTask;
        }
    }
}
