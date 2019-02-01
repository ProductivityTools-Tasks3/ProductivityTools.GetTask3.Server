using Microsoft.Extensions.DependencyInjection;
using System;

namespace ProductivityTools.GetTask3.CqrsController
{
    public class CqrsExetensions
    {
        public static void AddCqrs(this IServiceCollection services, params CqrsModule[] modules)
        {
            var registry = new CqrsRegistry();
        }
    }
}
