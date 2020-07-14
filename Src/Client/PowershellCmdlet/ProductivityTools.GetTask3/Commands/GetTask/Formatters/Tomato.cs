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

    public class Tomato
    {
        internal void Format(ColorString input, ElementView element)
        {
           
            string tomatoInfo = string.Empty;
            if (element.Tomatoes != null && element.Tomatoes.Count > 0)
            {
                var tomatoes = element.Tomatoes;
                foreach (var tomato in tomatoes)
                {
                    var finishDate = tomato.Finished.HasValue ? tomato.Finished.Value : DateTime.Now;
                    tomatoInfo = $"|{finishDate.Subtract(tomato.Created).ToString(@"hh\:mm")}| ";
                    var part = new ColorStringItem();
                    part.Value = tomatoInfo;
                    if (tomato.Status == CoreObjects.Tomato.Status.Finished)
                    {
                        part.Color = 8;
                    }
                    else
                    {
                        part.Color = 220;
                    }
                    input.Add(part);
                }
            }
          
        }
    }
}
