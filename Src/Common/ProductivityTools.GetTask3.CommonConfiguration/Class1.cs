using System;

namespace ProductivityTools.GetTask3.CommonConfiguration
{
    public class Consts
    {
        public const string TodayList = "TodayList";
        public const string HttpAddress = @"http://localhost:7000/";
        public static string EndpointAddress
        {
            get
            {
                return $"{HttpAddress}api/";
            }
        }
    }
}
