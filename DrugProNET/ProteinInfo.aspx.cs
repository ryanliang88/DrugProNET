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
            Response.Redirect("ProteinInfoResult.aspx?query_string=" + search_textBox.Text, false);
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
            if (cached == null)
            {
                try
                {
                    List<string> valuesList = new List<string>();

                    using (SampleDatabaseEntities context = new SampleDatabaseEntities())
                    {

                        DbSet<C18OC3_DrugProNET_A_Protein_Info> dbSet = context.C18OC3_DrugProNET_A_Protein_Info;

                        foreach (C18OC3_DrugProNET_A_Protein_Info protein in dbSet.ToList())
                        {
                            if (!string.IsNullOrEmpty(protein.NCBI__Gene_ID))
                            {
                                valuesList.Add(protein.NCBI__Gene_ID);
                            }
                            if (!string.IsNullOrEmpty(protein.Protein_Short_Name))
                            {
                                valuesList.Add(protein.Protein_Short_Name);
                            }
                            if (!string.IsNullOrEmpty(protein.Protein_Full_Name))
                            {
                                valuesList.Add(protein.Protein_Full_Name);
                            }
                            if (!string.IsNullOrEmpty(protein.Uniprot_ID))
                            {
                                valuesList.Add(protein.Uniprot_ID);
                            }
                            if (!string.IsNullOrEmpty(protein.NCBI_RefSeq_NP_ID))
                            {
                                valuesList.Add(protein.NCBI_RefSeq_NP_ID);
                            }
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

            // cached may be null if (valuesList == 0). 
            // This section has not been fully tested.

            return MatchFinder<string>.FindMatches(prefixText, cached, 0, 5, 
                (a, b) => a.ToLower().StartsWith(b.ToLower()), 
                (a, b) => a.CompareTo(b));
        }
    }
}