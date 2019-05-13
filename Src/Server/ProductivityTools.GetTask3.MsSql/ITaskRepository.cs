using ProductivityTools.GetTask3.Domain;

namespace ProductivityTools.GetTask3.MsSql
{
    public interface ITaskRepository
    {
        Bag GetStructure();
    }
}