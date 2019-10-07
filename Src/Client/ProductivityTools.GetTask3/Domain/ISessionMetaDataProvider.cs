using ProductivityTools.GetTask3.View;

namespace ProductivityTools.GetTask3.Domain
{
    public interface ISessionMetaDataProvider
    {
        StructureMetadata SessionMetadata { get; }
    }
}