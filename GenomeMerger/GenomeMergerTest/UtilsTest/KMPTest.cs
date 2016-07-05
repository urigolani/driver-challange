using GenomMerger.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace GenomMergerTest.UtilsTest
{
    [TestClass]
    public class KMPTest
    {
        [TestMethod]
        public void CalcLongestOverlappingSuffixPrefix_GetZeroWhenFirstStringIsNull()
        {
            Assert.AreEqual(0, KMP.CalcLongestOverlappingSuffixPrefix(null, "a"));
        }

        [TestMethod]
        public void CalcLongestOverlappingSuffixPrefix_GetZeroWhenSecondStringIsNull()
        {
            Assert.AreEqual(0, KMP.CalcLongestOverlappingSuffixPrefix("a", null));
        }

        [TestMethod]
        public void CalcLongestOverlappingSuffixPrefix_TwoSameLengthStringsOverlapPartially()
        {
            string first = "ABCD";
            string second = "CDEF|";

            Assert.AreEqual(2, KMP.CalcLongestOverlappingSuffixPrefix(first, second));
        }

        [TestMethod]
        public void CalcLongestOverlappingSuffixPrefix_FirstStringLongerOverlap()
        {
            StringBuilder first = new StringBuilder();
            string overlapPart = "acacacaca";

            first.Append("adadadadad").Append(overlapPart);
            string second = overlapPart + "rfrf";

            Assert.AreEqual(overlapPart.Length, KMP.CalcLongestOverlappingSuffixPrefix(first.ToString(), second));
        }

        [TestMethod]
        public void CalcLongestOverlappingSuffixPrefix_SecondStringLongerOverlap()
        {
            StringBuilder first = new StringBuilder();
            string overlapPart = "acacac";
            first.Append("adadadad").Append(overlapPart);
            string second = overlapPart + "rfrfrfrfrfrfrfrf";

            Assert.AreEqual(overlapPart.Length, KMP.CalcLongestOverlappingSuffixPrefix(first.ToString(), second));
        }

        [TestMethod]
        public void CalcLongestOverlappingSuffixPrefix_SecondFitsEntirely()
        {
            StringBuilder first = new StringBuilder();
            string second = "dedede";
            first.Append("acacacbab").Append(second);

            Assert.AreEqual(second.Length, KMP.CalcLongestOverlappingSuffixPrefix(first.ToString(), second));
        }

        [TestMethod]
        public void CalcLongestOverlappingSuffixPrefix_SecondFitsEntirelyBeforeTheEndOfFirst()
        {
            StringBuilder first = new StringBuilder();
            string second = "eeee";
            first.Append("aaa").Append(second).Append("d");

            Assert.AreEqual(0, KMP.CalcLongestOverlappingSuffixPrefix(first.ToString(), second));
        }
    }
}
