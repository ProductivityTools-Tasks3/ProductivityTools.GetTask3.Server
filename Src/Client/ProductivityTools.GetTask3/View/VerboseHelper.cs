using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.GetTask3.View
{
    public static class VerboseHelper
    {
        static VerboseHelper()
        {
            WriteVerboseStatic = (s) => Console.WriteLine(s);
        }

        public static Action<String> WriteVerboseStatic;
    }
}
