using System;
using System.Text;

namespace GenomMerger.Utils
{
    /// <summary>
    /// This class implements a modified version of the Knuth Morris Pratt Algorithm <see cref="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/>, by finding the longest prefix of word W which is a suffix of word S
    /// It provides useful methods for joining two strings to a third string with minimal length such that the first string is a prefix and the second is a suffix.
    /// </summary>
    public static class KMP
    {
        /// <summary>
        /// Returns the size of the longest suffix of <paramref name="first"/> which is a prefix of <paramref name="second"/>
        /// Runtime for this operation is O(<paramref name="first"/> + <paramref name="second"/>).
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <example>CalcLongestOverlappingSuffixPrefix("ABCD", "CDEF") => "2" </example>
        public static int CalcLongestOverlappingSuffixPrefix(string first, string second)
        {
            if(string.IsNullOrEmpty(first)|| string.IsNullOrEmpty(second))
            {
                return 0;
            }

            int m = Math.Max(0, first.Length - second.Length);

            return Search(first, second, m);
        }

        private static int Search(string first, string second, int m)
        {
            var table = BuildKMPTable(second);
            int i = 0;

            while ((m + i) < first.Length)
            {
                if (second[i] == first[m + i])
                {
                    i++;
                    if (i == second.Length)
                    {
                        return i;
                    }
                    
                }
                else
                {
                    m += i - table[i];
                    if (i > 0)
                    {
                        i = table[i];
                    }
                }
            }

            return i;
        }
        
        private static int[] BuildKMPTable(string word)
        {
            int[] table = new int[word.Length];

            // The position we calculate within the table
            int pos = 2;

            // The candidate we evaluate against pos within word
            int cnd = 0;

            // The first values are fixed. -1 is set for the use of the algoritm, mainly in order, to stop the search.
            // 0 is set since there is no proper prefix to a size 1 word.
            table[0] = -1;
            table[1] = 0;

            while (pos < word.Length)
            {
                // 
                if (word[pos - 1] == word[cnd])
                {
                    // First case: The substring continues
                    table[pos] = cnd + 1;
                    cnd++;
                    pos++;
                }
                else if (cnd > 0)
                {
                    // Second case: it doesn't, but we can fall back
                    cnd = table[cnd];
                }
                else
                {
                    // Third case: we have run out of candidates
                    table[pos] = 0;
                    pos++;
                }
            }

            return table;
        }
    }
}