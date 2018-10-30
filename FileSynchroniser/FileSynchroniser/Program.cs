using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/// <summary>
/// Program for copy unique files from "original" directories to "target" dir.
/// </summary>
namespace FileSynchroniser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            string command = "";

            CommandHandler.ViewCommandList();

            while ((command = Console.ReadLine()) != "quit")
            {
                try
                {
                    if (!CommandHandler.Process(command))
                        continue;
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Change original or target directory, because now it incorrect");
                }
            }
        }
    }
}
