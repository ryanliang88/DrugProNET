using System;
using System.Collections.Generic;
using System.Web;

namespace DrugProNET.Advertisement
{
    public class RandomPicker
    {
        static Random r = new Random(Guid.NewGuid().GetHashCode());

        /// <summary>
        /// Author: Andy Tang
        /// </summary>
        public static T PickRandom<T>(List<T> items, int start, int end)
        {
            return items[r.Next(start, end)];
        }
    }

}
