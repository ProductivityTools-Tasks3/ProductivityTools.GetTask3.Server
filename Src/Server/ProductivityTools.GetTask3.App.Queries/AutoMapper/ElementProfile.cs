using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Queries.AutoMapper
{
    public class ElementProfile : Profile
    {
        public ElementProfile()
        {
            CreateMap<Domain.Element, ItemView>();
        }
    }
}

