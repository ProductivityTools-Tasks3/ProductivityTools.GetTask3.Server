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
    public class GetElements
    {
        [TestMethod]
        public void CheckStructureOfTwoItemsOnTheSameLevel()
        {
            var structure = new Contract.ElementView();
            structure.Elements = new System.Collections.Generic.List<Contract.ElementView>();
            structure.Elements.Add(new Contract.ElementView() { ElementId = 11, Name = "First", Type = CoreObjects.ElementType.Task });
            structure.Elements.Add(new Contract.ElementView() { ElementId = 12, Type = CoreObjects.ElementType.Task });

            ISessionMetaDataProvider sessionMetaDataProvider = new SessionMetaDataProviderTest();
            var taskRepository = new Moq.Mock<ITaskRepositoryCmd>();
            taskRepository.Setup(i => i.GetStructure(Moq.It.IsAny<int?>(),string.Empty)).Returns(structure);
            var task = new App.Task(sessionMetaDataProvider, taskRepository.Object,string.Empty);


            var x = task.Elements.ToList();
            Assert.AreEqual(2, x.Count);
            Assert.AreEqual("First", x[0].Element.Name);
            Assert.AreEqual(1, x[1].SessionElement.Order);
        }

        [TestMethod]
        public void CheckStructureOfBagAndChild()
        {
            var structure = new Contract.ElementView();
            structure.Elements = new System.Collections.Generic.List<Contract.ElementView>();
            structure.Elements.Add(new Contract.ElementView() { ElementId = 11, Name = "Parent", Type = CoreObjects.ElementType.TaskBag });
            structure.Elements[0].Elements = new System.Collections.Generic.List<Contract.ElementView>();
            structure.Elements[0].Elements.Add(new Contract.ElementView() { ElementId = 12,Name="Child", Type = CoreObjects.ElementType.Task });

            ISessionMetaDataProvider sessionMetaDataProvider = new SessionMetaDataProviderTest();
            var taskRepository = new Moq.Mock<ITaskRepositoryCmd>();
            taskRepository.Setup(i => i.GetStructure(Moq.It.IsAny<int?>(),string.Empty)).Returns(structure);
            var task = new App.Task(sessionMetaDataProvider, taskRepository.Object,string.Empty);


            var x = task.Elements.ToList();
            Assert.AreEqual(1, x.Count);
            Assert.AreEqual("Parent", x[0].Element.Name);
            Assert.AreEqual("Child", x[0].Element.Elements[0].Name);
        }
    }
}
