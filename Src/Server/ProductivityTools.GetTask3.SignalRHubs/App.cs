using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using ProductivityTools.GetTask3.CommonConfiguration;

namespace ProductivityTools.GetTask3.SignalRHubs
{
   public static class App
    {
        public static void ConfigureSignalR(this IApplicationBuilder app)
        {
            app.UseSignalR(route =>
            {
                route.MapHub<TomatoHub>(Consts.TomatoHubEndLocation);
            });
        }
    }
}
