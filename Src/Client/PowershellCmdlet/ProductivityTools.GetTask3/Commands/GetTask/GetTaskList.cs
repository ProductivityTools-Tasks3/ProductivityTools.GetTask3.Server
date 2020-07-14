using ProductivityTools.ConsoleColor;
using ProductivityTools.GetTask3.App;
using ProductivityTools.GetTask3.Client;
using ProductivityTools.GetTask3.Commands.GetTask.Formatters;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.CoreObjects;
using ProductivityTools.GetTask3.Domain;
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
        App.Task TaskStructure { get; set; }

        protected override bool Condition => true;

        public GetTaskList(GetTask3Cmdlet cmdlet) : base(cmdlet)
        {
            this.TaskStructure = TaskStructureFactory.Get(cmdlet);
        }

        private int GetTomatoTime(DateTime dt)
        {
            //pw:change it to provider
            var x = DateTime.Now - dt;
            return x.Minutes;
        }

        protected override void Invoke()
        {
            VerboseHelper.WriteVerboseStatic("GetTaskList Invoke");
            App.Task ts = TaskStructureFactory.Get(this.Cmdlet);
            WriteOutput("GetTaskList");
            if (TaskStructure.CurrentElement == null)
            {
                WriteOutput("No task found");
            }
            else
            {
                WriteOutput($"+[{TaskStructure.CurrentElement.Name}]");
                DisplayList(TaskStructure.Elements, CoreObjects.ElementType.TaskBag);
                DisplayList(TaskStructure.Elements, CoreObjects.ElementType.Task);
            }
        }



        public void DisplayList(IEnumerable<PSElementView> elements, CoreObjects.ElementType type)
        {
            foreach (var element in elements.Where(x => x.Element.Type == type))
            {
                WriteToScreen(element);
            }
        }

        private void WriteToScreen(PSElementView element)
        {
            var colorString = FormatRow(element);
            ConsoleColors.WriteInColor(colorString);
            ResetColor();
        }

        private void ResetColor()
        {
            Console.ResetColor();
        }

        private ColorString FormatRow(PSElementView element)
        {
            var result = new ColorString();
            new Order().Format(result, element);
            new Category().Format(result, element.Element);
            new Tomato().Format(result, element.Element);
            new ItemName().Format(result, element.Element);
            new ChildCount().Format(result, element);
            return result;
        }
    }
}
