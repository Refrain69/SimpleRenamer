using System;
using System.IO;

namespace SimpleRenamer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (Directory.Exists(args[0]))
                {
                    RenameFiles(args[0]);
                    Console.WriteLine("Operation finished.");
                }
                else if (File.Exists(args[0]))
                {
                    Console.WriteLine("Please provide a directory, not a file.");
                }
                else
                {
                    Console.WriteLine("Directory does not exist.");
                }
            }

            if (args.Length == 0)
            {
                Console.Write("Dir:");
                string directory = Console.ReadLine().Trim('"');
                if (Directory.Exists(directory))
                {
                    RenameFiles(directory);
                }
                else
                {
                    Console.WriteLine("Directory does not exist.");
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void RenameFiles(string directory)
        {
            int count = 1;
            string[] sortedFiles = Directory.GetFiles(directory);
            Array.Sort(sortedFiles, new WindowsSorter());

            foreach (string file in sortedFiles)
            {
                string newFileName = Path.Combine(Path.GetDirectoryName(file), count.ToString("D3") + Path.GetExtension(file));
                File.Move(file, newFileName);
                count++;
            }
            foreach (string subDirectory in Directory.GetDirectories(directory))
            {
                RenameFiles(subDirectory);
            }
        }
    }
}
