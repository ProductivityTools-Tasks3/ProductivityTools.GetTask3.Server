using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.Gateway.Swagger.Settings
{
    public class SwaggerSettings
    {
        public string EnvironmentName { get; set; }
        public string Title { get; set; }
        public string ProjectName { get; set; }
        public string Company { get; set; }
        public DateTime ModificationDate { get; set; }
        public string Version { get; set; }
        public JsonSerializerSettings JsonSerializerSettings { get; set; }
    }
}
