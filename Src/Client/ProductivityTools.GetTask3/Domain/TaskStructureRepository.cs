using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.CoreObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Domain
{
    class TaskStructureRepository
    {
        public ElementView GetStructure(int? currentNode)
        {
            // var rootElement = GetTaskHttpClient.Post<Contract.ElementView>("List", currentNode.ToString());
            var rootElement = GetTaskHttpClient.Post2<ElementView>("List", new ListRequest() { ParentId = currentNode }).Result;
            return rootElement;
        }

        public async void Add(string Name, int? parentId, ElementType type)
        {
            var x= await GetTaskHttpClient.Post2<object>("Add", new AddRequest() { Name = Name, ParentId = parentId });
        }
    }
}
