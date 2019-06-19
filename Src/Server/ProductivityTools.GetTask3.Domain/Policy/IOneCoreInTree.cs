namespace ProductivityTools.GetTask3.Domain.Policy
{
    public interface IOneCoreInTree
    {
        void Evaluate(int? parentId, ElementType type);
    }
}