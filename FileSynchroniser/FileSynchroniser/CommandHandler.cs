using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSynchroniser
{
    class CommandHandler
    {
        public static void ViewCommandList()
        {
            Console.WriteLine("Input:\n" +
                              "quit - to leave programm\n" +
                              "get_original - to get path to original directory\n" +
                              "get_target - to get path to target directory\n" +
                              "set_original - to set path to original directory\n" +
                              "set_target - to set path to target directory\n" +
                              "sync - to start synchronisation"
                              );
        }

        public static bool Process(string command)
        {
            switch (command)
            {
                case "get_original":
                    Console.WriteLine(OptionsData.OriginalDirectory);
                    return true;
                case "get_target":
                    Console.WriteLine(OptionsData.TargetDirectory);
                    return true;
                case "set_original":
                    OptionsData.OriginalDirectory = GetArgumentFromConsole();
                    Console.WriteLine("successfully");
                    return true;
                case "set_target":
                    OptionsData.TargetDirectory = GetArgumentFromConsole();
                    Console.WriteLine("successfully");
                    return true;
                case "sync":
                    Synchroniser.Synchronise();
                    return true;
                default:
                    Console.WriteLine("Incorrect comand, try again");
                    return false;
            }
        }

        static string GetArgumentFromConsole()
        {
            Console.WriteLine("Input data:");
            return Console.ReadLine();
        }
    }
}
