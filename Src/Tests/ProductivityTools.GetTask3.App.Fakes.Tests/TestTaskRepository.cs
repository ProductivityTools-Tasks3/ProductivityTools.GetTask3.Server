using ProductivityTools.DateTimeTools;
using ProductivityTools.GetTask3.Infrastructure;
using ProductivityTools.GetTask3.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.GetTask3.App.Fakes.Tests
{
    public class TestTaskRepository : ITaskRepository
    {
        public Element Element = new Infrastructure.Element { Name = "root", ElementId = 0, Elements = new List<Element>() };

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

        private Element GetNodeRecurse(Element element, int value)
        {
            if (element.ElementId == value)
            {
                return element;
            }
            else
            {
                foreach (var el in element.Elements)
                {
                    return GetNodeRecurse(el, value);
                }
                throw new Exception("Elmenent not found");
            }
        }

        public Element GetNode(string filter, int? node)
        {
            if (node.HasValue == false)
            {
                return this.Element;
            }
            else
            {
                return GetNodeRecurse(this.Element, node.Value);
            }
        }

        public Infrastructure.Element GetStructure(string filter, int? root = null)
        {
            if (root.HasValue == false)
            {
                return this.Element;
            }
            else
            {
                return GetNodeRecurse(this.Element, root.Value);
            }
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
