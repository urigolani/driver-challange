using GenomMerger.Utils;
using GenomMerger.Utils.DirectedGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenomMerger
{
    public static class GenomeMerger
    {
        /// <summary>
        /// Returns the genom sequence built from merging parts with the following reason:
        /// Part A merges to Part B from the left iff
        /// either the suffix(A) with |suffix(A)| > 0/5*|A| is substring of B starting at position 0
        /// or prefix(B) with |prefix(B)| > 0/5*|B| is substring of A starting at position |A|-|suffix(B)|
        /// for example: 
        /// Example input:
        /// >Frag_56
        /// ATTAGACCTG
        /// >Frag_57
        /// CCTGCCGGAA
        /// >Frag_58
        /// AGACCTGCCG
        /// >Frag_59
        /// GCCGGAATAC
        /// 
        /// Example output:
        /// ATTAGACCTGCCGGAATAC
        /// </summary>
        /// <param name="genomParts">Genom parts</param>
        /// <returns></returns>
        public static string Merge(IEnumerable<GenomePart> genomeParts)
        {
            if(genomeParts == null)
            {
                return null;
            }

            var genomePartsGraph = DirectedGraph<GenomePart>.BuildGraph(genomeParts.ToList());

            // Find the hemiltonian path
            var hemiltonianPath = HemiltonianPath.Find(genomePartsGraph);

            if (hemiltonianPath != null)
            {
                return MergeParts(hemiltonianPath.ToList());
            }

            // Could not assemble parts
            return null;
        }

        private static string MergeParts(IList<GenomePart> genomeParts)
        {
            // Since Genom parts overlap by at least half, each one contribues at most 0.5*GenomeParMaxLength. The first and last contribute 1 more.
            StringBuilder genome = new StringBuilder((int)(genomeParts.Count * GenomePart.GenomePartMaxLength * 0.5) + GenomePart.GenomePartMaxLength);
            foreach(var part in genomeParts)
            {
                int overlapCount = KMP.CalcLongestOverlappingSuffixPrefix(genome.ToString(), part.Content);
                genome.Append(part.Content, overlapCount, part.Content.Length - overlapCount);
            }

            return genome.ToString();
        }
    }
}

