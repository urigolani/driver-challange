using GenomMerger;
using GenomMerger.Utils;
using System;
using System.IO;

namespace GenomeMergerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Please provide a path to a genome set");
                Environment.Exit(2);
            }

            var path = args[0];
            if (!File.Exists(path))
            {
                Console.Error.WriteLine("File Not Found. Please make sure the path is correct");
                Environment.Exit(2);
            }

            var parts = GenomePartsFileReader.GetParts(path);
            var result = GenomeMerger.Merge(parts);
            if(result == null)
            {
                Console.WriteLine("Did not find a result for the set provided");
            } else
            {
                Console.WriteLine("Result:");
                Console.WriteLine(result);
            }

            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
