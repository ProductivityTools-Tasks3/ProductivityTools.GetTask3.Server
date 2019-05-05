using NUnit.Framework;
using ProductivityTools.GetTask3.Domain;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
            
        }

        [Test]
        public void GetEmptyTaskList()
        {
            var ts = new TaskService();
            var structure = ts.GetStructure();
            Assert.AreEqual(0, structure.Components.Count);
        }

        [Test]
        public void AddOneItem()
        {
            string valueToTest = "Pawel Wujczyk";
            var ts = new TaskService();
            ts.Add(valueToTest);
            var structure=ts.GetStructure();
            var x = structure.Components[0] as Item ;
            Assert.AreEqual(valueToTest, x.Name);
        }
    }
}