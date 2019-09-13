using System;

namespace ProductivityTools.GetTask3.CommonConfiguration
{
    public class Consts
    {
        public const string Task = "Task";
        public const string TodayList = "TodayList";
        public const string AddToTomato = "AddToTomato";
        public const string FinishTomato = "FinishTomato";

        public const string HttpAddress = @"http://localhost:4501/";
        public static string EndpointAddress
        {
            get
            {
                return $"{HttpAddress}api/";
            }
        }
    }
}
