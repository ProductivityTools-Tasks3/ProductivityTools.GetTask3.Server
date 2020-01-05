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
    class TaskRepositoryCmd : ITaskRepositoryCmd
    {
        public ElementView GetStructure(int? currentNode, string path)
        {
            VerboseHelper.WriteVerboseStatic("Calling GetStructure");
            var rootElement = GetTaskHttpClient.Post2<ElementView>(Consts.Task, Consts.TodayList, new ListRequest() { ElementId = currentNode, Path = path }, VerboseHelper.WriteVerboseStatic).Result;
            return rootElement;
        }

        public int? GetRoot(int? currentNode, string path)
        {
            VerboseHelper.WriteVerboseStatic("Calling GetRoot");
            var rootElement = GetTaskHttpClient.Post2<int?>(Consts.Task, Consts.GetRoot, new GetRootRequest() { ElementId = currentNode, Path = path }, VerboseHelper.WriteVerboseStatic).Result;
            return rootElement;
        }

        public async void Add(string name, int? parentId, ElementType type)
        {
            switch (type)
            {
                case ElementType.TaskBag:
                    await ProductivityTools.GetTask3.Client.Calls.Task.AddBag(name, parentId,VerboseHelper.WriteVerboseStatic);
                    break;
                case ElementType.Task:
                    await ProductivityTools.GetTask3.Client.Calls.Task.Add(name, parentId, VerboseHelper.WriteVerboseStatic);
                    break;
                default:
                    break;
            }
        }

        public async void Finish(int elementId)
        {
            await ProductivityTools.GetTask3.Client.Calls.Task.Finish(elementId, VerboseHelper.WriteVerboseStatic);
        }

        public async void Start(int elementId)
        {
            await ProductivityTools.GetTask3.Client.Calls.Task.Start(elementId, VerboseHelper.WriteVerboseStatic);
        }

        public async void Move(int[] elementIds, int target)
        {
            await ProductivityTools.GetTask3.Client.Calls.Task.Move(elementIds, target, VerboseHelper.WriteVerboseStatic);
        }

        public async void Undone(int elementId)
        {
            await ProductivityTools.GetTask3.Client.Calls.Task.Undone(elementId, VerboseHelper.WriteVerboseStatic);
        }

        public async void Delay(int elementId, DateTime date)
        {
            await ProductivityTools.GetTask3.Client.Calls.Task.Delay(elementId, date, VerboseHelper.WriteVerboseStatic);
        }

        public async void Delete(int elementId)
        {
            await ProductivityTools.GetTask3.Client.Calls.Task.Delete(elementId, VerboseHelper.WriteVerboseStatic);
        }

        public async void AddToTomato(int[] elementIds)
        {
            await ProductivityTools.GetTask3.Client.Calls.Task.AddToTomato(elementIds, VerboseHelper.WriteVerboseStatic);
        }
    }
}
