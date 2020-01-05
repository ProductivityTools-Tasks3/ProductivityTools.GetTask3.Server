using ProductivityTools.GetTask3.CommonConfiguration;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.Contract.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Client.Calls
{
    public static class Task
    {
        //pw: change this console.writeline to logging mechamism
        public async static Task<object> Add(string name, int? parentId, Action<string> log)
        {
            return await GetTaskHttpClient.Post2<object>(Consts.Task, "Add", new AddRequest() { Name = name, ParentId = parentId }, log);
        }

        public async static Task<object> AddBag(string name, int? parentId, Action<string> log)
        {
            return await GetTaskHttpClient.Post2<object>(Consts.Task, "AddBag", new AddRequest() { Name = name, ParentId = parentId }, log);
        }

        public async static Task<object> Move(int[] elementIds, int target, Action<string> log)
        {
            return await GetTaskHttpClient.Post2<object>(Consts.Task, Consts.Move, new MoveRequest() { ElementIds = elementIds, Target = target }, log);
        }

        public async static Task<object> Finish(int elementId, Action<string> log)
        {
            return await GetTaskHttpClient.Post2<object>(Consts.Task, Consts.Finish, new FinishRequest() { ElementId = elementId }, log);
        }

        public async static Task<object> Start(int elementId, Action<string> log)
        {
            return await GetTaskHttpClient.Post2<object>(Consts.Task, Consts.Start, new StartRequest() { ElementId = elementId }, log);
        }

        public async static Task<object> Undone(int elementId, Action<string> log)
        {
            return await GetTaskHttpClient.Post2<object>(Consts.Task, "Undone", new UndoneRequest() { ElementId = elementId }, log);
        }

        public async static Task<object> Delay(int elementId, DateTime date, Action<string> log)
        {
            return await GetTaskHttpClient.Post2<object>(Consts.Task, Consts.Delay, new DelayItemRequest() { ElementId = elementId, InitializationDate = date }, log);
        }

        public async static Task<object> Delete(int elementId, Action<string> log)
        {
            return await GetTaskHttpClient.Post2<int>(Consts.Task, Consts.Delete, elementId, (s) => Console.WriteLine(s));
        }

        public async static Task<object> AddToTomato(int[] elementIds, Action<string> log)
        {
            return await GetTaskHttpClient.Post2<object>(Consts.Task, Consts.AddToTomatoById, new AddToTomatoByIdRequest() { ElementItems = elementIds }, log);
        }
    }
}
