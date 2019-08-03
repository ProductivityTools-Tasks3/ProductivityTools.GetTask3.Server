using AutoMapper;
using ProductivityTools.GetTask3.Contract.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.API.Automapper
{
    public class DefinedTaskProfile : Profile
    {
        public DefinedTaskProfile()
        {
            CreateMap<Domain.DefinedElementGroup, DefinedTaskGroupView>();
            CreateMap<Domain.DefinedElement, DefinedTask>();
        }
    }
}
