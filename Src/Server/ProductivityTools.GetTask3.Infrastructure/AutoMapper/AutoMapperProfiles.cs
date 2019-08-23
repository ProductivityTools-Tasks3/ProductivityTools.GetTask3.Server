using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.AutoMapper
{
    public class ElementProfile:Profile
    {
        public ElementProfile()
        {
            CreateMap<Infrastructure.Element, Domain.Element>().ReverseMap();
        }
    }
}
