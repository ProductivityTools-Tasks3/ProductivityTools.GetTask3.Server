using ProductivityTools.GetTask3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Commands.GetTask.Formatters
{

    public class TomatoInfo
    {
        internal void Format(ColorString input, PSElementView element)
        {
            var part = new ColorStringItem();
            string tomatoInfo = string.Empty;
            if (element.Element.Tomatoes != null && element.Element.Tomatoes.Count > 0)
            {
                var tomatoes = element.Element.Tomatoes;
                foreach (var tomato in tomatoes)
                {
                    tomatoInfo += $" [T{tomato.TomatoId}] {DateTime.Now.Subtract(tomato.Created)} - {tomato.Status}";
                }
            }
            input.Add(part);
        }
    }
}
