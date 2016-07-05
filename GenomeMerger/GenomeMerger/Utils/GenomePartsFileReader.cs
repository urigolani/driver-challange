using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace GenomMerger.Utils
{
    /// <summary>
    /// This class provides reading capabilities over files containing genome parts in the following format:
    /// \>Rosalind_5929
    /// CCGACTGGACATTTCTTCGGGGTTTTTTGGGTCGCCATTGGATACATTTCATCCGTGCTA
    /// GTCGTAACCTGAGCAGAACCAGCCGCATGCTCGGAGCCTCGCAGGCGGCTGGAGATTAAC
    /// CCGACTGGACATTTCTTCGGGGTTTTTTGGGTCGCCATTGGATACATTTCATCCGTGCTA
    /// \>Rosalind_7325
    /// AGTTCCGTAACAGCCTTACGGGTACCGTCGGTTCCATTTAAACTCGTCATTTGGTTAGCT
    /// ATTGAACGTTGCCATTGGATGCCGTTCTCATCATGCTTCTGTTATGACTCAGACATGTCT
    /// </summary>
    public static class GenomePartsFileReader
    {
        private const string PartSeperator = ">.+";
        private const string WindowsLineFeed = "\r\n";
        private const string LinuxLineFeed = "\n";        

        public static IList<GenomePart> GetParts(string path)
        {
            string input = File.ReadAllText(path);
            var lineFeed = GetLineFeed(input);            
            var partSeperatorRegEx = new Regex(PartSeperator + lineFeed);
            
            // Skip the first elemnt (before the first seperator)
            var genomeParts = partSeperatorRegEx.Split(input)
                .Select(part => new GenomePart(part.Replace(lineFeed, string.Empty))).Skip(1).ToList();

            return genomeParts;

        }

        private static string GetLineFeed(string input)
        {
            var lineFeedTester = new Regex(WindowsLineFeed);

            // Assume a line feed occurs not later than 2 times a genome part size.
            var testData = input.Substring(0, Math.Min(2 * GenomePart.GenomePartMaxLength, input.Length));

            if (lineFeedTester.IsMatch(testData))
            {
                return WindowsLineFeed;
            }

            return LinuxLineFeed;
        }
    }
}
