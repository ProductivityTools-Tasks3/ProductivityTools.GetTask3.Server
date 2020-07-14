using ProductivityTools.ConsoleColor;
using ProductivityTools.GetTask3.Commands.GetTask.Formatters;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.Domain;
using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductivityTools.GetTask3.Commands.GetTaskReport
{
    class MultiLevel : PSCmdlet.PSCommandPT<GetTaskReportCmdlet>
    {
        App.Task TaskStructure { get; set; }

        public MultiLevel(GetTaskReportCmdlet cmdletType) : base(cmdletType)
        {
        }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            this.TaskStructure = TaskStructureFactory.Get(this.Cmdlet);
            if (TaskStructure.CurrentElement == null)
            {
                WriteOutput("No task found");
            }
            else
            {
                string parent = $"+[{TaskStructure.CurrentElement.Name}]";
                DisplayNode(parent, TaskStructure.Structure.Elements);
            }
        }

        private void DisplayNode(string parentpath, IEnumerable<ElementView> elements)
        {
            DisplayList(parentpath, elements, CoreObjects.ElementType.TaskBag);
            DisplayList(parentpath, elements, CoreObjects.ElementType.Task);
        }

        public void DisplayList(string parentPath, IEnumerable<ElementView> elements, CoreObjects.ElementType type)
        {
            foreach (var element in elements.Where(x => x.Type == type))
            {
                WriteToScreen(parentPath, element);
                if (element.Elements.Any())
                {
                    string parent = parentPath + $".[{element.Name}]";
                    DisplayNode(parent, element.Elements);
                }
            }
        }

        private static void WriteToScreen(string parentPath, ElementView element)
        {
            var colorString = FormatRow(parentPath, element);
            ConsoleColors.WriteInColor(colorString);
            ResetColor();
        }

        private static void ResetColor()
        {
            Console.ResetColor();
        }

        private static ColorString FormatRow(string parentpath, ElementView element)
        {
            var result = new ColorString();
            new Parent().Format(result, parentpath);
            new Category().Format(result, element);
            new Tomato().Format(result, element);
            new ItemName().Format(result, element);
            return result;
        }
    }
}
