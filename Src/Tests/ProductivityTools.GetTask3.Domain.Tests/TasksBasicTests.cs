using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ProductivityTools.GetTask3.App.Commands;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.MsSql;

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
                serviceCollection.ConfigureServices();
                var serviceProvider = serviceCollection.BuildServiceProvider();

                var taskrepository = serviceProvider.GetService<ITaskRepository>();
                var ts = new GTaskApp(taskrepository);
                return ts;
            }
        }

        [Test]
        public void GetEmptyTaskList()
        {
            //var serviceProvider = serviceCollection.BuildServiceProvider();
            //var taskrepository = serviceProvider.GetService<ITaskRepository>();
            //var ts = new GTaskApp(taskrepository);
            var structure = GTaskApp.GetTaskList();
            Assert.AreEqual(0, structure.Components.Count);
        }

        [Test]
        public void AddOneItem()
        {
            string valueToTest = "Pawel Wujczyk";
            GTaskApp.Add(valueToTest);
            var structure = GTaskApp.GetTaskList();
            var x = structure.Components[0] as Item;
            Assert.AreEqual(valueToTest, x.Name);
        }

        [Test]
        public void AddSecondBag()
        {
            string bagName = "HomeTasks";
            GTaskApp.AddBag(bagName);

            var structure = GTaskApp.GetTaskList();
            var x = structure.Components[0] as Bag;
            Assert.AreEqual(bagName, x.Name);
        }

        [Test]
        public void FinishTask()
        {
            GTaskApp.Add("TaskToFinish");

            var structure = GTaskApp.GetTaskList();
            var x = structure.Components[0] as Item;
            var taskOrderId = x.TaskOrderId;

            GTaskApp.FinishTask(taskOrderId);
        }
    }
}