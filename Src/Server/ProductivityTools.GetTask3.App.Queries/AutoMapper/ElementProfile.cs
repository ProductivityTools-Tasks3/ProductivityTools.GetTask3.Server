using AutoMapper;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.Contract.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Queries.AutoMapper
{
    public class ElementProfile : Profile
    {
        public ElementProfile()
        {

            CreateMap<Infrastructure.Element, ElementView>();
            CreateMap<Domain.Tomato, TomatoView>();
            CreateMap<Domain.Element, TomatoElementView>();
            //    ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements));
            //CreateMap<List<Domain.Element>, List<ItemView>>();

        }
    }
}

