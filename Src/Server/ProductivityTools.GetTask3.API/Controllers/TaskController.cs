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
        public ItemView GetTasks([FromBody]int? parentId = null)
        {
            var x = GTaskAppQuery.GetTaskList(parentId);
            return x;
        }

        [HttpPost]
        [Route("Add")]
        public void Add([FromBody] ElementRequest request)
        {
            GTaskApp.Add(request.Name, request.ParentId);
        }

        [HttpPost]
        [Route("AddBag")]
        public void AddBag([FromBody] ElementRequest request)
        {
            GTaskApp.AddBag(request.Name, request.ParentId);
        }


        [HttpPost]
        [Route("Finish")]
        public void Finish(int bagId)
        {
            //GTaskApp.Finish(orderId, bagId);
        }
    }
}