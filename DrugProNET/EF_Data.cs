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
                    if (IsQueryInValues(query, d.Compound_CAS_ID, d.ChEMBL_ID, d.PubChem_CID,
                        d.PubChem_SID, d.Drug_PDB_ID))
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

        private static bool IsQueryInValues(string query, params string[] values)
        {
            return values.Select(value => value?.ToLower()).ToArray().Contains(query?.ToLower());
        }
    }
}