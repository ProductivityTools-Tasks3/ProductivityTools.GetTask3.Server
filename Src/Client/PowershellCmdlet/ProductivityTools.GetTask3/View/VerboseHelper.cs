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
            WriteVerboseStatic = (s) =>
            {
                if (IsVerbose)
                {
                     Console.WriteLine(s);
                }
               
            };


        }

        public static Action<String> WriteVerboseStatic;

        public static bool IsVerbose { get; set; }

        public static void SetVerbose(bool b)
        {
            IsVerbose = b;
        }
    }
}

