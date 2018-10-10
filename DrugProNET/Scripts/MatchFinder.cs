using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugProNET.Scripts
{
    public class MatchFinder<T>
    {
        public static List<string> FindMatches(string prefix, List<string> dataList, int start, int count, Comparison<string> compare)
        {
            List<string> matches = new List<string>();
            foreach (string data in dataList)
            {
                if (data.ToLower().StartsWith(prefix.ToLower())) matches.Add(data);
            }

            matches.Sort(Comparer<string>.Create(compare));

            return (matches.Count < count) ? matches.GetRange(start, matches.Count) : matches.GetRange(start, count);
        }

        public static List<T> FindMatches
            (T token,
            List<T> dataList,
            int start,
            int count,
            Compare compare,
            Comparison<T> sortCompare)
        {
            List<T> matches = new List<T>();
            foreach (T data in dataList)
            {
                if (compare(data, token))
                {
                    matches.Add(data);
                }

                matches.Sort(sortCompare);
            }

            return (matches.Count < count) ? matches.GetRange(start, matches.Count) : matches.GetRange(start, count);
        }

        public delegate bool Compare(T a, T b);
    }
}