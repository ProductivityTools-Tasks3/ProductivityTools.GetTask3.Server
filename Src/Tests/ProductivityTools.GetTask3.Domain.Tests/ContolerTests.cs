using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ProductivityTools.GetTask3.API.Controllers;
using ProductivityTools.GetTask3.App.Commands;
using ProductivityTools.GetTask3.App.Queries;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {


        }

        private GTaskApp GTaskApp
        {
            get
            {
                var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
                serviceCollection.ConfigureInfrastructureServices();
                var serviceProvider = serviceCollection.BuildServiceProvider();

                var taskrepository = serviceProvider.GetService<ITaskRepository>();
                var ts = new GTaskApp(taskrepository);
                return ts;
            }
        }


        private GTaskAppQuery GTaskAppQuery
        {
            get
            {
                var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
                serviceCollection.ConfigureInfrastructureServices();
                var serviceProvider = serviceCollection.BuildServiceProvider();

                var taskrepository = serviceProvider.GetService<ITaskRepository>();
                var ts = new GTaskAppQuery(taskrepository);
                return ts;
            }
        }



    

        [Test]
        public void AddSecondBag()
        {
            string bagName = "HomeTasks";
            GTaskApp.AddBag(bagName);

            var structure = GTaskAppQuery.GetTaskList();
            var x = structure.Items[0];
            Assert.AreEqual(bagName, x.Name);
        }

        [Test]
        public void FinishTask()
        {
            GTaskApp.Add("TaskToFinish");

            var structure = GTaskAppQuery.GetTaskList();
            var x = structure.Items[0];
            //var taskOrderId = x.TaskOrderId;

            //GTaskApp.FinishTask(taskOrderId);
        }
    }
}