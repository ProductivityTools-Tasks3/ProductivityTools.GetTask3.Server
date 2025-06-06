using DbUp;
using System;
using System.Linq;
using System.Reflection;

namespace ProductivityTools.GetTask3.Server.DbUp
{
    class Program
    {
        static int Main(string[] args)
        {
            var connectionString =
          args.FirstOrDefault()
          ?? "Server=.\\SQL2022; Database=PTTasks3;Integrated Security=True;Encrypt=False";
            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
    }
}
