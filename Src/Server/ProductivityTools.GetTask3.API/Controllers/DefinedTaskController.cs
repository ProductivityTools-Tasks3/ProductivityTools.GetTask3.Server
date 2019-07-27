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
        IDefinedTaskQueries _queries;
        IMapper _mapper;

        public DefinedTaskController(IDefinedTaskQueries queries, IMapper mapper)
        {
            this._queries = queries;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("List")]
        public DefinedTaskView GetDefinedTasks([FromBody]ListDefinedTaskRequest request = null)
        {
            var definedTask = _queries.GetDefinedTaskForBag(request?.BagId, request.IncudeDetails);
            DefinedTaskView result = new DefinedTaskView();
            result.DefinedTasks = _mapper.Map<List<DefinedTaskGroup>>(definedTask);
            //definedTask.ForEach(x => result.DefinedTasks.Add(new DefinedTaskGroup() { Name = x.Name, BagName = x.BagId.ToString() }));
            return result;
        }
    }
}
