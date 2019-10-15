using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.API
{
    public class TomatoHub : Microsoft.AspNetCore.SignalR.Hub
    {
        IHubContext<TomatoHub> context;

        public TomatoHub(IHubContext<TomatoHub> context)
        {
            this.context = context;
        }

        public void NewTomato()
        {
            Clients.All.SendAsync("NewTomato", "fdsa");
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
