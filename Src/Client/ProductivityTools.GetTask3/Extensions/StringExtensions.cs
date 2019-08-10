using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.Extensions
{
    public static class StringExtensions
    {
        public static int[] GetIds(this string s)
        {
            var r=s.Split(' ').Select(x => int.Parse(x)).ToArray();
            return r; 
        }

        public static bool NorNullNotEmpty(this string s)
        {
            var r = s != null && s != string.Empty;
            return r;
        }
    }
}
