using ProductivityTools.GetTask3.TomatoTray;
using ProductivityTools.GetTask3.TomatoTray.EventAggregator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace TomatoesTray
{
    /// <summary>
    /// Interaction logic for Balloon.xaml
    /// </summary>
    public partial class Balloon : UserControl
    {
        EventAggregator EventAggregator;
        private string text;
        private string Text
        {
            get
            {
                return text;
            }
            set
            {
                this.text = value;
                this.BallonTextTXT.Text = this.text;
            }
        }

        public string TomatoTime
        {
            set
            {
                this.BallonTimeTXT.Text = value;
            }
        }

        public Balloon()
        {
            InitializeComponent();
        }

        public Balloon(EventAggregator EventAggregator, string text, TomatoStatus status) : this()
        {
            this.EventAggregator = EventAggregator;
            this.Text = text;
            ChangeIconPic(status);
        }

        private void ChangeIconPic(TomatoStatus iconType)

        {
            string iconPath = string.Empty;
            Color color = (Color)ColorConverter.ConvertFromString("#FFC0C0C0");
            switch (iconType)
            {
                case TomatoStatus.Work:
                    iconPath = @"pack://application:,,,/Images/TomatoGreen.png";
                    color = (Color)ColorConverter.ConvertFromString("#FFBFFF77");
                    this.FinishTomato.Visibility = Visibility.Visible;
                    break;
                case TomatoStatus.Idle:
                    iconPath = @"pack://application:,,,/Images/TomatoGray.png";
                    color = (Color)ColorConverter.ConvertFromString("#FFC0C0C0");
                    this.FinishTomato.Visibility = Visibility.Hidden;
                    break;
                case TomatoStatus.WorkExceed:
                    iconPath = @"pack://application:,,,/Images/TomatoRed.png";
                    color = (Color)ColorConverter.ConvertFromString("#FFF0005A");
                    this.FinishTomato.Visibility = Visibility.Visible;
                    break;
                case TomatoStatus.IdleExceed:
                    iconPath = @"pack://application:,,,/Images/TomatoDarkGray.png";
                    color = (Color)ColorConverter.ConvertFromString("#FF000000");
                    this.FinishTomato.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }

            var uri = new Uri(iconPath);
            var bitmap = new BitmapImage(uri);
            this.image.Source = bitmap;
            //Color = (Color)ColorConverter.ConvertFromString("#FFF0005A") }); red
            LinearGradientBrush LinearGradientBrush = this.ColoredRectangle.Fill as LinearGradientBrush;
            LinearGradientBrush.GradientStops.Clear();
            LinearGradientBrush.GradientStops.Add(new GradientStop() { Color = color });
            LinearGradientBrush.GradientStops.Add(new GradientStop() { Color = (Color)ColorConverter.ConvertFromString("#FFFFFFFF"), Offset = 1 });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //pw:to be done
            //Tomatom tm = new TaskManager();
            //tm.FinishTomato();
            //this.EventAggregator.PublishEvent(new CloseBalonEvent());
        }
    }
}