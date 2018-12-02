using DrugProNET.Advertisement;
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
    public partial class DrugQuery : AdvertiseablePage
    {
        private const string DROP_DOWN_PROMPT_MESSAGE = "Select from list of output options";
        private const string DROP_DOWN_NO_MATCHES_MESSAGE = "No matching items found";

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        protected void Search_Textbox_Changed(object sender, EventArgs e)
        {
            search_drop_down.Items.Clear();
            search_drop_down.Items.Add(DROP_DOWN_PROMPT_MESSAGE);

            List<Protein_Information> proteinList = new List<Protein_Information>();

            const int minPrefixLength = 3;
            if (search_textBox.Text.Length < minPrefixLength)
            {
                return;
            }
            Drug_Information drug = EF_Data.GetDrugsQuery(search_textBox.Text).FirstOrDefault();

            if (drug != null)
            {
                List<PDB_Information> pdbInfoList = EF_Data.GetPDBInfoUsingDrug(drug.Drug_PDB_ID);

                foreach (PDB_Information pdb in pdbInfoList)
                {
                    Protein_Information protein = EF_Data.GetProteinByUniprotID(pdb.Uniprot_ID);

                    if (protein != null)
                    {
                        proteinList.Add(protein);
                    }
                }
            }

            if (proteinList.Count > 0)
            {
                List<string> valuesList = new List<string>();

                foreach (Protein_Information protein in proteinList)
                {
                    valuesList.Add(protein.Uniprot_ID);
                    valuesList.Add(protein.Protein_Short_Name);
                    valuesList.Add(protein.Protein_Full_Name);
                    valuesList.Add(protein.Protein_Alias);
                    valuesList.Add(protein.NCBI_RefSeq_NP_ID);
                    valuesList.Add(protein.PhosphoNET_Name);
                    valuesList.Add(protein.PDB_Protein_Name);
                }

                valuesList = DataUtilities.FilterDropdownList(valuesList);

                foreach (string value in valuesList)
                {
                    search_drop_down.Items.Add(new ListItem(value, value, true));
                }
            }
            else
            {
                search_drop_down.Items.Clear();
                search_drop_down.Items.Add(DROP_DOWN_NO_MATCHES_MESSAGE);
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
                    List<Drug_Information> drugs = EF_Data.GetDrugsQuery(prefixText);

                    foreach (Drug_Information drug in drugs)
                    {
                        valuesList.Add(drug.Other_Drug_Name_Alias);
                        valuesList.Add(drug.Drug_Common_Name);
                        valuesList.Add(drug.Drug_Chemical_Name);
                        valuesList.Add(drug.Compound_CAS_ID);
                        valuesList.Add(drug.PubChem_CID);
                        valuesList.Add(drug.ChEMBL_ID);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return DataUtilities.FilterDropdownList(valuesList, prefixText);
        }

        protected void Reset_Button_Click(object sender, EventArgs e)
        {
            search_textBox.Text = string.Empty;

            search_drop_down.SelectedIndex = 0;

            interaction_distance_drop_down.SelectedIndex = 0;

            protein_chain_checkbox.Checked = false;
            protein_atoms_checkbox.Checked = false;
            protein_residues_checkbox.Checked = false;
            protein_residue_number_checkbox.Checked = false;
            drug_atoms_checkbox.Checked = false;
        }

        protected void Generate_Table_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("QueryResult.aspx?query_string=" + search_textBox.Text
                + "&protein_specification=" + search_drop_down.SelectedValue
                + "&interaction_distance=" + interaction_distance_drop_down.SelectedValue
                + "&protein_chain=" + protein_chain_checkbox.Checked
                + "&protein_atoms=" + protein_atoms_checkbox.Checked
                + "&protein_residues=" + protein_residues_checkbox.Checked
                + "&protein_residue_numbers=" + protein_residue_number_checkbox.Checked
                + "&drug_atoms=" + drug_atoms_checkbox.Checked, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}