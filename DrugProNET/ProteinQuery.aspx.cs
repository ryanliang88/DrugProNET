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
    public partial class ProteinQuery : AdvertiseablePage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        protected void Search_Textbox_Changed(object sender, EventArgs e)
        {
            List<Drug_Information> drugList = new List<Drug_Information>();

            const int minPrefixLength = 3;
            if (search_textBox.Text.Length < minPrefixLength)
            {
                return;
            }

            Protein_Information protein = EF_Data.GetProteinsInfoQuery(search_textBox.Text).FirstOrDefault();

            if (protein != null)
            {
                List<PDB_Information> pdbInfoList = EF_Data.GetPDBInfoUsingProtein(protein.Uniprot_ID);

                foreach (PDB_Information pdb in pdbInfoList)
                {
                    Drug_Information drug = EF_Data.GetDrugByDrugPDBID(pdb.Drug_PDB_ID);
                    if (drug != null)
                    {
                        drugList.Add(drug);
                    }
                }
            }

            if (drugList.Count > 0)
            {
                search_drop_down.Items.Clear();

                List<string> valuesList = new List<string>();

                foreach (Drug_Information drug in drugList)
                {
                    valuesList.Add(drug.Other_Drug_Name_Alias);
                    valuesList.Add(drug.Drug_Common_Name);
                    valuesList.Add(drug.Drug_Chemical_Name);
                    valuesList.Add(drug.Compound_CAS_ID);
                    valuesList.Add(drug.PubChem_CID);
                    valuesList.Add(drug.ChEMBL_ID);
                }

                valuesList = DataUtilities.FilterDropdownList(valuesList);

                foreach (string value in valuesList)
                {
                    search_drop_down.Items.Add(new ListItem(value, value, true));
                }
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

        protected void Reset_Button_Click(object sender, EventArgs e)
        {
            search_textBox.Text = string.Empty;


            search_drop_down.Items.Clear();
            search_drop_down.Items.Add(new ListItem("Select from list of output options", "0", true));
            search_drop_down.SelectedIndex = 0;

            interaction_distance_drop_down.SelectedIndex = 9;

            protein_chain_checkbox.Checked = true;
            protein_atoms_checkbox.Checked = true;
            protein_residues_checkbox.Checked = true;
            protein_residue_number_checkbox.Checked = true;
            drug_atoms_checkbox.Checked = true;
        }

        protected void Generate_Table_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("QueryResult.aspx?query_string=" + search_textBox.Text
                + "&drug_specification=" + search_drop_down.SelectedValue
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