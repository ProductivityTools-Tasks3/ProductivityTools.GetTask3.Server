using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Fakes.Tests
{
    public class TestTaskRepository : ITaskRepository
    {
        public Element Element = new Infrastructure.Element();//"root", "Details", "",CoreObjects.ElementType.TaskBag, null);
        public List<Element> ElementsTeset = new List<Infrastructure.Element>();

        public void Add(Element entity)
        {
            this.Element.Elements.Add(entity);
        }

        public Element Get(int? id)
        {
            throw new System.NotImplementedException();
        }

        public List<Element> GetElements(List<int> elementids)
        {
            return ElementsTeset;
        }

        public Element GetNode(int? node)
        {
            throw new NotImplementedException();
        }

        public Infrastructure.Element GetStructure(int? root = null)
        {
            return this.Element;
        }

        public List<Infrastructure.Element> GetTaskBags(int? rootId)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Infrastructure.Element entity)
        {

        }
    }
}
