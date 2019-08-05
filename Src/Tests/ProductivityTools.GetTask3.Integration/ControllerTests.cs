using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ProductivityTools.GetTask3.API.Controllers;
using ProductivityTools.GetTask3.App.Commands;
using ProductivityTools.GetTask3.App.Queries;
using ProductivityTools.GetTask3.Configuration;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.IntegrationTests;
using System;
using System.Data.SqlClient;

namespace Tests
{
    public class Tests
    {
        [SetUp]

        public void Setup()
        {

            var config = ServiceProvider.GetService<IConfig>();
            Snapshot.CreateSnapshot(config.ConnectionString);
        }

        [TearDown]
        public void TearDown()
        {
            var config = ServiceProvider.GetService<IConfig>();
            Snapshot.RestoreFromSnapshot(config.ConnectionString);
        }


        ServiceProvider ServiceProvider
        {
            get
            {
                var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
                serviceCollection.ConfigureInfrastructureServices();
                serviceCollection.AddSingleton<TaskController>();
                serviceCollection.ConfigureServicesQueries();
                serviceCollection.ConfigureServicesCommands();
                serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                serviceCollection.AddSingleton<IConfig, ConfigTest>();
                var serviceProvider = serviceCollection.BuildServiceProvider();
                return serviceProvider;
            }
        }

        TaskController TaskController
        {
            get
            {
                return ServiceProvider.GetService<TaskController>();
            }
        }

        [Test]
        public void GetEmptyTaskList2()
        {
            var structure = TaskController.GetTasks();
            Assert.Null(structure);
        }


        [Test]
        public void AddOneItem2()
        {
            string valueToTest = "Pawel Wujczyk";
            TaskController.Add(new AddRequest() { Name = valueToTest });
            var structure = TaskController.GetTasks();
            var x = structure.Elements[0];
            Assert.AreEqual(valueToTest, x.Name);
        }

        [Test]
        public void AddSecondBag()
        {
            string bagName = "HomeTasks";
            TaskController.AddBag(new AddRequest() { Name = bagName });

            var structure = TaskController.GetTasks();
            var x = structure.Elements[0];
            Assert.AreEqual(bagName, x.Name);
        }

        [Test]
        public void FinishTask2()
        {
            TaskController.Add(new AddRequest() { Name = "TaskToFinish" });

            var structure = TaskController.GetTasks();
            var x = structure.Elements[0];
            Assert.AreEqual(Status.New.ToString(), x.Status);
            var taskOrderId = x.OrderId;
            TaskController.Finish(new ProductivityTools.GetTask3.Contract.Requests.FinishRequest() { ElementId = taskOrderId });

            structure = TaskController.GetTasks();
            x = structure.Elements[0];
        }

        [Test]
        public void AddTaskAddBagAddTaskInBagFinish()
        {
            string firstTask = "FirstTask";
            TaskController.Add(new AddRequest() { Name = firstTask });
            var structure = TaskController.GetTasks();
            var structureItem = structure.Elements[0];
            Assert.AreEqual(firstTask, structureItem.Name);

            string bag = "Bag";
            TaskController.AddBag(new AddRequest() { Name = bag });
            structure = TaskController.GetTasks();
            Assert.AreEqual(2, structure.Elements.Count);

            var bagObj = structure.Elements.Find(x => x.Name == bag);
            Assert.AreEqual(bag, bagObj.Name);

            structure = TaskController.GetTasks(new ListRequest() { ParentId = bagObj.ElementId });
            Assert.AreEqual(0, structure.Elements.Count);

            string secondTask = "SecondTask";
            TaskController.Add(new AddRequest() { Name = secondTask, ParentId = bagObj.ElementId });

            structure = TaskController.GetTasks(new ListRequest() { ParentId = bagObj.ElementId });
            Assert.AreEqual(1, structure.Elements.Count);
        }

    }
}