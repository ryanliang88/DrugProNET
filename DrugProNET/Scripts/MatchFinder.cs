using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugProNET.Scripts
{
    public class MatchFinder
    {
        public static List<string> FindTopNMatches(string prefix, List<string> dataList, int n)
        {
            return FindMatches(prefix, dataList, 0, n,
                (a, b) => a.ToLower().StartsWith(b.ToLower()),
                (a, b) => a.CompareTo(b));
        }

        private static List<string> FindMatches(string token, List<string> dataList, int start, int count, Compare compare, Comparison<string> sortCompare)
        {
            List<string> matches = new List<string>();

            if (dataList != null)
            {
                foreach (string data in dataList)
                {
                    if (compare(data, token))
                    {
                        matches.Add(data);
                    }

                    matches.Sort(sortCompare);
                }

                matches = (matches.Count < count) ? matches.GetRange(start, matches.Count) : matches.GetRange(start, count);
            }
           
            return matches;
        }

        public delegate bool Compare(string a, string b);
    }
}