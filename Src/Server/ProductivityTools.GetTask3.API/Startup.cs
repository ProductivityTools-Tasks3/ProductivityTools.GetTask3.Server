using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductivityTools.GetTask3.App.Commands;
using ProductivityTools.GetTask3.App.Queries;
using ProductivityTools.GetTask3.Configuration;
using ProductivityTools.GetTask3.Handlers;
using ProductivityTools.GetTask3.SignalRHubs;

namespace ProductivityTools.GetTask3.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(x => x.AllowEmptyInputInBodyModelBinding = true).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSignalR();
            services.ConfigureServicesQueries();
            services.ConfigureServicesConfig();
            services.ConfigureServicesCommands();
            services.ConfigureSingalRServices();
            services.ConfigureServicesHandlers();

            services.AddLogging(opt =>
                     {
                         opt.AddConsole();
                         opt.AddDebug();
                     });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRouting();


            //app.UseHttpsRedirection();
            app.UseCors();
            //pw: signalR
            //app.ConfigureSignalR();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
