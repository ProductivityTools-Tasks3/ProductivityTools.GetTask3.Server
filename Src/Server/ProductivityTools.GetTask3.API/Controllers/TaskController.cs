using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductivityTools.GetTask3.API.Models;
using ProductivityTools.GetTask3.App.Commands;
using ProductivityTools.GetTask3.App.Queries;

namespace ProductivityTools.GetTask3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        IGTaskAppQuery GTaskAppQuery;
        IGTaskApp GTaskApp;

        public TaskController(IGTaskAppQuery gTaskAppQuery, IGTaskApp gTaskApp)
        {
            this.GTaskAppQuery = gTaskAppQuery;
            this.GTaskApp = gTaskApp;
        }

        // GET api/values
        [HttpGet]
        [Route("Demo")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [Route("List")]
        public ItemView GetTasks([FromBody]int? bagId = null)
        {
            var x = GTaskAppQuery.GetTaskList(bagId);
            return x;
        }

        [HttpPost]
        [Route("Add")]
        public void Add([FromBody] ElementRequest request)
        {
            GTaskApp.Add(request.Name, request.BagId);
        }

        [HttpPost]
        [Route("AddBag")]
        public void AddBag([FromBody] ElementRequest request)
        {
            GTaskApp.AddBag(request.Name, request.BagId);
        }


        [HttpPost]
        [Route("Finish")]
        public void Finish([FromBody] int orderId, int? bagId = null)
        {
            GTaskApp.Finish(orderId, bagId);
        }
    }
}