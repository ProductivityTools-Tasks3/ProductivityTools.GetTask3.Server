using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.Commands;
using ProductivityTools.GetTask3.CommonConfiguration;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.Contract.Requests;
using ProductivityTools.GetTask3.CoreObjects;
using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Domain
{
    class TaskRepository
    {
        //Action<String> WriteVerbose;

        //public TaskRepository(Action<string> writeVerbose)
        //{
        //    this.WriteVerbose = writeVerbose;
        //}

        public ElementView GetStructure(int? currentNode)
        {
            VerboseHelper.WriteVerboseStatic("Calling GetStructure");
            // var rootElement = GetTaskHttpClient.Post<Contract.ElementView>("List", currentNode.ToString());
            var rootElement = GetTaskHttpClient.Post2<ElementView>("Task", Consts.TodayList, new ListRequest() { ParentId = currentNode }, VerboseHelper.WriteVerboseStatic).Result;
            return rootElement;
        }

        public async void Add(string Name, int? parentId, ElementType type)
        {
            switch (type)
            {
                case ElementType.TaskBag:
                    var x0 = await GetTaskHttpClient.Post2<object>("Task", "AddBag", new AddRequest() { Name = Name, ParentId = parentId }, VerboseHelper.WriteVerboseStatic);
                    break;
                case ElementType.Task:
                    var x1 = await GetTaskHttpClient.Post2<object>("Task", "Add", new AddRequest() { Name = Name, ParentId = parentId }, VerboseHelper.WriteVerboseStatic);
                    break;
                default:
                    break;
            }

        }

        internal async void Finish(int elementId)
        {
            await GetTaskHttpClient.Post2<object>("Task", "Finish", new FinishRequest() { ElementId = elementId }, VerboseHelper.WriteVerboseStatic);
        }

        internal async void Undone(int elementId)
        {
            await GetTaskHttpClient.Post2<object>("Task", "Undone", new UndoneRequest() { ElementId = elementId }, VerboseHelper.WriteVerboseStatic);
        }

        internal async void Delay(int elementId, DateTime date)
        {
            await GetTaskHttpClient.Post2<object>("Task", "Delay", new DelayItemRequest() { ElementId = elementId, StartDate = date }, VerboseHelper.WriteVerboseStatic);
        }

        internal async void AddToTomato(int[] elementIds)
        {
            await GetTaskHttpClient.Post2<object>("Task", Consts.AddToTomato, new AddToTomatoRequest() { ElementItems = elementIds }, VerboseHelper.WriteVerboseStatic);
        }
    }
}
