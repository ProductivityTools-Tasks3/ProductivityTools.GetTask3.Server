using Microsoft.AspNetCore.SignalR.Client;
using ProductivityTools.GetTask3.CommonConfiguration;
using ProductivityTools.GetTask3.Contract.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TomatoesTray.Events;

namespace ProductivityTools.GetTask3.TomatoTray
{
    public static class SingnalRConnector
    {
        public static EventAggregator.EventAggregator EventAggregator = new EventAggregator.EventAggregator();
        static HubConnection connection;
        private static string URL = Consts.TomatoHubAddress;

        public static void Connect(Action<string> logger)
        {

            Func<TomatoView, Tomato> createTomato = (tomatoView) =>
            {
                var tomato = new Tomato();
                tomato.Status = tomatoView.Status;
                tomato.Name = tomatoView.Elements.Select(x => x.Name).Aggregate((a, b) => a + Environment.NewLine + b);
                tomato.CreatedDate = tomatoView.Created;
                tomato.FinishedDate = tomatoView.Finished;
                return tomato;
            };

            try
            {
                connection = new HubConnectionBuilder().WithUrl(URL).Build();
                connection.On<TomatoView>("NewTomato", tomatoView =>
                {
                    logger("Connection on NewTomato");
                    var name = tomatoView.Elements.Select(x => x.Name).Aggregate((a, b) => a + Environment.NewLine + b);
                    EventAggregator.PublishEvent(new TomatoInfoFlyInEvent(createTomato(tomatoView)));
                });

                connection.On<TomatoView>("FinishTomato", (tomatoView) =>
                {
                    logger("OnFinishTomato");
                    EventAggregator.PublishEvent(new TomatoFinishEvent(createTomato(tomatoView)));
                });

                connection.Closed += Connection_Closed;

                Thread.Sleep(2000);
                connection.StartAsync();
                logger("Its seems that connection is sucesss");
            }
            catch (Exception ex)
            {
                logger("Exception during connection");
                throw ex;
            }
        }

        internal static string GetConnectionState()
        {
            return connection.State.ToString();
        }

        private static Task Connection_Closed(Exception arg)
        {
            throw new NotImplementedException();
        }
    }
}

