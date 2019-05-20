using ProductivityTools.GetTask3.Domain;

namespace ProductivityTools.GetTask3.App.Queries
{
    public interface IGTaskAppQuery
    {
        StructureView GetTaskList();
    }
}