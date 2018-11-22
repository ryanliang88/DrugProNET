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
    public partial class ProteinInfo : AdvertiseablePage
    {
        protected void RetrieveData(object sender, EventArgs e)
        {
            string query = search_textBox.Text;
            Response.Redirect("ProteinInfoResult.aspx?query_string=" + query, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void ResetForm(object sender, EventArgs e)
        {
            search_textBox.Text = string.Empty;
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
    }
}