using System;

namespace ProductivityTools.GetTask3.CommonConfiguration
{
    public class Consts
    {
        public const string Task = "Task";
        public const string Move = "Move";
        public const string TodayList = "TodayList";
        public const string AddToTomatoById = "AddToTomatoById";
        public const string AddToTomatoByName = "AddToTomatoByName";
        public const string FinishTomato = "FinishTomato";
        public const string GetTomato = "GetTomato";

        public const string HttpAddress = @"http://localhost:5507/";
        public static string EndpointAddress
        {
            get
            {
                return $"{HttpAddress}api/";
            }
        }
        public static string TomatoHubLocation = "TomatoHub";
        public static string TomatoHubEndLocation = $"/{TomatoHubLocation}";
        public static string TomatoHubAddress
        {
            get
            {
                return $"{HttpAddress}{TomatoHubLocation}";
            }
        }

        public static TimeSpan BreakLength = TimeSpan.FromSeconds(5*60);

        public static TimeSpan TomatoLength = TimeSpan.FromSeconds(25*60);
    }
}
