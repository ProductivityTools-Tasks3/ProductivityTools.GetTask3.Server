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
using ProductivityTools.GetTask3.Contract.Responses;
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
        public ElementView GetTasks([FromBody]ListRequest request = null)
        {
            //pw: perform mapping in this layer
            var x = Queries.GetTaskList(request?.ElementId, request.Path);
            return x;
        }
        [HttpPost]
        [Route(Consts.GetRoot)]
        public int? GetRoot([FromBody]GetRootRequest request)
        {
            var x = Queries.GetRootRequest(request.ElementId, request.Path);
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
        [Route(Consts.Finish)]
        public void Finish([FromBody] FinishRequest request)
        {
            Commands.Finish(request.ElementId);
        }


        [HttpPost]
        [Route(Consts.Start)]
        public void Start([FromBody] StartRequest request)
        {
            Commands.Start(request.ElementId);
        }

        [HttpPost]
        [Route("Undone")]
        public void Undone([FromBody] UndoneRequest request)
        {
            Commands.Undone(request.ElementId);
        }

        [HttpPost]
        [Route(Consts.Delay)]
        public void Delay([FromBody] DelayItemRequest delayItem)
        {
            Commands.Delay(delayItem.ElementId, delayItem.InitializationDate);
        }

        [HttpPost]
        [Route(Consts.Delete)]
        public void Delay([FromBody] int elementId)
        {
            Commands.Delete(elementId);
        }

        [HttpPost]
        [Route(Consts.GetParent)]
        public int? GetParent([FromBody] int elementId)
        {
            var r = Queries.GetParent(elementId);
            return r;
        }

        [HttpPost]
        [Route(Consts.AddToTomatoById)]
        public void AddToTomato([FromBody] AddToTomatoByIdRequest request)
        {
            Commands.AddToTomato(request.ElementItems.ToList());
        }

        [HttpPost]
        [Route(Consts.AddToTomatoByName)]
        public void AddToTomatoByName([FromBody] AddToTomatoByNameRequest request)
        {
            Commands.AddToTomato(request.TaskName, request.ParentId);
        }

        [HttpPost]
        [Route(Consts.FinishTomato)]
        public void FinishTomato([FromBody] FinishTomatoRequest request)
        {
            Commands.FinishTomato(request.FinishAlsoTasks);
        }

        [HttpPost]
        [Route(Consts.GetTomato)]
        public TomatoView GetTomato()
        {
            var r = Queries.GetTomato();
            return r;
        }

        [HttpPost]
        [Route(Consts.GetTomatoReport)]
        public TomatoReportView TomatoReport(GetTomatoReportRequest request)
        {
            TomatoReportView r = Queries.GetTomatoReport(request.Date);
            //r.Tomatoes = new List<TomatoView>();
            //r.Tomatoes.Add(new TomatoView() { Created = DateTime.Now.AddMinutes(-20), Finished = DateTime.Now });
            //r.Tomatoes.Add(new TomatoView() { Created = DateTime.Now.AddMinutes(-40), Finished = DateTime.Now.AddMinutes(-20) });

            return r;
        }


        [HttpPost]
        [Route(Consts.Move)]
        public void Move(MoveRequest moveRequest)
        {
            Commands.Move(moveRequest.ElementIds, moveRequest.Target);
        }
    }
}