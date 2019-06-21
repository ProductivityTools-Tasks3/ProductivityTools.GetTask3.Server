using AutoMapper;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;

namespace ProductivityTools.GetTask3.App.Queries
{
    public class GTaskAppQuery : IGTaskAppQuery
    {
        private readonly IMapper _mapper;

        ITaskRepository _taskRepository;
        public GTaskAppQuery(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }


        //pw:change it to handlers
        public ItemView GetTaskList(int? bagId=null)
        {
            Domain.Element element = _taskRepository.GetStructure(bagId);
            ItemView st = _mapper.Map<Element,ItemView>(element);

            //element.Elements.ForEach(x => st.Items.Add(_mapper.Map<ItemView>(x)));

            //new ItemView() {
            //    Name = x.Name,
            //    OrderId =(x as Domain.Element).OrderId ,
            //Status= (x as Domain.Element).Status.ToString()}));
            return st;
        }
    }
}
