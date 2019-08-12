using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductivityTools.GetTask3.App.Commands;
using ProductivityTools.GetTask3.App.Queries;
using ProductivityTools.GetTask3.CommonConfiguration;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.Contract.Requests;
using ProductivityTools.GetTask3.CoreObjects;

namespace ProductivityTools.GetTask3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        ITaskQueries Queries;
        IGTaskCommands Commands;

        public TaskController(ITaskQueries gTaskAppQuery, IGTaskCommands gTaskApp)
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
        [Route(Consts.TodayList)]
        public ElementView GetTasks([FromBody]ListRequest request=null)
        {
            //pw: perform mapping in this layer
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
        public void Finish([FromBody] FinishRequest request)
        {
            Commands.Finish(request.ElementId);
        }

        [HttpPost]
        [Route("Undone")]
        public void Undone([FromBody] UndoneRequest request)
       {
            Commands.Undone(request.ElementId);
        }

        [HttpPost]
        [Route("Delay")]
        public void Delay([FromBody] DelayItemRequest delayItem)
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