namespace ProductivityTools.GetTask3.App.Commands
{
    public interface IGTaskApp
    {
        void Add(string name);
        void AddBag(string bagName);
        void FinishTask(int TaskOrderId);
    }
}