using ProductivityTools.GetTask3.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.View
{
    public static class ElementViewExtensions
    {
        public static int ChildElementsAmount(this ElementView that)
        {
            if (that.Elements==null || that.Elements.Count==0)
            {
                return 0;
            }
            else
            {
                var result=that.Elements.Sum(x => x.ChildElementsAmount());
                result += that.Elements.Count;
                return result;
            }
        }
    }
}
