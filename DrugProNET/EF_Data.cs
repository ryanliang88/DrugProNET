using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                // drug = dbSet.Where(d => d.ChEMBL_ID == query).ToList()[0];

                if (drug == null)
                {
                    drug = dbSet.Where(d => d.Drug_PDB_ID == query).ToList()[0];
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
                protein = dbSet.Where(p => p.Uniprot_ID == query).ToList()[0];
            }

            return protein;
        }

        public static C18OC3_DrugProNET_C_PDB_Info GetPDBInfo(string uniprot_ID, string drug_PDB_ID)
        {
            C18OC3_DrugProNET_C_PDB_Info PDBInfo;

            using (SampleDatabaseEntities context = new SampleDatabaseEntities())
            {
                DbSet<C18OC3_DrugProNET_C_PDB_Info> dbSet = context.C18OC3_DrugProNET_C_PDB_Info;
                PDBInfo = dbSet.Where(pdb =>
                pdb.Uniprot_ID == uniprot_ID &&
                pdb.Drug_PDB_ID == drug_PDB_ID
                ).ToList()[0];
            }

            return PDBInfo;

        }

    }
}