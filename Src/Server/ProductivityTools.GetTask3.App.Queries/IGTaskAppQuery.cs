using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.Domain;

namespace ProductivityTools.GetTask3.App.Queries
{
    public interface IGTaskAppQuery
    {
        ItemView GetTaskList(int? bagId = null);
    }
}