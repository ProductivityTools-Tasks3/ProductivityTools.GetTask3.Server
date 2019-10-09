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
            Domain.Element root = new Domain.Element("Root", CoreObjects.ElementType.TaskBag, null);
            Domain.Element element = new Domain.Element("element", CoreObjects.ElementType.Task, root.ElementId);
            root.Elements.Add(element);


            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ElementProfile());
            });
            var mapper = mockMapper.CreateMapper();

            ITaskUnitOfWork taskUnitOfWork = new TaskUnitOfWorkTest(taskRepository, null);
            ITaskQueries taskCommands = new TaskQueries(taskRepository, null, mapper);
            var result = taskCommands.GetTaskList();
            Assert.AreEqual(result.Name, "root");
        }
    }
}
