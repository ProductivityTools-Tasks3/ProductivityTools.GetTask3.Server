using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.View;
using System.Linq;

namespace ProductivityTools.GetTask3.Cmdlet
{
    class SessionMetaDataProviderTest : ISessionMetaDataProvider
    {
        public SessionMetaDataProviderTest()
        {
            this.SessionMetadata = new StructureMetadata();
        }

        public StructureMetadata SessionMetadata { get; set; }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var structure = new Contract.ElementView();
            structure.Elements = new System.Collections.Generic.List<Contract.ElementView>();
            structure.Elements.Add(new Contract.ElementView() { Type = CoreObjects.ElementType.Task });

            ISessionMetaDataProvider sessionMetaDataProvider = new SessionMetaDataProviderTest();
            var taskRepository = new Moq.Mock<ITaskRepository>();
            taskRepository.Setup(i => i.GetStructure(Moq.It.IsAny<int?>())).Returns(structure);
            var task = new App.Task(sessionMetaDataProvider, taskRepository.Object);
            var x = task.Elements.ToList();
        }
    }
}
