using DrugProNET.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace DrugProNET
{
    public partial class SNVIDQuery : Advertisement.AdvertiseablePage
    {
        private const string DROP_DOWN_PROMPT_MESSAGE = "Select from list of output options";
        private const string DROP_DOWN_NO_MATCHES_MESSAGE = "No matching items found";

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                drug_specification_drop_down.Items.Add(DROP_DOWN_PROMPT_MESSAGE);
                amino_acid_specification_drop_down.Items.Add(DROP_DOWN_PROMPT_MESSAGE);
            }
        }

        protected void Search_Textbox_Changed(object sender, EventArgs e)
        {
            drug_specification_drop_down.Items.Clear();
            drug_specification_drop_down.Items.Add(DROP_DOWN_PROMPT_MESSAGE);

            amino_acid_specification_drop_down.Items.Clear();
            amino_acid_specification_drop_down.Items.Add(DROP_DOWN_PROMPT_MESSAGE);

            Protein_Information protein = EF_Data.GetProteinsInfoQuery(search_textBox.Text).FirstOrDefault();

            if (protein != null)
            {
                List<PDB_Information> pdbList = EF_Data.GetPDBInfoUsingProtein(protein.Uniprot_ID);
                List<Drug_Information> drugList = new List<Drug_Information>();

                foreach (PDB_Information pdb in pdbList)
                {
                    Drug_Information drug = EF_Data.GetDrugByDrugPDBID(pdb.Drug_PDB_ID);
                    if (drug != null)
                    {
                        drugList.Add(drug);
                    }
                }

                if (drugList.Count > 0)
                {
                    foreach (Drug_Information drug in drugList)
                    {
                        drug_specification_drop_down.Items.Add(drug.Drug_Name_for_Pull_Down_Menu);
                    }
                }
                else
                {
                    drug_specification_drop_down.Items.Clear();
                    drug_specification_drop_down.Items.Add(DROP_DOWN_NO_MATCHES_MESSAGE);
                }
            }
        }

        protected void LoadAminoAcidDropDown(object sender, EventArgs e)
        {
            amino_acid_specification_drop_down.Items.Clear();
            amino_acid_specification_drop_down.Items.Add(DROP_DOWN_PROMPT_MESSAGE);

            try
            {
                Protein_Information protein = EF_Data.GetProtein(search_textBox.Text);
                Drug_Information drug = EF_Data.GetDrugUsingDropDownName(drug_specification_drop_down.SelectedItem.Value);
                PDB_Information PDB = EF_Data.GetPDBInfo(protein, drug);

                List<SNV_Mutations> mutations = EF_Data.GetMutations(protein.Uniprot_ID, drug.Drug_PDB_ID, PDB.PDB_File_ID);

                if (mutations.Count > 0)
                {
                    foreach (SNV_Mutations mutation in mutations)
                    {
                        amino_acid_specification_drop_down.Items.Add(mutation.SNV_Key);
                    }
                }
                else
                {
                    amino_acid_specification_drop_down.Items.Clear();
                    amino_acid_specification_drop_down.Items.Add(DROP_DOWN_NO_MATCHES_MESSAGE);
                }
            }
            catch (Exception)
            {
                amino_acid_specification_drop_down.Items.Clear();
                amino_acid_specification_drop_down.Items.Add(DROP_DOWN_PROMPT_MESSAGE);
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static List<string> GetAutoCompleteData(string prefixText, int count)
        {
            const int minPrefixLength = 3;
            List<string> valuesList = new List<string>();

            if (prefixText.Length >= minPrefixLength)
            {
                try
                {
                    List<Protein_Information> proteins = EF_Data.GetProteinsInfoQuery(prefixText);

                    foreach (Protein_Information p in proteins)
                    {
                        valuesList.Add(p.Protein_Short_Name);
                        valuesList.Add(p.Protein_Full_Name);
                        valuesList.Add(p.NCBI_Gene_ID);
                        valuesList.Add(p.PDB_Protein_Name);
                        valuesList.Add(p.Protein_Alias);
                        valuesList.Add(p.Uniprot_ID);
                        valuesList.Add(p.NCBI_RefSeq_NP_ID);
                        valuesList.Add(p.NCBI_Gene_Name);
                        valuesList.Add(p.PhosphoNET_Name);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return DataUtilities.FilterDropdownList(valuesList, prefixText, true);
        }

        protected void Generate(object sender, EventArgs e)
        {
            if (!drug_specification_drop_down.SelectedItem.Value.Equals(DROP_DOWN_PROMPT_MESSAGE)
                && !drug_specification_drop_down.SelectedItem.Value.Equals(DROP_DOWN_NO_MATCHES_MESSAGE)
                && !amino_acid_specification_drop_down.SelectedItem.Value.Equals(DROP_DOWN_PROMPT_MESSAGE)
                && !amino_acid_specification_drop_down.SelectedItem.Value.Equals(DROP_DOWN_NO_MATCHES_MESSAGE)
                && !search_textBox.Text.Equals(string.Empty))
            {
                Response.Redirect("SNVIDResult.aspx?query_string=" + search_textBox.Text
                    + "&drug_specification=" + drug_specification_drop_down.SelectedItem.Value
                    + "&snv_id_key=" + amino_acid_specification_drop_down.SelectedItem.Value, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void Reset(object sender, EventArgs e)
        {
            search_textBox.Text = string.Empty;
            drug_specification_drop_down.Items.Clear();
            drug_specification_drop_down.Items.Add(DROP_DOWN_PROMPT_MESSAGE);
            amino_acid_specification_drop_down.Items.Clear();
            amino_acid_specification_drop_down.Items.Add(DROP_DOWN_PROMPT_MESSAGE);
        }

        private static bool HasMatch(string searchTerm, params string[] values)
        {
            foreach (string value in values)
            {
                if (!string.IsNullOrEmpty(value) && value.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        private static void AddIfExists(List<string> list, params string[] values)
        {
            foreach (string value in values)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    list.Add(value);
                }
            }
        }
    }
}