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
        public static C18OC3_DrugProNET_B_Drug_Info GetDrug(string query)
        {
            C18OC3_DrugProNET_B_Drug_Info drug = null;

            using (SampleDatabaseEntities context = new SampleDatabaseEntities())
            {
                DbSet<C18OC3_DrugProNET_B_Drug_Info> dbSet = context.C18OC3_DrugProNET_B_Drug_Info;

                query = query.ToLower();

                foreach (C18OC3_DrugProNET_B_Drug_Info d in dbSet)
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

        public static C18OC3_DrugProNET_A_Protein_Info GetProtein(string query)
        {
            C18OC3_DrugProNET_A_Protein_Info protein = null;

            using (SampleDatabaseEntities context = new SampleDatabaseEntities())
            {
                DbSet<C18OC3_DrugProNET_A_Protein_Info> dbSet = context.C18OC3_DrugProNET_A_Protein_Info;

                foreach (C18OC3_DrugProNET_A_Protein_Info p in dbSet)
                {
                    if (IsQueryInValues(query,
                        p.Protein_Short_Name,
                        p.Protein_Full_Name,
                        p.NCBI__Gene_ID,
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

        public static C18OC3_DrugProNET_C_PDB_Info GetPDBInfo(string uniprot_ID, string drug_PDB_ID)
        {
            C18OC3_DrugProNET_C_PDB_Info PDBInfo = null;

            using (SampleDatabaseEntities context = new SampleDatabaseEntities())
            {
                DbSet<C18OC3_DrugProNET_C_PDB_Info> dbSet = context.C18OC3_DrugProNET_C_PDB_Info;
                foreach (C18OC3_DrugProNET_C_PDB_Info pdb in dbSet)
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

        private static bool IsQueryInValues(string query, params string[] values)
        {
            return values.Select(value => value?.ToLower()).ToArray().Contains(query?.ToLower());
        }
    }
}