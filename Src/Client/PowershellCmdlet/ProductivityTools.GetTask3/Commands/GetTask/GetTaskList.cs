using ProductivityTools.ConsoleColors;
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
using ConsoleColor = ProductivityTools.ConsoleColors.ConsoleColor;

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

        private ColorString FormatRow(PSElementView element)
        {
            var result = new ColorString();
            new Order().Format(result, element);
            new Category().Format(result, element);
            new Tomato().Format(result, element);
            new ItemName().Format(result, element);
            new ChildCount().Format(result, element);
            return result;
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
                DisplayList(TaskStructure.Elements,CoreObjects.ElementType.TaskBag);
                DisplayList(TaskStructure.Elements,CoreObjects.ElementType.Task);
            }
        }

        private void Setcolor(ElementView element)
        {
            if (element.Type != ElementType.TaskBag)
            {
                if (element.Delayed())
                {
                    Console.ForegroundColor = System.ConsoleColor.Red;
                }
                if (element.Finished.HasValue)
                {
                    Console.ForegroundColor = System.ConsoleColor.DarkGray;
                }
            }
        }

        private void ResetColor()
        {
            Console.ResetColor();
        }


        private void WriteToScreen(PSElementView element)
        {
            var colorString = FormatRow(element);
            ConsoleColor.WriteInColor(colorString);
            ResetColor();
        }

        private void DisplayBags()
        {
            WriteOutput($"+[{TaskStructure.CurrentElement.Name}]");
            foreach (var element in TaskStructure.Elements)
            {
                if (element.Element.Type == CoreObjects.ElementType.TaskBag)
                {
                    WriteToScreen(element);
                }
            }
        }

        private void DisplayList(IEnumerable<PSElementView> elements, CoreObjects.ElementType type)
        {
            foreach (var element in elements.Where(x => x.Element.Type == type))
            {
                WriteToScreen(element);
            }
        } 
    }
}
