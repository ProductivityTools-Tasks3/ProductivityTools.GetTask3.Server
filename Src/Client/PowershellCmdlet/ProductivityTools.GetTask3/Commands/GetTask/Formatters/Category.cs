using ProductivityTools.ConsoleColor;
using ProductivityTools.GetTask3.Contract;
using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetTask.Formatters
{

    public class Category
    {
        internal void Format(ColorString input, ElementView element)
        {
            if (!string.IsNullOrEmpty(element.Category))
            {
                var part = new ColorStringItem($"[{element.Category}] ",70);
                input.Add(part);
            }
        }
    }
}
