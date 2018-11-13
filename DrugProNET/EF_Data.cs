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

        public static Drug_Information GetDrugUsingDropDownName(string dropDownName)
        {
            Drug_Information drug = null;

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                DbSet<Drug_Information> drugSet = context.Drug_Information;

                foreach (Drug_Information d in drugSet)
                {
                    if (d.Drug_Name_for_Pull_Down_Menu.Equals(dropDownName))
                    {
                        drug = d;
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

        public static List<PDB_Distances> GetPDB_Distances(string pdb_entry, double interaction_distance)
        {
            List<PDB_Distances> distances = new List<PDB_Distances>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                foreach (PDB_Distances distance in context.PDB_Distances.Where(d => d.PDB_Entry == pdb_entry))
                {
                    if (double.Parse(distance.Distance) < interaction_distance)
                    {
                        distances.Add(distance);
                    }
                }
            }

            return distances.OrderBy(d => double.Parse(d.Distance)).ToList();
        }

        public static List<PDB_Interactions> GetPDB_Interactions(string uniprot_ID)
        {
            List<PDB_Interactions> interactions = new List<PDB_Interactions>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                foreach (PDB_Interactions interaction in context.PDB_Interactions.Where(i => i.UniProt_ID == uniprot_ID))
                {
                    interactions.Add(interaction);
                }
            }

            return interactions.OrderByDescending(i => double.Parse(i.Interaction_Distance_Ratio)).ToList();
        }

        public static List<PDB_Interactions> GetPDB_Interactions(string uniprot_ID, string drug_pdb_id)
        {
            List<PDB_Interactions> interactions = new List<PDB_Interactions>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                foreach (PDB_Interactions interaction in context.PDB_Interactions.Where(i => i.UniProt_ID == uniprot_ID && i.Drug_PDB_ID == drug_pdb_id))
                {
                    interactions.Add(interaction);
                }
            }

            return interactions.OrderByDescending(i => double.Parse(i.Interaction_Distance_Ratio)).ToList();
        }

        public static PDB_Interactions GetPDB_Interaction(string uniprot_ID, string drug_PDB_ID, string amino_acid_specification)
        {
            PDB_Interactions PDB_interaction = new PDB_Interactions();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                PDB_interaction = context.PDB_Interactions
                    .Where(i => i.UniProt_ID.Equals(uniprot_ID, StringComparison.OrdinalIgnoreCase)
                    && i.Drug_PDB_ID.Equals(drug_PDB_ID, StringComparison.OrdinalIgnoreCase)
                    && i.AA_Residue_Type_And_Number.Equals(amino_acid_specification, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }

            return PDB_interaction;
        }

        public static PDB_Information GetPDBInfo(string uniprot_ID, string drug_PDB_ID)
        {
            PDB_Information PDBInfo = null;

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                DbSet<PDB_Information> dbSet = context.PDB_Information;
                foreach (PDB_Information pdb in dbSet)
                {
                    if (string.Equals(pdb.Uniprot_ID, uniprot_ID, StringComparison.OrdinalIgnoreCase)
                        && string.Equals(pdb.Drug_PDB_ID, drug_PDB_ID, StringComparison.OrdinalIgnoreCase))
                    {
                        PDBInfo = pdb;
                    }
                }
            }

            return PDBInfo;
        }

        public static List<PDB_Information> GetPDBInfoUsingProtein(string uniprot_ID)
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

        public static List<PDB_Information> GetPDBInfoUsingDrug(string drug_pdb_id)
        {
            List<PDB_Information> list = new List<PDB_Information>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                DbSet<PDB_Information> dbSet = context.PDB_Information;
                foreach (PDB_Information pdb in dbSet)
                {
                    if (pdb.Drug_PDB_ID == drug_pdb_id)
                    {
                        list.Add(pdb);
                    }
                }
            }

            return list;
        }

        public static List<SNV_Mutations> GetMutations(string uniprot_ID, string drug_PDB_ID, string PDB_File_ID)
        {
            List<SNV_Mutations> SNV_mutations = new List<SNV_Mutations>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                DbSet<SNV_Mutations> mutationSet = context.SNV_Mutations;

                foreach (SNV_Mutations mutation in mutationSet)
                {
                    if (mutation.UniProt_ID.Equals(uniprot_ID, StringComparison.OrdinalIgnoreCase)
                        && mutation.Drug_PDB_ID.Equals(drug_PDB_ID, StringComparison.OrdinalIgnoreCase)
                        && mutation.PDB_File_No.Equals(PDB_File_ID, StringComparison.OrdinalIgnoreCase))
                    {
                        SNV_mutations.Add(mutation);
                    }
                }
            }

            return SNV_mutations;
        }

        public static SNV_Mutations GetMutationBySNVKey(string SNV_Key)
        {
            SNV_Mutations SNV_mutation = new SNV_Mutations();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                SNV_mutation = context.SNV_Mutations.Where(m => m.SNV_Key.Equals(SNV_Key, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            }

            return SNV_mutation;
        }

        public static SNV_Mutations GetMutationBySNVIDKey(string SNV_ID_Key)
        {
            SNV_Mutations SNV_mutation = GetMutationsBySNVIDKey(SNV_ID_Key).First();

            return SNV_mutation;
        }

        public static List<SNV_Mutations> GetMutationsBySNVIDKey(string SNV_ID_Key)
        {
            List<SNV_Mutations> SNV_mutations = new List<SNV_Mutations>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                IQueryable<SNV_Mutations> mutations = context.SNV_Mutations.Where(m =>
                       m.SNV_P1W_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P2W_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P3W_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P1M1_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P1M2_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P1M3_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P2M1_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P2M2_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P2M3_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P3M1_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P3M2_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P3M3_ID.Equals(SNV_ID_Key, StringComparison.OrdinalIgnoreCase)
                );

                SNV_mutations = mutations.ToList();
            }

            return SNV_mutations;
        }

        private static bool IsQueryInValues(string query, params string[] values)
        {
            return values.Select(value => value?.ToLower()).ToArray().Contains(query?.ToLower());
        }
    }
}