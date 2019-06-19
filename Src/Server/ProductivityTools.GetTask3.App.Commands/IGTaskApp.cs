namespace ProductivityTools.GetTask3.App.Commands
{
    public interface IGTaskApp
    {
        void Add( string name, int? bagId);
        void AddBag(string bagName, int? bagId);
        void Finish(int orderId, int? bagId);
    }
}