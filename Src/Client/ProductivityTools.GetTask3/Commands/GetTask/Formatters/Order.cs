using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetTask.Formatters
{
    public class Order
    {
        internal string Format(string input, PSElementView element)
        {
            var part = string.Empty;
            var domain = element.Element;
            SessionElementMetadata viewMetadata = element.SessionElement;// this.View.ItemOrder[element.ElementId];
            switch (domain.Type)
            {
                case CoreObjects.ElementType.Task:
                    part = $"T{GetOrder(viewMetadata)}. ";
                    break;
                case CoreObjects.ElementType.TaskBag:
                    part = $"B{GetOrder(viewMetadata)}. ";
                    break;
            }
            var result = input + part;
            return result;
        }

        private string GetOrder(SessionElementMetadata metadata)
        {
            return metadata.Order.ToString().PadLeft(3, '0');
        }
    }
}
