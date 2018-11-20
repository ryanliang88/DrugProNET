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
using DrugProNET.Utility;

namespace DrugProNET
{
    public partial class DrugInfo : AdvertiseablePage
    {
        private static List<string> cached;

        protected void RetrieveData(object sender, EventArgs e)
        {
            string query = search_textBox.Text;
            Response.Redirect("DrugInfoResult.aspx?query_string=" + query, true);
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

                    using (DrugProNETEntities context = new DrugProNETEntities())
                    {
                        DbSet<Drug_Information> dbSet = context.Drug_Information;

                        foreach (Drug_Information d in dbSet.ToList())
                        {
                            DataUtilities.AddIfExists(valuesList,
                                d.Compound_CAS_ID,
                                d.ChEMBL_ID,
                                d.PubChem_SID,
                                d.Drug_PDB_ID,
                                d.Drug_Common_Name,
                                d.Drug_Chemical_Name,
                                d.Other_Drug_Name_Alias,
                                d.Drug_InChl,
                                d.ChemSpider_ID,
                                d.ChEBI_ID);
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

            int maxResultSize = 5;
            return MatchFinder.FindTopNMatches(prefixText, cached, maxResultSize);
        }
    }
}