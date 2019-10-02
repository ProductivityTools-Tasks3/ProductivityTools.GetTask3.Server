using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Fakes.Tests
{
    public class TaskUnitOfWorkTest : ITaskUnitOfWork
    {
        public ITaskRepository TaskRepository { get; }

        public ITomatoRepository TomatoRepository { get; }

        public TaskUnitOfWorkTest(ITaskRepository taskRepository,ITomatoRepository tomatoRepository)
        {
            this.TaskRepository = taskRepository;
            this.TomatoRepository = tomatoRepository;
        }
        public void Commit()
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void RejectChanges()
        {
            throw new NotImplementedException();
        }
    }
}
