using NUnit.Framework;
using ProductivityTools.GetTask3.App.Commands;
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
            var ts = new GTaskApp();
            var structure = ts.GetStructure();
            Assert.AreEqual(0, structure.Components.Count);
        }

        [Test]
        public void AddOneItem()
        {
            string valueToTest = "Pawel Wujczyk";
            var ts = new GTaskApp();
            ts.Add(valueToTest);
            var structure=ts.GetStructure();
            var x = structure.Components[0] as Item ;
            Assert.AreEqual(valueToTest, x.Name);
        }

        [Test]
        public void AddSecondBag()
        {
            string bagName = "HomeTasks";
            var ts = new GTaskApp();
            ts.AddBag(bagName);

            var structure = ts.GetStructure();
            var x=structure.Components[0] as Bag;
            Assert.AreEqual(bagName, x.Name);
        }

        [Test]
        public void FinishTask()
        {
            var ts = new GTaskApp();

            ts.Add("TaskToFinish");

            var structure = ts.GetStructure();
            var x = structure.Components[0] as Item;
            var taskOrderId=x.TaskOrderId;

            ts.FinishTask(taskOrderId);
        }


    }
}