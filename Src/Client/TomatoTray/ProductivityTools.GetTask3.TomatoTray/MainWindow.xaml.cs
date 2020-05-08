using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TomatoesTray.Events;
using ProductivityTools.GetTask3.TomatoTray.Properties;

namespace ProductivityTools.GetTask3.TomatoTray
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
          
        }

        private void FillLabel(string s)
        {
            this.LogLabel.Content += s;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SingnalRConnector.Connect(FillLabel);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.LogLabel.Content+= SingnalRConnector.GetConnectionState();
        }
    }
}
