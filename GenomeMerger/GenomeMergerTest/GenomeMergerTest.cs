using GenomMerger;
using GenomMerger.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenomMergerTest
{
    /// <summary>
    /// Summary description for GenomeMerger
    /// </summary>
    [TestClass]
    public class GenomeMergerTest
    {
        private const string ShortGenomeSetFileName = "ShortGenomeTestSet.txt";
        private const string CodingChallangeFileName = "coding_challenge_data_set.txt";

        /// <summary>
        /// The following test is mainly useful for debugging purposes as the Dataset it small
        /// </summary>
        [TestMethod]
        [DeploymentItem("TestData\\" + ShortGenomeSetFileName)]
        public void Merge_WhenMergingShortGenomeSetSuccesfuly()
        {
            var parts = GenomePartsFileReader.GetParts(ShortGenomeSetFileName);
            var genome = GenomeMerger.Merge(parts);

            Assert.IsNotNull(genome);
            foreach(var part in parts)
            {
                Assert.IsTrue(genome.Contains(part.Content), string.Format("part: {0} is missing", part.Content));
            }
        }

        [TestMethod]
        [DeploymentItem("TestData\\" + CodingChallangeFileName)]
        public void Merge_CodingChallange()
        {
            var parts = GenomePartsFileReader.GetParts(CodingChallangeFileName);
            var genome = GenomeMerger.Merge(parts);

            Assert.IsNotNull(genome);
            foreach (var part in parts)
            {
                Assert.IsTrue(genome.Contains(part.Content), string.Format("part: {0} is missing", part.Content));
            }
        }
    }
}
