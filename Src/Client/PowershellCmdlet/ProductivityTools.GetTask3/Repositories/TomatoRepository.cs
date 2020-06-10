using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.CommonConfiguration;
using ProductivityTools.GetTask3.Contract.Requests;
using ProductivityTools.GetTask3.Contract.Responses;
using ProductivityTools.GetTask3.View;
using System;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Repositories
{
    class TomatoRepository
    {
        public void AddToTomato(int[] ids)
        {
            VerboseHelper.WriteVerboseStatic("Calling AddToTomato");
            // var rootElement = GetTaskHttpClient.Post<Contract.ElementView>("List", currentNode.ToString());
            var rootElement = GetTaskHttpClient.Post2<object>(Consts.Task, Consts.AddToTomatoById, new AddToTomatoByIdRequest { ElementItems = ids }, VerboseHelper.WriteVerboseStatic).Result;
        }

        public void AddToTomato(string name, int parentId)
        {
            VerboseHelper.WriteVerboseStatic("Calling AddToTomato");
            // var rootElement = GetTaskHttpClient.Post<Contract.ElementView>("List", currentNode.ToString());
            var rootElement = GetTaskHttpClient.Post2<object>(Consts.Task, Consts.AddToTomatoByName, new AddToTomatoByNameRequest { TaskName = name, ParentId = parentId }, VerboseHelper.WriteVerboseStatic).Result;
        }

        public async Task Finish(bool finishAlsoTasks)
        {
            var request = new FinishTomatoRequest();
            request.FinishAlsoTasks = finishAlsoTasks;
            await GetTaskHttpClient.Post2<object>(Consts.Task, Consts.FinishTomato, request, VerboseHelper.WriteVerboseStatic);
        }

        public async Task<TomatoReportView> GetTomatoReport(DateTime date)
        {
            return await GetTaskHttpClient.Post2<TomatoReportView>(Consts.Task, Consts.GetTomatoReport, new GetTomatoReportRequest() { Date = date }, s => Console.WriteLine(s));
        }
    }
}
