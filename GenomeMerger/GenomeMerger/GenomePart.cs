using GenomMerger.Utils;
using GenomMerger.Utils.DirectedGraph;
using System;

namespace GenomMerger
{
    public class GenomePart : ILeftToRightRelation<GenomePart>
    {
        /// <summary>
        /// The expected length of a genome part
        /// </summary>
        public const int GenomePartMaxLength = 1000;

        /// <summary>
        /// The content of the part
        /// </summary>
        public string Content { get; private set; }

        public GenomePart(string content)
        {
            Content = content;
        }

        public bool IsRelate(GenomePart other)
        {
            if (string.IsNullOrEmpty(Content) || string.IsNullOrEmpty(other.Content))
            {
                return true;
            }

            // Adding 1 assures we get at least half of the string for odd lengths
            int minOverlapRequirement = Math.Min((Content.Length + 1) / 2, (other.Content.Length + 1) / 2);
            int overlapCount = KMP.CalcLongestOverlappingSuffixPrefix(Content, other.Content);

            return overlapCount >= minOverlapRequirement;
        }

        public override bool Equals(Object obj)
        {
            GenomePart part = obj as GenomePart;
            if (part == null)
            {
                return false;
            }

            return Equals(part);
        }

        public bool Equals(GenomePart part)
        {
            return Content == part.Content;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
