using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace DrugProNET
{
    public static class EF_Data
    {
        public static Drug_Information GetDrug(string query)
        {
            Drug_Information drug = null;

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                DbSet<Drug_Information> dbSet = context.Drug_Information;

                query = query.ToLower();

                foreach (Drug_Information d in dbSet)
                {
                    if (IsQueryInValues(query,
                        d.Compound_CAS_ID,
                        d.ChEMBL_ID,
                        d.PubChem_SID,
                        d.Drug_PDB_ID,
                        d.Drug_Common_Name,
                        d.Drug_Chemical_Name,
                        d.Other_Drug_Name_Alias,
                        d.Drug_InChl,
                        d.ChemSpider_ID,
                        d.ChEBI_ID))
                    {
                        drug = d;
                        break;
                    }
                }
            }

            return drug;
        }

        public static Protein_Information GetProtein(string query)
        {
            Protein_Information protein = null;

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                DbSet<Protein_Information> dbSet = context.Protein_Information;

                foreach (Protein_Information p in dbSet)
                {
                    if (IsQueryInValues(query,
                        p.Protein_Short_Name,
                        p.Protein_Full_Name,
                        p.NCBI_Gene_ID,
                        p.PDB_Protein_Name,
                        p.Protein_Alias,
                        p.Uniprot_ID,
                        p.NCBI_RefSeq_NP_ID,
                        p.NCBI_Gene_Name,
                        p.PhosphoNET_Name))
                    {
                        protein = p;
                    }
                }
            }

            return protein;
        }

        public static PDB_Information GetPDBInfo(string uniprot_ID, string drug_PDB_ID)
        {
            PDB_Information PDBInfo = null;

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                DbSet<PDB_Information> dbSet = context.PDB_Information;
                foreach (PDB_Information pdb in dbSet)
                {
                    if (pdb.Uniprot_ID.ToLower() == uniprot_ID.ToLower()
                        && pdb.Drug_PDB_ID.ToLower() == drug_PDB_ID.ToLower())
                    {
                        PDBInfo = pdb;
                    }
                }
            }

            return PDBInfo;
        }

        public static List<PDB_Information> GetPDBInfo(string uniprot_ID)
        {
            List<PDB_Information> list = new List<PDB_Information>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                DbSet<PDB_Information> dbSet = context.PDB_Information;
                foreach (PDB_Information pdb in dbSet)
                {
                    if (pdb.Uniprot_ID == uniprot_ID)
                    {
                        list.Add(pdb);
                    }
                }
            }

            return list;
        }

        private static bool IsQueryInValues(string query, params string[] values)
        {
            return values.Select(value => value?.ToLower()).ToArray().Contains(query?.ToLower());
        }
    }
}