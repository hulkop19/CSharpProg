using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSynchroniser
{
    static class Synchroniser
    {
        /// <summary>
        /// Start synchronisation.
        /// </summary>
        public static void Synchronise()
        {
            var targetDir = new DirectoryInfo(OptionsData.TargetDirectory);
            var originalDir = new DirectoryInfo(OptionsData.OriginalDirectory);

            var filesInOriginal = new HashSet<FileInfo>(originalDir.GetFiles());
            var filesInTarget = new HashSet<FileInfo>(targetDir.GetFiles());

            foreach (FileInfo file in filesInOriginal)
            {
                if (!filesInTarget.Select(el => el.Name).Contains(file.Name))
                {
                    try
                    {
                        file.CopyTo(OptionsData.TargetDirectory + "\\" + file.Name);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{file.Name} didn't copy because of:");
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            Console.WriteLine("Synchronised");
        }
    }
}
