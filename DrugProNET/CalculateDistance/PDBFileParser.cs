using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace DrugProNET.CalculateDistance
{
    public static class PDBFileParser
    {

        public static List<Atom> GetAllAtoms(string path)
        {
            List<Atom> atoms = new List<Atom>();

            StreamReader reader = File.OpenText(path);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("ATOM"))
                {
                    Atom atom = Atom.CreateAtom(line);
                    atoms.Add(atom);
                }
                else if (line.StartsWith("HETATOM") && !line.Contains("HOH"))
                {
                    Atom hetAtom = Atom.CreateAtom(line);
                    atoms.Add(hetAtom);
                }
            }

            return atoms;
        }

    }
}