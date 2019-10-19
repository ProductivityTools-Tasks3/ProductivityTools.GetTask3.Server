using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductivityTools.GetTask3.Infrastructure.Base;
using ProductivityTools.GetTask3.Infrastructure.Objects;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTools.GetTask3.Infrastructure
{
    public class TaskUnitOfWork : ITaskUnitOfWork
    {
        private readonly TaskContext _dbContext;

        public ITaskRepository TaskRepository { get; private set; }// => new TaskRepository(_dbContext);
        public ITomatoRepository TomatoRepository { get; private set; }// => new TaskRepository(_dbContext);
        public IMediator Mediator;

        public TaskUnitOfWork(TaskContext taskContext, ITaskRepository repository, ITomatoRepository tomatoRepository, IMediator mediator)
        {
            _dbContext = taskContext;
            TaskRepository = repository;
            TomatoRepository = tomatoRepository;
            Mediator = mediator;
        }


        public void Commit()
        {
            var ChangeTracker = _dbContext.ChangeTracker;

            var addedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();
            var modifiedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();
            var deletedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted).ToList();

            _dbContext.SaveChanges();

            var notificationObjects = ChangeTracker.Entries().Select(x => x.Entity as BaseObject).Where(x => x != null && x.Notifications != null);
            IEnumerable<INotification> notifications = notificationObjects.SelectMany(x => x.Notifications);
            foreach (INotification notification in notifications)
            {
                Mediator.Publish(notification);
            }

        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void RejectChanges()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries()
               .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}
