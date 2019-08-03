using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductivityTools.GetTask3.App.Commands;
using ProductivityTools.GetTask3.App.Queries;
using ProductivityTools.GetTask3.Contract.Requests;
using ProductivityTools.GetTask3.Contract.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefinedTaskController : ControllerBase
    {
        private readonly IDefinedTaskQueries _queries;
        private readonly IDefinedTaskCommands _commands;
        private readonly IMapper _mapper;

        public DefinedTaskController(IDefinedTaskQueries queries, IDefinedTaskCommands commands, IMapper mapper)
        {
            _queries = queries;
            _commands = commands;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("List")]
        public DefinedTaskView GetDefinedTasks([FromBody]ListDefinedTaskRequest request = null)
        {
            var definedTask = _queries.GetDefinedTaskGroupsForBag(request?.BagId, request.IncudeDetails);
            DefinedTaskView result = new DefinedTaskView();
            result.DefinedTasks = _mapper.Map<List<DefinedTaskGroupView>>(definedTask);
            //definedTask.ForEach(x => result.DefinedTasks.Add(new DefinedTaskGroup() { Name = x.Name, BagName = x.BagId.ToString() }));
            return result;
        }


        [HttpPost]
        [Route("GetDefinedTaskGroup")]
        public DefinedTaskGroupView GetDefinedTaskGroup([FromBody]GetDefinedTaskGroupRequest request = null)
        {
            Domain.DefinedElementGroup definedTaskGroup = _queries.GetDefinedTaskGroup(request.BagId, request.DefinedTaskGroupName);
            DefinedTaskGroupView result = _mapper.Map< DefinedTaskGroupView>(definedTaskGroup);
            return result;
        }

        [HttpPost]
        [Route("AddDefinedTasks")]
        public void AddDefinedTasks([FromBody]AddDefinedTasksRequest request)
        {
            _commands.AddDefinedTask(request.DefinedTaskId);
        }
    }
}
