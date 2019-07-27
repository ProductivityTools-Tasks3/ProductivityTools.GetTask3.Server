using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.Contract.Requests;
using ProductivityTools.GetTask3.CoreObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Domain
{
    class TaskRepository
    {
        public ElementView GetStructure(int? currentNode)
        {
            // var rootElement = GetTaskHttpClient.Post<Contract.ElementView>("List", currentNode.ToString());
            var rootElement = GetTaskHttpClient.Post2<ElementView>("Task", "List", new ListRequest() { ParentId = currentNode }).Result;
            return rootElement;
        }

        public async void Add(string Name, int? parentId, ElementType type)
        {
            switch (type)
            {
                case ElementType.TaskBag:
                    var x0 = await GetTaskHttpClient.Post2<object>("Task", "AddBag", new AddRequest() { Name = Name, ParentId = parentId });
                    break;
                case ElementType.Task:
                    var x1 = await GetTaskHttpClient.Post2<object>("Task", "Add", new AddRequest() { Name = Name, ParentId = parentId });
                    break;
                default:
                    break;
            }
            
        }

        internal async void Finish(int elementId)
        {
            await GetTaskHttpClient.Post2<object>("Task", "Finish", new FinishRequest() { ElementId = elementId });
        }

        internal async void Undone(int elementId)
        {
            await GetTaskHttpClient.Post2<object>("Task", "Undone", new UndoneRequest() { ElementId = elementId });
        }
    }
}
