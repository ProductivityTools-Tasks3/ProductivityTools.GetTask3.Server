using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductivityTools.GetTask3.App.Fakes.Tests;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;

namespace ProductivityTools.GetTask3.App.Commands.Tests
{
    //[TestClass]
    //public class TaskCommandsTests
    //{

    //    private static IMapper GetMapper()
    //    {
    //        var mockMapper = new MapperConfiguration(cfg =>
    //        {
    //            cfg.AddProfile(new TomatoProfie());
    //            cfg.AddProfile(new ElementProfile());
    //        });
    //        var mapper = mockMapper.CreateMapper();
    //        return mapper;
    //    }

    //    [TestMethod]
    //    public void AddTask()
    //    {
    //        ITaskUnitOfWork taskUnitOfWork = new TaskUnitOfWorkTest(new TestTaskRepository(), null);
    //        TaskCommands taskCommands = new TaskCommands(taskUnitOfWork, new DateTimeTools.DateTimePT(), GetMapper());
    //        taskCommands.Add("Test1", "Details", "", 0, false);
    //        Assert.AreEqual((taskUnitOfWork.TaskRepository as TestTaskRepository).Element.Elements.Count, 1);
    //    }

    //    [TestMethod]
    //    [Ignore]//currently tomatos are nott used
    //    public void AddTomatoById()
    //    {
    //        var testTomatoRepository = new TomatoRepositoryTest();
    //        var testTaskRepository = new TestTaskRepository();


    //        testTaskRepository.ElementsTeset.Add(new Infrastructure.Element());
    //        testTaskRepository.ElementsTeset.Add(new Infrastructure.Element());
    //        testTaskRepository.ElementsTeset.Add(new Infrastructure.Element());
    //        ITaskUnitOfWork taskUnitOfWork = new TaskUnitOfWorkTest(testTaskRepository, testTomatoRepository);

    //        TaskCommands taskCommands = new TaskCommands(taskUnitOfWork, new DateTimeTools.DateTimePT(),GetMapper());
    //        Assert.IsTrue(testTaskRepository.ElementsTeset[0].TomatoElements.Count == 0);
    //        taskCommands.AddToTomato(new List<int>() { 1, 2 });
    //        Assert.IsTrue(testTaskRepository.ElementsTeset[0].TomatoElements.Count > 0);
    //        Assert.IsTrue(testTaskRepository.ElementsTeset[1].TomatoElements.Count > 0);
    //    }

    //    [TestMethod]
    //    [Ignore]//currently tomatos are nott used

    //    public void AddTomatoByName()
    //    {
    //        var testTomatoRepository = new TomatoRepositoryTest();
    //        var testTaskRepository = new TestTaskRepository();
    //        ITaskUnitOfWork taskUnitOfWork = new TaskUnitOfWorkTest(testTaskRepository, testTomatoRepository);

    //        TaskCommands taskCommands = new TaskCommands(taskUnitOfWork, new DateTimeTools.DateTimePT(),GetMapper());
    //        Assert.IsTrue(testTaskRepository.Element.Elements.Count == 0);
    //        taskCommands.AddToTomato("XXX", "Details", 0);
    //        Assert.IsTrue(testTaskRepository.Element.Elements.Count == 1);
    //        Assert.IsTrue(testTaskRepository.Element.Elements[0].TomatoElements.Count > 0);
    //    }


    //}
}
