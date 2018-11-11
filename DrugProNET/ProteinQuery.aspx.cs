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
    public partial class ProteinQuery : Advertisement.AdvertiseablePage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            loading_label.Visible = false;
        }

        protected void Search_Textbox_Changed(object sender, EventArgs e)
        {
            if (search_textBox.Text != string.Empty)
            {
                loading_label.Visible = true;
            }

            List<Drug_Information> drugList = new List<Drug_Information>();

            Protein_Information protein = EF_Data.GetProtein(search_textBox.Text);

            if (protein != null)
            {
                List<PDB_Information> pdbInfoList = EF_Data.GetPDBInfoUsingProtein(protein.Uniprot_ID);

                foreach (PDB_Information pdb in pdbInfoList)
                {
                    Drug_Information drug = EF_Data.GetDrug(pdb.Drug_PDB_ID);
                    if (drug != null)
                    {
                        drugList.Add(drug);
                    }
                }
            }

            if (drugList.Count > 0)
            {
                search_drop_down.Items.Clear();

                foreach (Drug_Information drug in drugList)
                {
                    search_drop_down.Items.Add(drug.Drug_Name_for_Pull_Down_Menu);
                }

                loading_label.Visible = false;
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static List<string> GetAutoCompleteData(string prefixText, int count)
        {
            int minPrefixLength = 3;
            List<string> valuesList = new List<string>();

            if (prefixText.Length >= minPrefixLength)
            {
                try
                {
                    using (DrugProNETEntities context = new DrugProNETEntities())
                    {

                        DbSet<Protein_Information> dbSet = context.Protein_Information;

                        foreach (Protein_Information p in dbSet.ToList())
                        {
                            AddIfExists(valuesList,
                                p.Protein_Short_Name,
                                p.Protein_Full_Name,
                                p.PDB_Protein_Name,
                                p.Protein_Alias,
                                p.Uniprot_ID,
                                p.NCBI_RefSeq_NP_ID,
                                p.PhosphoNET_Name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            int maxResultSize = 5;
            return MatchFinder.FindTopNMatches(prefixText, valuesList, maxResultSize);
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
                + "&drug_specification=" + search_drop_down.SelectedValue
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