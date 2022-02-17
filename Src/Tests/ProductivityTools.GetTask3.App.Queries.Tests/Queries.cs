using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductivityTools.GetTask3.App.Commands;
using ProductivityTools.GetTask3.App.Fakes.Tests;
using ProductivityTools.GetTask3.App.Queries.AutoMapper;
using ProductivityTools.GetTask3.Infrastructure;

namespace ProductivityTools.GetTask3.App.Queries.Tests
{
    [TestClass]
    public class Queries
    {
        [TestMethod]
        public void GetList()
        {
            var taskRepository = new TestTaskRepository();
            Domain.Element root = new Domain.Element("Root", "Details","", CoreObjects.ElementType.TaskBag, null);
            Domain.Element elementLevel1 = new Domain.Element("Level1", "Details","", CoreObjects.ElementType.Task, root.ElementId);
            root.Elements.Add(elementLevel1);
            Domain.Element elementLevel2 = new Domain.Element("Level2", "Details", "", CoreObjects.ElementType.Task, root.ElementId);
            elementLevel1.Elements.Add(elementLevel2);


            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ElementProfile());
            });
            var mapper = mockMapper.CreateMapper();

            ITaskUnitOfWork taskUnitOfWork = new TaskUnitOfWorkTest(taskRepository, null);
            ITaskQueries taskCommands = new TaskQueries(taskRepository, null, mapper);
            var result = taskCommands.GetTaskList(null, string.Empty);
            Assert.AreEqual(result.Name, "root");

            result = taskCommands.GetTaskList(null, "Level1");
            Assert.AreEqual(result.Name, "Level1");

            result = taskCommands.GetTaskList(null, "Level1\\Level2");
            Assert.AreEqual(result.Name, "Level2");

        }

    }
}
