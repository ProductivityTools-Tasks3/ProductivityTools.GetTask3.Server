using ProductivityTools.GetTask3.Colors;
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
        internal void Format(ColorString input, PSElementView element)
        {
            var part = new ColorStringItem();
            var domain = element.Element;
            SessionElementMetadata viewMetadata = element.SessionElement;// this.View.ItemOrder[element.ElementId];
            switch (domain.Type)
            {
                case CoreObjects.ElementType.Task:
                    part.Value = $"<{viewMetadata.ChildCount}>";
                    break;
                case CoreObjects.ElementType.TaskBag:
                    part.Value = $"<{viewMetadata.ChildCount}t>";
                    break;
            }
            part.Color = 215;
            input.Add(part);
        }
    }
}
