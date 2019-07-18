using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductivityTools.GetTask3.API.Models;
using ProductivityTools.GetTask3.App.Commands;
using ProductivityTools.GetTask3.App.Queries;
using ProductivityTools.GetTask3.Contract;

namespace ProductivityTools.GetTask3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        IGTaskAppQuery Queries;
        IGTaskApp Commands;

        public TaskController(IGTaskAppQuery gTaskAppQuery, IGTaskApp gTaskApp)
        {
            this.Queries = gTaskAppQuery;
            this.Commands = gTaskApp;
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
        public ElementView GetTasks([FromBody]ListRequest request=null)
        {
            var x = Queries.GetTaskList(request?.ParentId);
            return x;
        }

        [HttpPost]
        [Route("Add")]
        public void Add([FromBody] AddRequest request)
        {
            Commands.Add(request.Name, request.ParentId);
        }

        [HttpPost]
        [Route("AddBag")]
        public void AddBag([FromBody] AddRequest request)
        {
            Commands.AddBag(request.Name, request.ParentId);
        }


        [HttpPost]
        [Route("Finish")]
        public void Finish([FromBody] int elementId)
        {
            Commands.Finish(elementId);
        }

        [HttpPost]
        [Route("Undone")]
        public void Undone([FromBody] int elementId)
        {
            Commands.Undone(elementId);
        }

        [HttpPost]
        [Route("Delay")]
        public void Delay([FromBody] DelayItem delayItem)
        {
            Commands.Delay(delayItem.ElementId, delayItem.StartDate);
        }

        [HttpPost]
        [Route("GetParent")]
        public int? GetParent([FromBody] int elementId)
        {
            var r = Queries.GetParent(elementId);
            return r;
        }


    }
}