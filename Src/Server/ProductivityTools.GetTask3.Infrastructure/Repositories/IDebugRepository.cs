using ProductivityTools.GetTask3.Infrastructure.Objects;

namespace ProductivityTools.GetTask3.Infrastructure.Repositories
{
    public interface IDebugRepository
    {
        Debug Get(int? id);
        void Add(Debug entity);
        void Update(Debug entity);
    }
}