using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductivityTools.Cqrs.HttpConnector;
using ProductivityTools.Gateway.Swagger;
using ProductivityTools.GetTask3.CqrsController;
using FrameworkExtensions;
using ProductivityTools.Gateway.Swagger.Settings;
using Newtonsoft.Json;

namespace ProductivityTools.GetTask3.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _environment = env;
        }
        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCqrs();

            services.AddHttpToCqrsConnector();



            services.AddSwagger(new SwaggerSettings
            {
                EnvironmentName = _environment.EnvironmentName,
                Title = FrameworkExtensions.AssemblyWrapper.GetCustomAttributeValue("Title"),
                ProjectName = AssemblyWrapper.GetCustomAttributeValue("Product"),
                Company = AssemblyWrapper.GetCustomAttributeValue("Company"),
                Version = AssemblyWrapper.GetCustomAttributeValue("FileVersion"),
                ModificationDate = AssemblyWrapper.GetLastModificationDate()
            });
         //       JsonSerializerSettings = JsonSerializer.CreateSwaggerOptions()
         //   },
         //options =>
         //{
         //    options.OperationFilter<ErrorCodeOperationFilter>();
         //           // Rpc gateways registration
         //           options.SchemaFilter<CqrsSchemaFilter>();
         //    options.DocumentFilter<CqrsDocumentFilter>();
         //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
