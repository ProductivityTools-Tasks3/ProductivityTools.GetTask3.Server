using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductivityTools.GetTask3.Configuration;
using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.AutoMapper;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ProductivityTools.GetTask3.Ifrastructure.Tests
{
    //public class ConfigurationSectionTest : IConfigurationSection
    //{
    //    public string this[string key]
    //    {
    //        get
    //        {
    //            return @"Server=.\sql2017;Database=GetTask3Test;Integrated Security=True";
    //            //return @"Server=.\sql2017;Database=GetTask3Test;Integrated Security=True";
    //        }
    //        set
    //        {
    //            throw new System.NotImplementedException();
    //        }
    //    }

    //    public string Key
    //    {
    //        get
    //        {
    //            return "fdsa";
    //        }
    //    }

    //    public string Path
    //    {
    //        get
    //        {
    //            return "strds";
    //        }
    //    }

    //    public string Value
    //    {
    //        get
    //        {
    //            return string.Empty;
    //        }
    //        set
    //        {

    //        }
    //    }

    //    public IEnumerable<IConfigurationSection> GetChildren()
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public IChangeToken GetReloadToken()
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public IConfigurationSection GetSection(string key)
    //    {
    //        return this;
    //    }
    //}

    //class ConfigTest : IConfiguration
    //{
    //    public string this[string key]
    //    {
    //        get
    //        {
    //            return @"Server=.\\sql2017;Database=GetTask3Test;Integrated Security=True";
    //        }
    //        set
    //        {

    //        }
    //    }

    //    public IEnumerable<IConfigurationSection> GetChildren()
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public IChangeToken GetReloadToken()
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public IConfigurationSection GetSection(string key)
    //    {
    //        return new ConfigurationSectionTest();
    //    }
    //}


    //var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
    //var serviceProvider = serviceCollection.BuildServiceProvider();
    //var config = serviceProvider.GetService<IConfiguration>();


    [TestClass]
    public class InfrastructureTests
    {

        private TaskContext GetTaskContextInMemory()
        {
            var r=GetTaskContextInMemory(Guid.NewGuid().ToString());
            return r;
        }

        private TaskContext GetTaskContextInMemory(string name)
        {
            var options = new DbContextOptionsBuilder<TaskContext>()
             .UseInMemoryDatabase(databaseName: name)
             .Options;

            var taskContext = new Infrastructure.TaskContext(options);
            return taskContext;
        }


        private static IMapper GetMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TomatoProfie());
                cfg.AddProfile(new ElementProfile());
            });
            var mapper = mockMapper.CreateMapper();
            return mapper;
        }


        [TestMethod]
        public void AddElementForExistingTomatoTest()
        {
            IMapper mapper = GetMapper();

            var taskContext = GetTaskContextInMemory();
            taskContext.Tomato.Add(new Tomato() { Status = CoreObjects.Tomato.Status.New });
            taskContext.SaveChanges();

           // var taskContext = GetTaskContextInMemory();
            TomatoRepository tomatoRepository = new TomatoRepository(taskContext, mapper);
            TaskRepository taskRepository = new TaskRepository(taskContext, mapper, new DateTimeTools.DateTimePT());
            TaskUnitOfWork taskUnitOfWork = new TaskUnitOfWork(taskContext, taskRepository, tomatoRepository);

            Domain.Tomato currentTomato = taskUnitOfWork.TomatoRepository.GetCurrent();
            var element = new Domain.Element("pawel", CoreObjects.ElementType.Task, 1);

            element.Tomatoes.Add(currentTomato);
            taskUnitOfWork.TaskRepository.Add(element);
            taskUnitOfWork.Commit();
        }


        [TestMethod]
        public void AddElemenToList()
        {
            IMapper mapper = GetMapper();

            var taskContext = GetTaskContextInMemory();
            TaskRepository taskRepository = new TaskRepository(taskContext, mapper, new DateTimeTools.DateTimePT());
            TaskUnitOfWork taskUnitOfWork = new TaskUnitOfWork(taskContext, taskRepository, null);
            taskUnitOfWork.TaskRepository.Add(new Domain.Element("pawelxxx", CoreObjects.ElementType.Task, 1));
            taskUnitOfWork.Commit();

            var element = taskContext.Element.Single();
            //var x = taskUnitOfWork.TaskRepository.GetStructure(1);
        }
    }
}
