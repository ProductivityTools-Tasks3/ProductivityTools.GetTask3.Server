using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.CommonConfiguration;
using ProductivityTools.GetTask3.Contract.Requests;
using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Repositories
{
    class TomatoRepository
    {
        public void AddToTomato(int[] ids)
        {
            VerboseHelper.WriteVerboseStatic("Calling AddToTomato");
            // var rootElement = GetTaskHttpClient.Post<Contract.ElementView>("List", currentNode.ToString());
            var rootElement = GetTaskHttpClient.Post2<object>(Consts.Task, Consts.AddToTomato, new AddToTomatoRequest { ElementItems = ids }, VerboseHelper.WriteVerboseStatic).Result;
        }

        public async void Finish(bool finishAlsoTasks)
        {
            var request = new FinishTomatoRequest();
            request.FinishAlsoTasks = finishAlsoTasks;
            await GetTaskHttpClient.Post2<object>(Consts.Task, Consts.FinishTomato, request, VerboseHelper.WriteVerboseStatic);
        }
    }
}
