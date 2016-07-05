using GenomMerger.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace GenomMergerTest.UtilsTest
{
    [TestClass]
    public class GenomePartsFileReaderTest
    {
        private const string TestDataDir = "TestData";
        private const string PartA = "GCGCCCGGGGCAAGAGTCATTATACTTGAGAATATACATTTAACAGCGGGCTCATAGCAC\nAGCAGTTATAAAAGAGGCAGATTCCGACCCCTTAGGGACTATAGGTTTTCTGGGTGTCAA\nCCCTTCGTGGTACTAGCGGGCGGCAATCCCTTAAATATCTAGGCCCGTACCATCGACAGG\nGCAGATTGGCATTTATTTCTGGGCCTACACAAATGCGAATAACGATTACTAGCCGAGCAC\nTCTCTTTTATGACAAGAGGGTTTCACATCCTCTTAGTATGTCCCGCACAATAACTTTTGC\nGCAAAAGTGCTCTTGTTTATTTAGTGAATATCCAGGGGTAAGTGCTAATATGTGAGCGCT\nGCTCTGCAGTTATGATGACCGGTTCATAGTTGCTCGTATTGGCACATGTACGGAAAGGCA\nAATCATTCAAGTTTTGGGGAGGAATATGGCGGCCCACCTCCTTGGCCCACGGAGTCCCGC\nCTTTGCGCTGGTGAAGGTGTCACTGCCCCCTAGATCATACCGCACTAAGTGGGGCCTGTA\nTCGAAGGGTTTCTTGTTTAGGACAATCTTTGCTGTCTTCTTCACCCAATCACAGCTTGAT\nTGAATGAGTAATCACTGAACATCGGGAGGCTCCATTTACCTAAGAATTCAGACGCCTGGT\nTGGATCTGTGAGACACTCACGTATGGATGTTAACGACAAGGCACCTGGGGTACTTGTAAC\nAAGTCATACACTATGCTGGGTACTACCCCAACGCAGAGTTTATGCCCCCATGCCACCGTT\nGCCTTCACGTCACCACCATTCATCATCTAGCGAGGCCCTGGTGTCCTATTTTGTAGAGGG\nGATATCTAGTGAGGGCCTATCATGCCACGGACGAGAACATAAAAATCTATAAAGAGCCTG\nGTTCGATTCTGATAACCTAAAAGCTAAAACTGAGATGTTTGGCCCTTCGCGGGACCTGCA\nGGTCGAGCTCCACCGGAGGGGTCCTCTCTAGAGG";
        private const string PartB = "TAGTGTGGGAAGCTTAACAAGCTGCGAACTGCAACTCAAACTTAAGACACCGCTCCGAAC\nTTCAAAAACGGGCCATGCGCATCGAGGGACGATAAACGTACGAAGTCACAGAGCCTCGCT\nCGTGGACAGTCCACAGCGTTCCCACGTTGTTGAGGTTGAGTCCTACTAAACGTAGGATTA\nTTGTAGTGCCCCACTAGGAGCTGTTTTACTAGGTTGCCCTGACCCGTTGCTAATTAACCA\nACGGTTTTGGTTCTTTCCTGCGGTAATTAGAGGCTAATGAGTATACCACGTCTCGTTGTC\nCGCGGCCTCCTTCCTGGGCGTTGAGCTTTGGACGACATTCCAAGCCAAAATTCGATGGCT\nTGACACGGCATCGGACTGAATGTTGATGAACCATTTCCTACGCCATGCCTTGCCCATTTA\nCTCATAGGAGTTATCAGATGAGCTATATAGGAAACGTGCCAGGTTGAGCAGCGTGTAAGA\nGTGAATACCCTCCATATCGGTCGCTAAAGGTCTTCTTGAAATGCTTCGCTCTTCGGAAAA\nTTCGCGGCGTCGTGTGCCCTGGGCGGAGTTACATCAATTTGTTCAGGCAATGCACACTCC\nGAGTCCTACTCTGCCAAACCTAGTGCGATCTGACTCCAATTACAGTTTTTAGGGGGTGTC\nTTCAAACACAAAGTCTCCGACCGTCCTGTCGCGGGTAGGGTGTTTAGAAATGACGTAATG\nGAACATACCAATGCTCCCTGGGTTGTATGGAGTAGCACCTATACAAAGGTAAATGGAACT\nCCTAGGGAGGAATGACTGCCGCATTCGACATCACCCAATCTATGCTCGGGTCCTTTCTCT\nAAAGCTGGTGCGTTAACGTGATTGCAGGTCCGGAACACAAGGCACGAGTGCGTTATAGCT\nCAAAGAACGTATATGCAAGGCCGTGTGGGTCCATTTTATGCCTGACCTGCGTCCCTTTTA\nCATCTGGTATGCCACCAAACTTAGACAAGGCGCCCTCCGG";
        private const string PartC = "GGCGAACATAGGTTAAGTCAAGACCGGAGCCTCTTCGATGAAGAAGAATCATCATATGCA\nTTGAGGAATAACCCTACCTACGCAATCGATAAATTGGGTGAATGAACCCAAGGGGCTGGA\nAGTATATATCATGGGAAAAGTCAGACATAGCTTTCAAGCCATAATAGGGAATCCGTAGAC\nCATGAGGTAAGCGCTTGTATTTTCAAGTCCCGTACATAAGAGTGCTCAGTCACAAGGATT\nAAGGACTGACGAGTTTCATCGAACGACAAGTGACTCATATGCGCTACAGTACCGGGGCGA\nTACCCCTGGCAAGGCAGACACAAGAGGGATAATAGGAGTCTCCGTACAGTAGCGGGAACC\nATCTACTCTTTGTTGTCTATTTCGCACAAACTGTGTCGTGTACTGGTAGACCCTTGGTTC\nTTGAGTCATGCTGGGACACCCAACCAGTAGGGAGAATTTTTCAGCAGCGAGGCAACGCGC\nATTGACTATTAAACAAGACACAGCAAAGATATACTACTTAGGTATATGCTTTTAGAGGAG\nCCTATTCAATTATAGTAACCATCGTTTGTTCGCCAGTTAGATGTCGGTATGTACGACGAC\nAACGGGGATTCCTAACGCCCTCTAGGATACTACCAATATCCTCTAGGCCACCTTACATTC\nATAGCACGTTCCAATACCGTTAGCTTCCATGGAGGTGAAATGAACCCACGGTATTCCGTG\nTAATGCTGCTTGTTGTGAGCTTGGGCCAGACCGATTTTAATATAACCCGAGGCTGGTGGG\nGCTGTTGGGAAACGGCCGTGCGGAGCCGGAATGATGTTGGCTTTTCTACAAGCCTTGTCA\nTAAGAAAGGGCCAGCCGCGCCTCATTCATTTATGCCGCATCGGAGTGGTCACTTTCGAGT\nACCTCTAGTGCGGATAGCACGTAGGACATTATTATTCGTGGTAATATGCCTCGGGCTTAA\nAGAACAAGTCGACCCTATCA";
        private const string LinuxLineFeed = "\n";
        private const string WindowsLineFeed = "\r\n";
        private const string Seperator = ">Rosalind_";
        
        readonly Random _random = new Random(2);

        [TestInitialize]
        public void PrepareTestDataFolder()
        {
            Directory.CreateDirectory(TestDataDir);
        }

        [TestCleanup]
        public void DeleteTestDataFolder()
        {
            Directory.Delete(TestDataDir, recursive: true);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void GetParts_WhenFileDoesntExist()
        {
            GenomePartsFileReader.GetParts(Path.Combine(TestDataDir, "NoSuchFileExist.txt"));
        }

        [TestMethod]
        public void GetParts_WhenNoPartsExist()
        {
            var path = CreateTestFile(new string[] {});
            var parts = GenomePartsFileReader.GetParts(path);
            Assert.AreEqual(0, parts.Count);

            File.Delete(path);
        }

        [TestMethod]
        public void GetParts_WhenSinglePartExist()
        {
            var path = CreateTestFile(new string[] { PartA });
            var parts = GenomePartsFileReader.GetParts(path);
            Assert.AreEqual(1, parts.Count);
            Assert.IsTrue(parts.Contains(new GenomMerger.GenomePart(PartA.Replace(LinuxLineFeed, string.Empty))));

            File.Delete(path);
        }

        [TestMethod]
        public void GetParts_WhenSeveralPartsExist()
        {
            var path = CreateTestFile(new string[] { PartA, PartB, PartC});
            var parts = GenomePartsFileReader.GetParts(path);

            Assert.AreEqual(3, parts.Count);
            Assert.IsTrue(parts.Contains(new GenomMerger.GenomePart(PartA.Replace(LinuxLineFeed, string.Empty))));
            Assert.IsTrue(parts.Contains(new GenomMerger.GenomePart(PartB.Replace(LinuxLineFeed, string.Empty))));
            Assert.IsTrue(parts.Contains(new GenomMerger.GenomePart(PartC.Replace(LinuxLineFeed, string.Empty))));

            File.Delete(path);
        }

        [TestMethod]
        public void GetParts_WhenSinglePartAndWindowsLineFeed()
        {
            var path = CreateTestFile(new string[] { PartA }, WindowsLineFeed);
            var parts = GenomePartsFileReader.GetParts(path);

            Assert.AreEqual(1, parts.Count);
            Assert.IsTrue(parts.Contains(new GenomMerger.GenomePart(PartA.Replace(LinuxLineFeed, string.Empty))));

            File.Delete(path);
        }

        private string CreateTestFile(string[] parts, string lineFeed = LinuxLineFeed)
        {
            string filePath = Path.Combine(TestDataDir, (Guid.NewGuid()).ToString());

            using (var fileStream = File.OpenWrite(filePath))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                foreach (var part in parts)
                {
                    streamWriter.Write(Seperator + _random.Next(10000) + lineFeed);
                    streamWriter.Write(part.Replace(LinuxLineFeed, lineFeed) + lineFeed);
                }
            }

            return filePath;
        }
    }
}
