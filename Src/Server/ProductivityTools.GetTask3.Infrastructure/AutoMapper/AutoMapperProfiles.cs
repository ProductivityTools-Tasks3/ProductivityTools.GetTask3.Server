using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.AutoMapper
{
    public class ElementProfile:Profile
    {
        public ElementProfile()
        {
            CreateMap<Infrastructure.Element, Domain.Element>()
                .ForMember(dest => dest.Tomatoes, opt => opt.MapFrom(src => src.TomatoElements.Select(x => x.Tomato)));
            CreateMap<Domain.Element,Infrastructure.Element>()
                .ForMember(x=>x.TomatoElements, opt=>opt.MapFrom<CustomResolver>());
        }

        public class CustomResolver : IValueResolver<Domain.Element, Infrastructure.Element, List<TomatoElement>>
        {
            public List<TomatoElement> Resolve(Domain.Element source, Infrastructure.Element destination, List<TomatoElement> member, ResolutionContext context)
            {
                var result= new List<TomatoElement>();
                var tomatoElement = new TomatoElement();
                var tomato = new Tomato();
                tomatoElement.Tomato = tomato;
                result.Add(tomatoElement);

                tomatoElement.Element = destination;

                return result;
            }
        }
    }

    public class TomatoProfie : Profile
    {
        public TomatoProfie()
        {
            CreateMap<Infrastructure.Tomato, Domain.Tomato>();
        }
    }
}
