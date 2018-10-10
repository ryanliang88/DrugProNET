using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Xml;
using System.IO;
using System.Diagnostics;

using DrugProNET.Advertisement;
using System.Web.Services;
using System.Web.Script.Services;
using DrugProNET.Scripts;
using System.Data.Entity;

namespace DrugProNET
{
    public partial class DrugInfo : AdvertiseablePage
    {
        private static List<string> cached;

        protected void RetrieveData(object sender, EventArgs e)
        {
            Response.Redirect("DrugInfoResult.aspx?query_string=" + search_textBox.Text, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void ResetForm(object sender, EventArgs e)
        {
            search_textBox.Text = string.Empty;
        }

        private class Pair<T, U>
        {
            T first;
            U second;
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
                        DbSet<C18OC3_DrugProNET_B_Drug_Info> dbSet = context.C18OC3_DrugProNET_B_Drug_Info;

                        foreach (C18OC3_DrugProNET_B_Drug_Info drug in dbSet.ToList())
                        {
                            if (!string.IsNullOrEmpty(drug.Drug_Common_Name))
                            {
                                valuesList.Add(drug.Drug_Common_Name);
                            }
                            if (!string.IsNullOrEmpty(drug.Compound_CAS_ID))
                            {
                                valuesList.Add(drug.Compound_CAS_ID);
                            }
                            if (!string.IsNullOrEmpty(drug.PubChem_CID)) // CID or SID?
                            {
                                valuesList.Add(drug.PubChem_CID);
                            }
                            if (!string.IsNullOrEmpty(drug.ChEMBL_ID))
                            {
                                valuesList.Add(drug.ChEMBL_ID);
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