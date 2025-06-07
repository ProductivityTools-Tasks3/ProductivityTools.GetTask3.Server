using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using ProductivityTools.GetTask3.App.Commands;
using ProductivityTools.GetTask3.App.Queries;
using ProductivityTools.GetTask3.Configuration;
using ProductivityTools.GetTask3.Handlers;
using ProductivityTools.GetTask3.SignalRHubs;

namespace ProductivityTools.GetTask3.API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string masterconfpath = Environment.GetEnvironmentVariable("MasterConfigurationPath");
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile($"{masterconfpath}\\ProductivityTools.GetTask3.ServiceAccount.json"),
            });
            services
             .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.Authority = "https://securetoken.google.com/ptgettasks3prod";
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidIssuer = "https://securetoken.google.com/ptgettasks3prod",
                     ValidateAudience = true,
                     ValidAudience = "ptgettasks3prod",
                     ValidateLifetime = true
                 };
             });

            IdentityModelEventSource.ShowPII = true;
            services.AddControllers();//??
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.Authority = "https://identityserver.productivitytools.tech:8010";
            //    options.Audience = "GetTask3.API";
            //});

            services.AddMvc(x => x.AllowEmptyInputInBodyModelBinding = true).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        //builder.WithOrigins("http://localhost:3000", "https://task3web.z13.web.core.windows.net","https://ptservicestatus-309299231472.us-central1.run.app").AllowAnyMethod().AllowAnyHeader();
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

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

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();


            //app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);

            //pw: signalR
            app.ConfigureSignalR();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
