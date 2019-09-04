using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetTask.Formatters
{
    public class ChildCount
    {
        internal string Format(string input, PSElementView element)
        {
            var part = string.Empty;
            var domain = element.Element;
            SessionElementMetadata viewMetadata = element.SessionElement;// this.View.ItemOrder[element.ElementId];
            switch (domain.Type)
            {
                case CoreObjects.ElementType.Task:
                    part = $"<{viewMetadata.ChildCount}>";
                    break;
                case CoreObjects.ElementType.TaskBag:
                    part = $"<{viewMetadata.ChildCount}t>";
                    break;
            }
            var result = input + part;
            return result;
        }
    }
}
