using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugProNET.Scripts
{
    public class MatchFinder
    {
        public static List<string> FindMatches(string prefix, List<string> dataList, int start, int count, Comparison<string> compare)
        {

            List<string> matches = new List<string>();

            foreach (string data in dataList)
            {
                if (data.ToLower().StartsWith(prefix.ToLower()))
                {
                    matches.Add(data);
                }
            }

            matches.Sort(Comparer<string>.Create(compare));

            if (matches.Count < count)
            {
                return matches.GetRange(start, matches.Count);
            }

            return matches.GetRange(start, count);
        }
    }
}