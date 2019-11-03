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
        public async static Task<object> Add(string name, int? parentId)
        {
            return await GetTaskHttpClient.Post2<object>("Task", "Add", new AddRequest() { Name = name, ParentId = parentId }, (s)=>Console.WriteLine(s));
        }

        public async static Task<object> AddBag(string name, int? parentId)
        {
            return await GetTaskHttpClient.Post2<object>("Task", "AddBag", new AddRequest() { Name = name, ParentId = parentId }, (s) => Console.WriteLine(s));
        }

        public async static Task<object> Finish(int elementId)
        {
            return await GetTaskHttpClient.Post2<object>("Task", "Finish", new FinishRequest() { ElementId = elementId }, (s) => Console.WriteLine(s));
        }

        public async static Task<object> Undone(int elementId)
        {
            return await GetTaskHttpClient.Post2<object>("Task", "Undone", new UndoneRequest() { ElementId = elementId }, (s) => Console.WriteLine(s));
        }

        public async static Task<object> Delay(int elementId, DateTime date)
        {
            return await GetTaskHttpClient.Post2<object>("Task", "Delay", new DelayItemRequest() { ElementId = elementId, StartDate = date }, (s) => Console.WriteLine(s));
        }

        public async static Task<object> AddToTomato(int[] elementIds)
        {
            return await GetTaskHttpClient.Post2<object>("Task", Consts.AddToTomatoById, new AddToTomatoByIdRequest() { ElementItems = elementIds }, (s) => Console.WriteLine(s));
        }
    }
}
