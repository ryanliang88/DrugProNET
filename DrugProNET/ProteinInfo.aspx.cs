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
    public partial class ProteinInfo : AdvertiseablePage
    {
        private static List<string> cached;

        protected void RetrieveData(object sender, EventArgs e)
        {
            string query = search_textBox.Text;
            Protein_Information protein = EF_Data.GetProtein(query);

            if (protein != null)
            {
                Response.Redirect("ProteinInfoResult.aspx?query_string=" + query, false);
                Context.ApplicationInstance.CompleteRequest();
            }
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

            if (cached == null && prefixText.Length >= minPrefixLength)
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
                                p.NCBI_Gene_ID,
                                p.PDB_Protein_Name,
                                p.Protein_Alias,
                                p.Uniprot_ID,
                                p.NCBI_RefSeq_NP_ID,
                                p.NCBI_Gene_Name,
                                p.PhosphoNET_Name);
                        }

                        if (valuesList.Count != 0)
                        {
                            cached = valuesList;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            const int maxResultSize = 5;
            return MatchFinder.FindTopNMatches(prefixText, cached, maxResultSize);
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