using System;

namespace ProductivityTools.GetTask3.CommonConfiguration
{
    public class Consts
    {
        public const string Task = "Task";
        public const string Finish = "Finish";
        public const string Start = "Start";
        public const string Move = "Move";
        public const string Delay = "Delay";
        public const string Delete = "Delete";
        public const string TodayList = "TodayList";
        public const string GetRoot = "GetRoot";
        public const string GetParent = "GetParent";
        public const string AddToTomatoById = "AddToTomatoById";
        public const string AddToTomatoByName = "AddToTomatoByName";
        public const string FinishTomato = "FinishTomato";
        public const string GetTomato = "GetTomato";
        public const string GetTomatoReport = "GetTomatoReport";

        public const string HttpAddress = @"http://localhost:5513/";
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
