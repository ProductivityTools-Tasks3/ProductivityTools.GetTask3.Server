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
    }

    public class TaskQueries : ITaskQueries
    {
        private readonly IMapper _mapper;
        ITaskRepository _taskRepository;

        public TaskQueries(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }


        //pw:change it to handlers
        public ElementView GetTaskList(int? bagId=null)
        {
            Domain.Element element = _taskRepository.GetStructure(bagId);
            ElementView st = _mapper.Map<Domain.Element,ElementView>(element);

            //element.Elements.ForEach(x => st.Items.Add(_mapper.Map<ItemView>(x)));

            //new ItemView() {
            //    Name = x.Name,
            //    OrderId =(x as Domain.Element).OrderId ,
            //Status= (x as Domain.Element).Status.ToString()}));
            return st;
        }

        public int? GetParent(int elementId)
        {
            var element=_taskRepository.Get(elementId);
            return element?.ParentId;
        }
    }
}
