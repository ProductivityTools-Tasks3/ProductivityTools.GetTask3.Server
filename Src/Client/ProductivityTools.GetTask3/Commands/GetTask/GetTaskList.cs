using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetTask
{
    public class GetTaskList : PSCmdlet.PSCommandPT<GetTask3Cmdlet>
    {
        ElementView RootElement;
        PSView View;
        protected override bool Condition => true;

        public GetTaskList(GetTask3Cmdlet cmdlet) : base(cmdlet) { }


        private string FormatRow(ElementView element)
        {
            var result = string.Empty;
            ViewMetadata viewMetadata = this.View.ItemOrder[element.ElementId];
            switch (element.Type)
            {
                case CoreObjects.ElementType.Task:
                    return $"T{GetOrder(viewMetadata)}. {element.Name} <{viewMetadata.ChildCount}>";
                case CoreObjects.ElementType.TaskBag:
                    return $"B{GetOrder(viewMetadata)}. [{element.Name}] <{viewMetadata.ChildCount}>";
            }
            return "empty";
        }

        protected override void Invoke()
        {

            //var xxxz = Cmdlet.SessionState.PSVariable.Get("fsa");
            //List<string> xxx = new List<string>();
            //xxx.Add("p");
            //xxx.Add("d");
            //Environment.SetEnvironmentVariable("pawel", "Fdsa");
            //Cmdlet.SessionState.PSVariable.Set("fsa", xxx);
            GetTasks();
            WriteOutput("GetTaskList");
            WriteOutput($"+[{RootElement.Name}]");
            DisplayList(CoreObjects.ElementType.TaskBag);
            DisplayList(CoreObjects.ElementType.Task);
            //root.Elements

            // var z = GetTaskHttpClient.Get<ElementView>("Add", "{Name: \"XXX\",ParentId: 3 }");
        }

        private void DisplayBags()
        {
            WriteOutput($"+[{RootElement.Name}]");
            foreach (var element in RootElement.Elements)
            {
                if (element.Type == CoreObjects.ElementType.TaskBag)
                {
                    WriteOutput(FormatRow(element));
                }
            }
        }

        private void DisplayList(CoreObjects.ElementType type)
        {
            foreach (var element in RootElement.Elements.Where(x => x.Type == type))
            {
                WriteOutput(FormatRow(element));
            }
        }

        private void GetTasks()
        {
            RootElement = GetTaskHttpClient.Get<ElementView>("List", string.Empty);
            View = new PSView();
            View.ItemOrder = new Dictionary<int, ViewMetadata>();

            int taskcounter = 0;
            int bagcounter = 0;

            foreach (var element in RootElement.Elements)
            {
                switch (element.Type)
                {
                    case CoreObjects.ElementType.TaskBag:
                        View.ItemOrder.Add(element.ElementId, new ViewMetadata() { ElementId = element.ElementId, Order = bagcounter, ChildCount=element.ChildElementsAmount() });
                        bagcounter++;
                        break;
                    case CoreObjects.ElementType.Task:
                        View.ItemOrder.Add(element.ElementId, new ViewMetadata() { ElementId = element.ElementId, Order = taskcounter, ChildCount = element.ChildElementsAmount() });
                        taskcounter++;
                        break;
                    default:
                        break;
                }
            }
        }

        private string GetOrder(ViewMetadata metadata)
        {
            return metadata.Order.ToString().PadLeft(3, '0');
        }
    }
}
