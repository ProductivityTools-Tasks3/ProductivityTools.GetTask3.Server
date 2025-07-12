using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductivityTools.GetTask3.API.Controllers;
using ProductivityTools.GetTask3.App.Commands;
using ProductivityTools.GetTask3.App.Queries;
using ProductivityTools.GetTask3.Configuration;
using ProductivityTools.GetTask3.Handlers;
using System;

namespace ProductivityTools.GetTasks3.Integration.Tests
{
    [TestClass]
    public class UnitTest1
    {

        private IConfiguration? _config;
        public IConfiguration Configuration
        {
            get
            {
                if (_config == null)
                {
                    var builder = new ConfigurationBuilder().AddJsonFile($"testsettings.json", optional: false);
                    _config = builder.Build();
                }

                return _config;
            }
        }
        public ServiceProvider ServiceProvider
        {
            get
            {
                var services = new ServiceCollection();

                services.AddSingleton<IConfiguration>(Configuration);


                //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                services.AddControllers();
                services.ConfigureServicesQueries();
                services.ConfigureServicesConfig();
                services.ConfigureServicesCommands();
                services.ConfigureServicesHandlers();
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                return services.BuildServiceProvider();
            }
        }

        ITaskQueries ITaskQueries => ServiceProvider.GetService<ITaskQueries>();
        IGTaskCommands IGTaskCommands => ServiceProvider.GetService<IGTaskCommands>();
        IMapper Mapper => ServiceProvider.GetService<IMapper>();


        [TestMethod]
        public void AddNewElement()
        {
            TaskController controller = new TaskController(ITaskQueries, IGTaskCommands);
            int result = controller.Add(new GetTask3.Contract.AddRequest { Name = "Nowy", Details = "Details", ParentId = 1 });
            Assert.IsTrue(result > 0);
        }
    }
}