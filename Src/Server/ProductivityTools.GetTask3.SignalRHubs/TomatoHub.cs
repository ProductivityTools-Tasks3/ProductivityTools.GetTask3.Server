using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.SignalRHubs
{
    public class TomatoHub : Microsoft.AspNetCore.SignalR.Hub
    {
        IHubContext<TomatoHub> context;

        public TomatoHub(IHubContext<TomatoHub> context)
        {
            this.context = context;
        }

        public void NewTomato(string tomatoName)
        {
            this.context.Clients.All.SendAsync("NewTomato", tomatoName);
        }


        public void FinishTomato()
        {
            this.context.Clients.All.SendAsync("FinishTomato");
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
