using ProductivityTools.GetTask3.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.IntegrationTests
{
    class ConfigTest : IConfig
    {
        public string ConnectionString => @"Server=.\sql2017;Database=GetTask3Test;Integrated Security=True";
    }
}
