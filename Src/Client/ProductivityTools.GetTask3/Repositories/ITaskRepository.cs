using System;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.CoreObjects;

namespace ProductivityTools.GetTask3.Domain
{
    public interface ITaskRepository
    {
        void Add(string Name, int? parentId, ElementType type);
        void AddToTomato(int[] elementIds);
        void Delay(int elementId, DateTime date);
        void Finish(int elementId);
        ElementView GetStructure(int? currentNode);
        void Undone(int elementId);
    }
}