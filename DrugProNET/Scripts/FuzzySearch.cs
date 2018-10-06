using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Source code
// https://www.codeproject.com/Articles/36869/Fuzzy-Search

namespace DrugProNET.Scripts
{
    public class FuzzySearch
    {
        private const double DEFAULT_FUZZINESS = 0.75;

        public static List<string> Search(string word, List<string> list, double fuzziness = DEFAULT_FUZZINESS)
        {
            List<string> foundWords = new List<string>();

            foreach (string s in list) {
                int distance = LevenshteinDistance(word, s);
                int length = Math.Max(word.Length, s.Length);
                double score = 1.0 - (distance / length);
                if (score > fuzziness) foundWords.Add(s);
            }

            return foundWords;
        }

        public static int LevenshteinDistance(string src, string dest)
        {
            int[,] d = new int[src.Length + 1, dest.Length + 1];
            int i, j, cost;
            char[] str1 = src.ToCharArray();
            char[] str2 = dest.ToCharArray();

            for (i = 0; i <= str1.Length; i++)
            {
                d[i, 0] = i;
            }
            for (j = 0; j <= str2.Length; j++)
            {
                d[0, j] = j;
            }
            for (i = 1; i <= str1.Length; i++)
            {
                for (j = 1; j <= str2.Length; j++)
                {
                    cost = str1[i - 1] == str2[j - 1] ? 0 : 1;
                    d[i, j] = Math.Min(d[i - 1, j] + 1, Math.Min(d[i, j - 1] + 1, d[i - 1, j - 1] + cost));

                    if ((i > 1) && (j > 1) && (str1[i - 1] ==
                        str2[j - 2]) && (str1[i - 2] == str2[j - 1]))
                    {
                        d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
                    }
                }
            }

            return d[str1.Length, str2.Length];
        }
    }
}