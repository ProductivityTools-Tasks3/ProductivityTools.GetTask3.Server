using AutoMapper;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Queries
{
    public interface IDefinedTaskQueries
    {
        List<Domain.DefinedElementGroup> GetDefinedTaskGroupsForBag(int? bagId, bool includeDetails);
        Domain.DefinedElementGroup GetDefinedTaskGroup(int bagid, string definedTaskGroupName);
    }

    public class DefinedTaskQueries : IDefinedTaskQueries
    {
        private readonly IMapper _mapper;
        ITaskRepository _taskRepository;
        IDefinedTaskRepository _definedTaskRepository;

        public DefinedTaskQueries(ITaskRepository taskRepository, IDefinedTaskRepository definedtaskrepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _definedTaskRepository = definedtaskrepository;
        }

        public List<Domain.DefinedElementGroup> GetDefinedTaskGroupsForBag(int? bagId, bool includeDetails=false)
        {
            var result = new List<Domain.DefinedElementGroup>();
            var childbags = _taskRepository.GetTaskBags(bagId);
            foreach (var bag in childbags)
            {
                var definedTask = _definedTaskRepository.GetForBag(bag.ElementId, includeDetails);
                result.AddRange(definedTask);
            }
            return result;
        }

        public Domain.DefinedElementGroup GetDefinedTaskGroup(int bagid, string definedTaskGroupName)
        {
            var r = _definedTaskRepository.GetByName(bagid, definedTaskGroupName);
            return r;
        }
    }
}
