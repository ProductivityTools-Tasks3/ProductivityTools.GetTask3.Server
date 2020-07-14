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

    public class Parent
    {
        internal void Format(ColorString input, string parent)
        {
            var part = new ColorStringItem();
            if (!string.IsNullOrEmpty(parent))
            {
                part.Value = $"{parent} ";
                part.Color = 70;
                input.Add(part);
            }
        }
    }
}
