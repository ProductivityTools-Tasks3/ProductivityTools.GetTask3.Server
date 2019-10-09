using AutoMapper;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;

namespace ProductivityTools.GetTask3.App.Queries
{
    public interface ITaskQueries
    {
        ElementView GetTaskList(int? bagId = null);
        int? GetParent(int elementId);
        TomatoView GetTomato();
    }

    public class TaskQueries : ITaskQueries
    {
        private readonly IMapper _mapper;
        ITaskRepository _taskRepository;
        ITomatoRepository _tomatoRepository;

        public TaskQueries(ITaskRepository taskRepository, ITomatoRepository tomatoRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _tomatoRepository = tomatoRepository;
            _mapper = mapper;
        }


        //pw:change it to handlers
        public ElementView GetTaskList(int? bagId=null)
        {
            Domain.Element element = _taskRepository.GetStructure(bagId);
            ElementView st = _mapper.Map<Domain.Element,ElementView>(element);
            return st;
        }

        public int? GetParent(int elementId)
        {
            var element=_taskRepository.Get(elementId);
            return element?.ParentId;
        }

        public TomatoView GetTomato()
        {
            var tomato = _tomatoRepository.GetCurrent();
            TomatoView result = _mapper.Map<Domain.Tomato, TomatoView>(tomato);
            return result;
        }
    }
}
