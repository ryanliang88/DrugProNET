using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugProNET.CalculateDistance
{
    public class Atom
    {
        public static Atom CreateAtom(string line)
        {
            List<string> data = line.Split(' ').ToList();

            data = data.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            Atom atom = new Atom(data[0], data[1], data[2], data[3], data[4],
                Convert.ToDouble(data[5]), Convert.ToDouble(data[6]), Convert.ToDouble(data[7]), Convert.ToDouble(data[8]));

            return atom;
        }

        public Atom(string recordName, string serial, string atomName, string residue, string chainType, double resSeq, double x, double y, double z)
        {
            this.RecordName = recordName;
            this.Serial = serial;
            this.AtomName = atomName;
            this.Residue = residue;
            this.ChainType = chainType;
            this.ResSeq = resSeq;
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        
        public string RecordName { get; set; }
        public string Serial { get; set; }
        public string AtomName { get; set; }
        public string Residue { get; set; }
        public string ChainType { get; set; }
        public double ResSeq { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}