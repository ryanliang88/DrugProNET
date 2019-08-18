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
        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static Drug_Information GetDrugUsingDropDownName(string query)
        {
            Drug_Information drug = null;

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                drug = context.Drug_Information.Where(d =>
                    d.Drug_Name_for_Pull_Down_Menu.ToLower().Contains(query.ToLower())
                ).FirstOrDefault();
            }

            return drug;
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static Drug_Information GetDrugByDrugPDBID(string query)
        {
            Drug_Information drug = null;

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                drug = context.Drug_Information.Where(d =>
                    d.Drug_PDB_ID.ToLower().Contains(query.ToLower())
                ).FirstOrDefault();
            }

            return drug;
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static List<Drug_Information> GetDrugsQuery(string query)
        {
            List<Drug_Information> drugs = new List<Drug_Information>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                drugs = context.Drug_Information.Where(d =>
                    d.Other_Drug_Name_Alias.ToLower().Contains(query.ToLower())
                    || d.Drug_Common_Name.ToLower().Contains(query.ToLower())
                    || d.Drug_Chemical_Name.ToLower().Contains(query.ToLower())
                    || d.Compound_CAS_ID.ToLower().Contains(query.ToLower())
                    || d.PubChem_CID.ToLower().Contains(query.ToLower())
                    || d.ChEMBL_ID.ToLower().Contains(query.ToLower())
                ).ToList();
            }

            return drugs;
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static List<Drug_Information> GetDrugsInfoQuery(string query)
        {
            List<Drug_Information> drugs = new List<Drug_Information>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                drugs = context.Drug_Information.Where(d =>
                    d.Compound_CAS_ID.ToLower().Contains(query.ToLower())
                    || d.ChEMBL_ID.ToLower().Contains(query.ToLower())
                    || d.PubChem_SID.ToLower().Contains(query.ToLower())
                    || d.Drug_PDB_ID.ToLower().Contains(query.ToLower())
                    || d.Drug_Common_Name.ToLower().Contains(query.ToLower())
                    || d.Drug_Chemical_Name.ToLower().Contains(query.ToLower())
                    || d.Other_Drug_Name_Alias.ToLower().Contains(query.ToLower())
                    || d.Drug_InChl.ToLower().Contains(query.ToLower())
                    || d.ChemSpider_ID.ToLower().Contains(query.ToLower())
                    || d.ChEBI_ID.ToLower().Contains(query.ToLower())
                ).ToList();
            }

            return drugs;
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static Protein_Information GetProteinByUniprotID(string uniProt_ID)
        {
            Protein_Information protein = null;

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                protein = context.Protein_Information.Where(p =>
                    p.UniProt_ID.ToLower().Contains(uniProt_ID.ToLower())
                ).FirstOrDefault();
            }

            return protein;
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static Protein_Information GetProtein(string query)
        {
            Protein_Information protein = null;

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                protein = context.Protein_Information.Where(p =>
                    p.Protein_Short_Name.ToLower().Contains(query.ToLower())
                    || p.Protein_Full_Name.ToLower().Contains(query.ToLower())
                    || p.NCBI_Gene_ID.ToLower().Contains(query.ToLower())
                    || p.PDB_Protein_Name.ToLower().Contains(query.ToLower())
                    || p.Protein_Alias.ToLower().Contains(query.ToLower())
                    || p.UniProt_ID.ToLower().Contains(query.ToLower())
                    || p.NCBI_RefSeq_NP_ID.ToLower().Contains(query.ToLower())
                    || p.NCBI_Gene_Name.ToLower().Contains(query.ToLower())
                    || p.PhosphoNET_Name.ToLower().Contains(query.ToLower())
                ).FirstOrDefault();
            }

            return protein;
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static List<Protein_Information> GetProteinsInfoQuery(string query)
        {
            List<Protein_Information> proteins = new List<Protein_Information>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                proteins = context.Protein_Information.Where(p =>
                    p.Protein_Short_Name.ToLower().Contains(query.ToLower())
                    || p.Protein_Full_Name.ToLower().Contains(query.ToLower())
                    || p.NCBI_Gene_ID.ToLower().Contains(query.ToLower())
                    || p.PDB_Protein_Name.ToLower().Contains(query.ToLower())
                    || p.Protein_Alias.ToLower().Contains(query.ToLower())
                    || p.UniProt_ID.ToLower().Contains(query.ToLower())
                    || p.NCBI_RefSeq_NP_ID.ToLower().Contains(query.ToLower())
                    || p.NCBI_Gene_Name.ToLower().Contains(query.ToLower())
                    || p.PhosphoNET_Name.ToLower().Contains(query.ToLower())
                ).ToList();
            }

            return proteins;
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static PDB_Interaction GetPDB_Interaction(string uniProt_ID, string drug_PDB_ID, string amino_acid_specification)
        {
            PDB_Interaction PDB_interaction = new PDB_Interaction();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                PDB_interaction = context.PDB_Interaction.Where(i =>
                    i.UniProt_ID.ToLower().Contains(uniProt_ID.ToLower())
                    && i.Drug_PDB_ID.ToLower().Contains(drug_PDB_ID.ToLower())
                    && (i.AA_Residue_Type + "-" + i.UniProt_Residue_Number).ToLower().Contains(amino_acid_specification.ToLower())
                ).FirstOrDefault();
            }

            return PDB_interaction;
        }

        /// <summary>
        /// Author: Andy Tang
        /// </summary>
        public static PDB_Information GetPDBInfo(Protein_Information protein, Drug_Information drug)
        {
            PDB_Information PDBInfo = null;

            if (protein == null || drug == null)
            {
                return PDBInfo;
            }

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                PDBInfo = context.PDB_Information.Where(pdb =>
                    pdb.UniProt_ID.Equals(protein.UniProt_ID, StringComparison.OrdinalIgnoreCase)
                    && pdb.Drug_PDB_ID.Equals(drug.Drug_PDB_ID, StringComparison.OrdinalIgnoreCase)
                ).FirstOrDefault();
            }

            return PDBInfo;
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static List<PDB_Information> GetPDBInfoUsingProtein(string uniProt_ID)
        {
            List<PDB_Information> PDB_Information = new List<PDB_Information>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                PDB_Information = context.PDB_Information.Where(pdb =>
                    pdb.UniProt_ID.ToLower().Contains(uniProt_ID.ToLower())
                ).ToList();
            }

            return PDB_Information;
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static List<PDB_Information> GetPDBInfoUsingDrug(string drug_PDB_ID)
        {
            List<PDB_Information> PDB_information = new List<PDB_Information>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                PDB_information = context.PDB_Information.Where(p =>
                    p.Drug_PDB_ID.ToLower().Contains(drug_PDB_ID.ToLower())
                ).ToList();
            }

            return PDB_information;
        }

        public static Dictionary<PDB_Distance, string> GetDistanceAndUniprotResidueNumbers(string pdb_File_ID, double interaction_distance)
        {
            var DistanceAndUniprotResidueNumbers = new Dictionary<PDB_Distance, string>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                DistanceAndUniprotResidueNumbers = context.PDB_Distance.Where(distance =>
                   distance.PDB_File_ID == pdb_File_ID
                   && distance.Distance <= interaction_distance)
                   .Join(context.PDB_Interaction,
                   distance => new
                   {
                       PDB_File_ID = distance.PDB_File_ID.ToLower(),
                       Protein_Residue = distance.Protein_Residue,
                       Protein_Residue_Number = distance.Protein_Residue_Number,
                   },
                   interaction => new
                   {
                       PDB_File_ID = interaction.PDB_File_ID.ToLower(),
                       Protein_Residue = interaction.AA_Residue_Type,
                       Protein_Residue_Number = interaction.PDB_Residue_Number,
                   },
                   (distance, interaction) => new
                   {
                       distance,
                       interaction,
                   }
                ).OrderBy(a => a.distance.Distance)
                .ToDictionary(a => a.distance, a => a.interaction.PDB_Residue_Number);
            }

            return DistanceAndUniprotResidueNumbers;
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static List<SNV_Mutation> GetMutations(string uniProt_ID, string drug_PDB_ID, string pdb_File_ID)
        {
            List<SNV_Mutation> SNV_Mutation = new List<SNV_Mutation>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                SNV_Mutation = context.SNV_Mutation.Where(m =>
                    m.UniProt_ID.Equals(uniProt_ID, StringComparison.OrdinalIgnoreCase)
                    && m.Drug_PDB_ID.Equals(drug_PDB_ID, StringComparison.OrdinalIgnoreCase)
                    && m.PDB_File_ID.Equals(pdb_File_ID, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            return SNV_Mutation;
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static SNV_Mutation GetMutationBySNVKey(string snv_Key)
        {
            SNV_Mutation SNV_mutation = new SNV_Mutation();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                SNV_mutation = context.SNV_Mutation.Where(m =>
                    m.SNV_Key.Equals(snv_Key, StringComparison.OrdinalIgnoreCase)
                ).SingleOrDefault();
            }

            return SNV_mutation;
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static List<SNV_Mutation> GetMutationsBySNVIDKey(string snv_ID_Key)
        {
            List<SNV_Mutation> SNV_Mutation = new List<SNV_Mutation>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                SNV_Mutation = context.SNV_Mutation.Where(m =>
                    m.SNV_P1W_ID.Equals(snv_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P2W_ID.Equals(snv_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P3W_ID.Equals(snv_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P1M1_ID.Equals(snv_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P1M2_ID.Equals(snv_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P1M3_ID.Equals(snv_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P2M1_ID.Equals(snv_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P2M2_ID.Equals(snv_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P2M3_ID.Equals(snv_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P3M1_ID.Equals(snv_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P3M2_ID.Equals(snv_ID_Key, StringComparison.OrdinalIgnoreCase)
                    || m.SNV_P3M3_ID.Equals(snv_ID_Key, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            return SNV_Mutation;
        }

        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static List<SNV_Mutation> GetMutationsBySNVIDKeyContains(string snv_ID_Key)
        {
            List<SNV_Mutation> SNV_Mutation = new List<SNV_Mutation>();

            using (DrugProNETEntities context = new DrugProNETEntities())
            {
                SNV_Mutation = context.SNV_Mutation.Where(m =>
                    m.SNV_P1W_ID.ToLower().Contains(snv_ID_Key.ToLower())
                    || m.SNV_P2W_ID.ToLower().Contains(snv_ID_Key.ToLower())
                    || m.SNV_P3W_ID.ToLower().Contains(snv_ID_Key.ToLower())
                    || m.SNV_P1M1_ID.ToLower().Contains(snv_ID_Key.ToLower())
                    || m.SNV_P1M2_ID.ToLower().Contains(snv_ID_Key.ToLower())
                    || m.SNV_P1M3_ID.ToLower().Contains(snv_ID_Key.ToLower())
                    || m.SNV_P2M1_ID.ToLower().Contains(snv_ID_Key.ToLower())
                    || m.SNV_P2M2_ID.ToLower().Contains(snv_ID_Key.ToLower())
                    || m.SNV_P2M3_ID.ToLower().Contains(snv_ID_Key.ToLower())
                    || m.SNV_P3M1_ID.ToLower().Contains(snv_ID_Key.ToLower())
                    || m.SNV_P3M2_ID.ToLower().Contains(snv_ID_Key.ToLower())
                    || m.SNV_P3M3_ID.ToLower().Contains(snv_ID_Key.ToLower())
                ).ToList();
            }

            return SNV_Mutation;
        }
    }
}
