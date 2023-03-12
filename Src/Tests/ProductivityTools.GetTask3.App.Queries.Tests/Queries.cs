using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductivityTools.GetTask3.App.Commands;
using ProductivityTools.GetTask3.App.Fakes.Tests;
using ProductivityTools.GetTask3.App.Queries.AutoMapper;
using ProductivityTools.GetTask3.Infrastructure;

namespace ProductivityTools.GetTask3.App.Queries.Tests
{
    //[TestClass]
    //public class Queries
    //{
    //    [TestMethod]
    //    public void GetList()
    //    {
    //        var taskRepository = new TestTaskRepository();
    //        //Infrastructure.Element root = new Infrastructure.Element { Name = "Root" };
    //        //root.Elements = new System.Collections.Generic.List<Element>();
    //        taskRepository.Element.Elements = new System.Collections.Generic.List<Element>();
    //        Infrastructure.Element elementLevel1 = new Infrastructure.Element { Name = "Level1", ElementId = 1 };
    //        elementLevel1.Elements = new System.Collections.Generic.List<Element>();
    //        taskRepository.Element.Elements.Add(elementLevel1);
    //        Infrastructure.Element elementLevel2 = new Infrastructure.Element { Name = "Level2", ElementId = 2 };
    //        elementLevel1.Elements.Add(elementLevel2);


    //        var mockMapper = new MapperConfiguration(cfg =>
    //        {
    //            cfg.AddProfile(new ProductivityTools.GetTask3.App.Queries.AutoMapper.ElementProfile());
    //        });
    //        var mapper = mockMapper.CreateMapper();

    //        ITaskUnitOfWork taskUnitOfWork = new TaskUnitOfWorkTest(taskRepository, null);
    //        ITaskQueries taskCommands = new TaskQueries(taskRepository, null, mapper);
    //        var result = taskCommands.GetTaskList(1, string.Empty);
    //        Assert.AreEqual(result.Name, "root");

    //        result = taskCommands.GetTaskList(1, "Level1");
    //        Assert.AreEqual(result.Name, "Level1");

    //        result = taskCommands.GetTaskList(1, "Level1\\Level2");
    //        Assert.AreEqual(result.Name, "Level2");

    //    }

    //}
}
