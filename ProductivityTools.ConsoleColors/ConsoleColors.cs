using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.ConsoleColors
{
    public static class ConsoleColor
    {
        //https://jonasjacek.github.io/colors/

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int handle);

        private static void ChangeMode()
        {
            var handle = GetStdHandle(-11);
            int mode;
            GetConsoleMode(handle, out mode);
            SetConsoleMode(handle, mode | 0x4);
        }

        public static void WriteInColor(ColorString input, bool newLine=true)
        {
            ChangeMode();
            foreach (var item in input)
            {
                if (item.Color != null)
                {
                    Console.Write($"\x1b[38;5;{item.Color}m");
                }
                Console.Write(item.Value);
            }
            if (newLine)
            {
                Console.WriteLine();
            }
        }
    }
}
