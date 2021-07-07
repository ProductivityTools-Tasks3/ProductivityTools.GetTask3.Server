using Microsoft.AspNetCore.SignalR.Client;
using ProductivityTools.GetTask3.CommonConfiguration;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.Contract.Responses;
using ProductivityTools.GetTask3.TomatoTray.Managers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TomatoesTray;
using TomatoesTray.Events;

namespace ProductivityTools.GetTask3.TomatoTray
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // TomatoManager TomatoManager;
        EventAggregator.EventAggregator EventAggregator = new EventAggregator.EventAggregator();
        IconNotyfication IconNotyfication;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application.Current.MainWindow = null;
            //base.OnStartup(e);
            IconNotyfication = new IconNotyfication(EventAggregator);
            //   IconNotyfication.TaskbarIcon.ContextMenu.Items.Add(new MenuItem() { Header = "close", Command = ExitApplicationCommand });

            IconNotyfication.TaskbarIcon.DataContext = this;

            //  TomatoManager = new TomatoManager(EventAggregator);
            Connect(URL);
            ////this.EventAggregator.Subscribe(this);
            //WCFServer server = new WCFServer(this.EventAggregator);
            //server.Run();
        }

        HubConnection connection;
        private string URL = Consts.TomatoHubAddress;
        private void Connect(string uri)
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
                connection = new HubConnectionBuilder().WithUrl(uri).Build();
                connection.On<TomatoView>("NewTomato", tomatoView =>
                {
                    var name = tomatoView.Elements.Select(x => x.Name).Aggregate((a, b) => a + Environment.NewLine + b);
                    this.EventAggregator.PublishEvent(new TomatoInfoFlyInEvent(createTomato(tomatoView)));
                });

                connection.On<TomatoView>("FinishTomato", (tomatoView) =>
                {
                    this.EventAggregator.PublishEvent(new TomatoFinishEvent(createTomato(tomatoView)));
                });

                connection.Closed += Connection_Closed;

                Thread.Sleep(2000);
                connection.StartAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Task Connection_Closed(Exception arg)
        {
            throw new NotImplementedException();
        }
    }
}
