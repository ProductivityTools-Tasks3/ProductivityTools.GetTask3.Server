using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Fakes.Tests
{
    public class TestTaskRepository : ITaskRepository
    {
        public Domain.Element Element = new Domain.Element("root", "Details", "",CoreObjects.ElementType.TaskBag, null);
        public List<Domain.Element> ElementsTeset = new List<Domain.Element>();

        public void Add(Domain.Element entity)
        {
            this.Element.Elements.Add(entity);
        }

        public Domain.Element Get(int? id)
        {
            throw new System.NotImplementedException();
        }

        public List<Domain.Element> GetElements(List<int> elementids)
        {
            return ElementsTeset;
        }

        public Element GetNode(int? node)
        {
            throw new NotImplementedException();
        }

        public Domain.Element GetStructure(int? root = null)
        {
            return this.Element;
        }

        public List<Infrastructure.Element> GetTaskBags(int? rootId)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Domain.Element entity)
        {

        }
    }
}
