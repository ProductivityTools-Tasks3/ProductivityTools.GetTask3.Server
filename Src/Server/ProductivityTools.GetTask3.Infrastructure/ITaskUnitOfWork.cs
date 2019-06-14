using ProductivityTools.GetTask3.Infrastructure.Base;
using ProductivityTools.GetTask3.Infrastructure.Repositories;

namespace ProductivityTools.GetTask3.Infrastructure
{
    public interface ITaskUnitOfWork: IUnitOfWork
    {
        TaskRepository TaskRepository { get; }
    }
}