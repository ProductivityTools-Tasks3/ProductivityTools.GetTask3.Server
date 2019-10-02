using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure.AutoMapper
{
    public class ElementProfile : Profile
    {
        public ElementProfile()
        {
            CreateMap<Infrastructure.Element, Domain.Element>()
                .ForMember(dest => dest.Tomatoes, opt => opt.MapFrom(src => src.TomatoElements.Select(x => x.Tomato)));
            CreateMap<Domain.Element, Infrastructure.Element>()
                .ForMember(x => x.TomatoElements, opt => opt.MapFrom<CustomResolver>());
        }

        public class CustomResolver : IValueResolver<Domain.Element, Infrastructure.Element, List<TomatoElement>>
        {
            public List<TomatoElement> Resolve(Domain.Element source, Infrastructure.Element destination, List<TomatoElement> member, ResolutionContext context)
            {
                List<TomatoElement> result = null;
                if (source.Tomatoes.Any())
                {
                    result = new List<TomatoElement>();
                    foreach (var tomato in source.Tomatoes)
                    {
                        var tomatoElement = new TomatoElement();
                        tomatoElement.TomatoId = tomato.TomatoId;
                        tomatoElement.ElementId = source.ElementId;
                        tomatoElement.Tomato = context.Mapper.Map<Infrastructure.Tomato>(tomato);
                        result.Add(tomatoElement);
                        tomatoElement.Element = destination;
                    }

                }
                return result;
            }
        }
    }

    public class TomatoProfie : Profile
    {
        public TomatoProfie()
        {
            CreateMap<Infrastructure.Tomato, Domain.Tomato>()
                .ForMember(x=>x.ElementsId,opt=>opt.MapFrom(x=>x.TomatoElements.Select(y=>y.ElementId)))
                .ReverseMap();
        }
    }

    public class DefinedElementGroupProfile : Profile
    {
        public DefinedElementGroupProfile()
        {
            CreateMap<Infrastructure.DefinedElementGroup, Domain.DefinedElementGroup>().ReverseMap();
        }
    }

    public class DefinedElementProfile : Profile
    {
        public DefinedElementProfile()
        {
            CreateMap<Infrastructure.DefinedElement, Domain.DefinedElement>().ReverseMap();
        }
    }
}
