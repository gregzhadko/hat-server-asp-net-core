using System;
using OldServerService;

namespace ConsoleUtilities
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Enter Command:\n");
                var command = Console.ReadLine();
                if (command == null)
                {
                    continue;
                }

                if (string.Equals(command, "exit", StringComparison.OrdinalIgnoreCase) || String.Equals(command, "e", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                var args = command.Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);

                if (string.Equals(args[0], "migrate", StringComparison.OrdinalIgnoreCase))
                {
                    Migrate(args);
                }
            }
        }

        private static void Migrate(string[] args)
        {
            var oldService = new OldService();
            var packs = OldService.GetAllPacksInfo();

        }
    }
}
