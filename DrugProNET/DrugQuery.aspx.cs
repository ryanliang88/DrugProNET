using DrugProNET.Advertisement;
using DrugProNET.Scripts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DrugProNET
{
    public partial class DrugQuery : AdvertiseablePage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        protected void Search_Textbox_Changed(object sender, EventArgs e)
        {
            List<Protein_Information> proteinList = new List<Protein_Information>();

            Drug_Information drug = EF_Data.GetDrugUsingDropDownName(search_textBox.Text);

            if (drug != null)
            {
                List<PDB_Information> pdbInfoList = EF_Data.GetPDBInfoUsingDrug(drug.Drug_PDB_ID);

                foreach (PDB_Information pdb in pdbInfoList)
                {
                    Protein_Information protein = EF_Data.GetProtein(pdb.Uniprot_ID);
                    if (protein != null)
                    {
                        proteinList.Add(protein);
                    }
                }
            }

            if (proteinList.Count > 0)
            {
                search_drop_down.Items.Clear();
                foreach (Protein_Information protein in proteinList)
                {
                    search_drop_down.Items.AddRange(
                        GenerateListItemsFromValues(
                            protein.Uniprot_ID,
                            protein.Protein_Short_Name,
                            protein.Protein_Full_Name,
                            protein.Protein_Alias,
                            protein.NCBI_RefSeq_NP_ID,
                            protein.PhosphoNET_Name,
                            protein.PDB_Protein_Name).ToArray());
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
                    Drug_Information drug = EF_Data.GetDrug(prefixText);

                    using (DrugProNETEntities context = new DrugProNETEntities())
                    {
                        DbSet<Drug_Information> dbSet = context.Drug_Information;

                        foreach (Drug_Information d in dbSet.ToList())
                        {
                            if (d.Other_Drug_Name_Alias.StartsWith(prefixText, StringComparison.OrdinalIgnoreCase) 
                                || d.Drug_Common_Name.StartsWith(prefixText, StringComparison.OrdinalIgnoreCase)
                                || d.Drug_Chemical_Name.StartsWith(prefixText, StringComparison.OrdinalIgnoreCase)
                                || d.Compound_CAS_ID.StartsWith(prefixText, StringComparison.OrdinalIgnoreCase)
                                || d.PubChem_CID.StartsWith(prefixText, StringComparison.OrdinalIgnoreCase)
                                || d.PubChem_SID.StartsWith(prefixText, StringComparison.OrdinalIgnoreCase)
                                || d.ChEMBL_ID.StartsWith(prefixText, StringComparison.OrdinalIgnoreCase))
                            {
                                valuesList.Add(d.Drug_Name_for_Pull_Down_Menu);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            const int maxResultSize = 5;
            return valuesList.Count >= maxResultSize ? valuesList.GetRange(0, 5) : valuesList;
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

        private List<ListItem> GenerateListItemsFromValues(params string[] values)
        {
            List<ListItem> listItemList = new List<ListItem>();

            foreach (string value in values)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    listItemList.Add(new ListItem(value, value, true));
                }
            }

            return listItemList;
        }
    }
}