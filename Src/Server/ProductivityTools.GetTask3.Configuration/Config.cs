using System;

namespace ProductivityTools.GetTask3.Configuration
{
    public class Config : IConfig
    {
        public string ConnectionString
        {
            get
            {
                return @"Server=.\sql2017;Database=GetTask3;Integrated Security=True";
            }
        }
    }
}
