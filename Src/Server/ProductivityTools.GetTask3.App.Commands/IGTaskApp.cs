namespace ProductivityTools.GetTask3.App.Commands
{
    public interface IGTaskApp
    {
        void Add( string name, int? bagId = null);
        void AddBag(string bagName, int? bagId = null);
        void Finish(int orderId, int? bagId = null);
    }
}