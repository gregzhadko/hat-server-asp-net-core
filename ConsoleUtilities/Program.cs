using System;
using System.Collections.Generic;
using System.Linq;
using HatServer.Models;
using OldServerService;

namespace ConsoleUtilities
{
    class Program
    {
        static void Main()
        {
             Migrate();
//            while (true)
//            {
//                Console.WriteLine("Enter Command:\n");
//                var command = Console.ReadLine();
//                if (command == null)
//                {
//                    continue;
//                }
//
//                if (string.Equals(command, "exit", StringComparison.OrdinalIgnoreCase) || String.Equals(command, "e", StringComparison.OrdinalIgnoreCase))
//                {
//                    return;
//                }
//
//                var args = command.Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
//
//                if (string.Equals(args[0], "migrate", StringComparison.OrdinalIgnoreCase))
//                {
//                    Migrate(args);
//                }
//            }
        }

        private static void Migrate(params string[] args)
        {
            var service = new OldService();
            var packs = service.GetAllPacksInfo();
            var result = new List<Pack>();
            foreach (var packInfo in packs)
            {
                Console.WriteLine(packInfo);
                var pack = service.GetPackById(packInfo.Id);
                result.Add(pack);
            }

        }
    }
}
