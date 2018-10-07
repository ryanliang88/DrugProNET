using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugProNET.Scripts
{
    public class Pair<T, U>
    {
        public T First { get; set; }
        public U Second { get; set; }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }
    }
}