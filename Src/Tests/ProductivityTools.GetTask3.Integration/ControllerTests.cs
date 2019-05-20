using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ProductivityTools.GetTask3.API.Controllers;
using ProductivityTools.GetTask3.App.Commands;
using ProductivityTools.GetTask3.App.Queries;
using ProductivityTools.GetTask3.Infrastructure;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        TaskController TaskController
        {
            get
            {
                var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
                serviceCollection.ConfigureInfrastructureServices();
                serviceCollection.AddSingleton<TaskController>();
                serviceCollection.ConfigureServicesQueries();
                serviceCollection.ConfigureServicesCommands();
                var serviceProvider = serviceCollection.BuildServiceProvider();

                return serviceProvider.GetService<TaskController>();
            }
        }

        [Test]
        public void GetEmptyTaskList2()
        {
            var structure = TaskController.GetTasks();
            Assert.AreEqual(0, structure.Items.Count);
        }


        [Test]
        public void AddOneItem2()
        {
            string valueToTest = "Pawel Wujczyk";
            TaskController.Add(valueToTest);
            var structure = TaskController.GetTasks();
            var x = structure.Items[0];
            Assert.AreEqual(valueToTest, x.Name);
        }

    }
}