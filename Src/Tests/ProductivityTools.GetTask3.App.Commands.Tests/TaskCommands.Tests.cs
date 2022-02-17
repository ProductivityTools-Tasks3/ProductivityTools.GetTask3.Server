using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductivityTools.GetTask3.App.Fakes.Tests;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;

namespace ProductivityTools.GetTask3.App.Commands.Tests
{
    [TestClass]
    public class TaskCommandsTests
    {
        [TestMethod]
        public void AddTask()
        {
            ITaskUnitOfWork taskUnitOfWork = new TaskUnitOfWorkTest(new TestTaskRepository(), null);
            TaskCommands taskCommands = new TaskCommands(taskUnitOfWork, new DateTimeTools.DateTimePT());
            taskCommands.Add("Test1", "Details","", 0, false);
            Assert.AreEqual((taskUnitOfWork.TaskRepository as TestTaskRepository).Element.Elements.Count, 1);
        }

        [TestMethod]
        public void AddTomatoById()
        {
            var testTomatoRepository = new TomatoRepositoryTest();
            var testTaskRepository = new TestTaskRepository();

            testTaskRepository.ElementsTeset.Add(new Domain.Element("core", "Details", "", CoreObjects.ElementType.TaskBag, null));
            testTaskRepository.ElementsTeset.Add(new Domain.Element("jeden", "Details", "", CoreObjects.ElementType.Task, 0));
            testTaskRepository.ElementsTeset.Add(new Domain.Element("dwa", "Details", "", CoreObjects.ElementType.Task, 0));
            ITaskUnitOfWork taskUnitOfWork = new TaskUnitOfWorkTest(testTaskRepository, testTomatoRepository);

            TaskCommands taskCommands = new TaskCommands(taskUnitOfWork, new DateTimeTools.DateTimePT());
            Assert.IsTrue(testTaskRepository.ElementsTeset[0].Tomatoes.Count == 0);
            taskCommands.AddToTomato(new List<int>() { 1, 2 });
            Assert.IsTrue(testTaskRepository.ElementsTeset[0].Tomatoes.Count > 0);
            Assert.IsTrue(testTaskRepository.ElementsTeset[1].Tomatoes.Count > 0);
        }

        [TestMethod]
        public void AddTomatoByName()
        {
            var testTomatoRepository = new TomatoRepositoryTest();
            var testTaskRepository = new TestTaskRepository();
            ITaskUnitOfWork taskUnitOfWork = new TaskUnitOfWorkTest(testTaskRepository, testTomatoRepository);

            TaskCommands taskCommands = new TaskCommands(taskUnitOfWork, new DateTimeTools.DateTimePT());
            Assert.IsTrue(testTaskRepository.Element.Elements.Count == 0);
            taskCommands.AddToTomato("XXX", "Details", 0);
            Assert.IsTrue(testTaskRepository.Element.Elements.Count == 1);
            Assert.IsTrue(testTaskRepository.Element.Elements[0].Tomatoes.Count > 0);
        }


    }
}
