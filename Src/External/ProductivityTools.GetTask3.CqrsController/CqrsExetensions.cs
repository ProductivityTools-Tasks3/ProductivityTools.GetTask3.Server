using Microsoft.Extensions.DependencyInjection;

namespace ProductivityTools.GetTask3.CqrsController
{
    public static class CqrsExetensions
    {
        public static void AddCqrs(this IServiceCollection services, params CqrsModule[] modules)
        {
            var registry = new CqrsRegistry();
            services.AddTransient<ICqrsAuthPolicy, DefaultCqrsAuthPolicy>();
            services.AddMvc().AddApplicationPart(typeof(CqrsMediatorController).Assembly);
        }
    }
}
