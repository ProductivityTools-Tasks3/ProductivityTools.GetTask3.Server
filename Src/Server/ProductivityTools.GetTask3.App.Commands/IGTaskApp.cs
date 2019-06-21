using System;

namespace ProductivityTools.GetTask3.App.Commands
{
    public interface IGTaskApp
    {
        void Add( string name, int? bagId);
        void AddBag(string bagName, int? bagId);
        void Finish(int elementId);
        void Undone(int elementId);
        void Delay(int elementId, DateTime dateTime);
    }
}