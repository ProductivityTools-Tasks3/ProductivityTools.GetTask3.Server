using Microsoft.AspNetCore.Mvc;
using System;

namespace ProductivityTools.Cqrs.HttpConnector
{
    [ApiController]
    [Route("/rpc")]
    public class CqrsMediatorController : Controller
    {
        private readonly HttpToCqrsConnector _connector;

        public CqrsMediatorController(HttpToCqrsConnector connector)
        {
            _connector = connector;
        }
    }
}
