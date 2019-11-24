using Microsoft.AspNetCore.SignalR;
using ProductivityTools.GetTask3.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.SignalRHubs
{
    public class TomatoHub : Hub
    {
        IHubContext<TomatoHub> context;

        public TomatoHub(IHubContext<TomatoHub> context)
        {
            this.context = context;
        }

        public void NewTomato(TomatoView tomato)
        {
            this.context.Clients.All.SendAsync("NewTomato", tomato);
        }


        public void FinishTomato(TomatoView tomato)
        {
            this.context.Clients.All.SendAsync("FinishTomato", tomato);
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
