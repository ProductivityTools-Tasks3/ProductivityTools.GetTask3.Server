using AutoMapper;
using ProductivityTools.GetTask3.App.Queries.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Queries.AutoMapper
{
    public class ElementProfile : Profile
    {
        public ElementProfile()
        {
            CreateMap<Domain.Component, ItemView>()
                .Include<Domain.Task, TaskView>()
                .Include<Domain.Bag, BagView>();
            CreateMap<Domain.Task, TaskView>();
            CreateMap<Domain.Bag, BagView>();
        }
    }
}

