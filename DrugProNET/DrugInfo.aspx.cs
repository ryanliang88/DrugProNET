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
    public partial class DrugInfo : AdvertiseablePage
    {
        protected void RetrieveData(object sender, EventArgs e)
        {
            string query = search_textBox.Text;
            Response.Redirect("DrugInfoResult.aspx?query_string=" + query, false);
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
                    List<Drug_Information> drugs = EF_Data.GetDrugsInfoQuery(prefixText);

                    foreach (Drug_Information drug in drugs)
                    {
                        valuesList.Add(drug.Drug_Name_for_Pull_Down_Menu);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return DataUtilities.FilterDropdownList(valuesList);
        }
    }
}