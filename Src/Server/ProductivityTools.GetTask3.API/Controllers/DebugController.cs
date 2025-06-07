using Microsoft.AspNetCore.Mvc;
using ProductivityTools.GetTask3.App.Queries;
using System;

namespace ProductivityTools.GetTask3.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebugController : Controller
    {
        private readonly IDebugQueries _debugQueries;
        public DebugController(IDebugQueries context)
        {
            this._debugQueries = context;
        }

        [HttpGet]
        [Route("Date")]
        public string Date()
        {
            return DateTime.Now.ToString();
        }

        [HttpGet]
        [Route("AppName")]
        public string AppName()
        {
            return "PTTask3";
        }

        [HttpGet]
        [Route("Hello")]
        public string Hello(string name)
        {
            return string.Concat($"Hello {name.ToString()} Current date:{DateTime.Now}");
        }

        [HttpGet]
        [Route("ServerName")]
        public string ServerName()
        {
            string server = this._debugQueries.GetServerName();
            return server;
        }
    }
}
