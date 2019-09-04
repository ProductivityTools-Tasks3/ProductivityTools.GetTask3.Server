using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetTask.Formatters
{
    public class ItemName
    {
        internal string Format(string input, PSElementView element)
        {
            var part = string.Empty;
            var domain = element.Element;
            SessionElementMetadata viewMetadata = element.SessionElement;// this.View.ItemOrder[element.ElementId];
            switch (domain.Type)
            {
                case CoreObjects.ElementType.Task:
                    part = $"{domain.Name}";
                    break;
                case CoreObjects.ElementType.TaskBag:
                    part = $"[{domain.Name}]";
                    break;
            }
            var result = input + part;
            return result;
        }
    }
}
