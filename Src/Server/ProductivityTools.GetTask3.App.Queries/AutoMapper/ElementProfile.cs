using AutoMapper;
using ProductivityTools.GetTask3.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Queries.AutoMapper
{
    public class ElementProfile : Profile
    {
        public ElementProfile()
        {

            CreateMap<Domain.Element, ElementView>();
            CreateMap<Domain.Tomato, TomatoView>();
            //    ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements));
            //CreateMap<List<Domain.Element>, List<ItemView>>();

        }
    }
}

