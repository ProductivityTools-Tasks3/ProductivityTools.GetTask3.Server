using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using ProductivityTools.GetTask3.CommonConfiguration;

namespace ProductivityTools.GetTask3.SignalRHubs
{
   public static class App
    {
        public static void ConfigureSignalR(this IApplicationBuilder app)
        {
            //pw: singalr
            app.UseSignalR(route =>
            {
                route.MapHub<TomatoHub>(Consts.TomatoHubEndLocation);
            });
        }
    }
}
