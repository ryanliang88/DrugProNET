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

            Protein_Information protein = EF_Data.GetProtein(search_textBox.Text);

            if (protein != null)
            {
                List<PDB_Information> pdbList = EF_Data.GetPDBInfoUsingProtein(protein.Uniprot_ID);
                List<Drug_Information> drugList = new List<Drug_Information>();

                foreach (PDB_Information pdb in pdbList)
                {
                    Drug_Information drug = EF_Data.GetDrug(pdb.Drug_PDB_ID);
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
                PDB_Information PDB = EF_Data.GetPDBInfo(protein.Uniprot_ID, drug.Drug_PDB_ID);

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
            catch (Exception ex)
            {
                amino_acid_specification_drop_down.Items.Clear();
                amino_acid_specification_drop_down.Items.Add(DROP_DOWN_PROMPT_MESSAGE);
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
                    using (DrugProNETEntities context = new DrugProNETEntities())
                    {
                        DbSet<Protein_Information> dbSet = context.Protein_Information;

                        foreach (Protein_Information p in dbSet)
                        {
                            if (HasMatch(prefixText,
                                p.NCBI_Gene_Name,
                                p.Protein_Full_Name,
                                p.Protein_Short_Name,
                                p.Uniprot_ID,
                                p.NCBI_RefSeq_NP_ID))
                            {
                                if (!string.IsNullOrEmpty(p.Uniprot_ID))
                                {
                                    valuesList.Add(p.Uniprot_ID);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            const int maxAutocompleteLength = 5;

            return valuesList.Count >= maxAutocompleteLength ? valuesList.GetRange(0, 5) : valuesList;
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
                    + "&snv_id_key=" + amino_acid_specification_drop_down.SelectedItem.Value, true);
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