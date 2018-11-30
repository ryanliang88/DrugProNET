using DrugProNET.Advertisement;
using DrugProNET.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DrugProNET
{
    public partial class SNVDrugQuery : AdvertiseablePage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        [WebMethod]
        [ScriptMethod]
        public static List<string> GetAutoCompleteData(string prefixText, int count)
        {
            List<string> valuesList = new List<string>();

            const int minPrefixLength = 14;
            if (prefixText.Length >= minPrefixLength)
            {
                try
                {
                    List<SNV_Mutation> SNV_Mutation = EF_Data.GetMutationsBySNVIDKeyContains(prefixText);

                    foreach (SNV_Mutation mutation in SNV_Mutation)
                    {
                        valuesList.AddRange(new List<string> {  mutation.SNV_P1W_ID, mutation.SNV_P2W_ID, mutation.SNV_P3W_ID,
                                                    mutation.SNV_P1M1_ID, mutation.SNV_P1M2_ID,mutation.SNV_P1M3_ID,
                                                    mutation.SNV_P2M1_ID, mutation.SNV_P2M2_ID, mutation.SNV_P2M3_ID,
                                                    mutation.SNV_P3M1_ID, mutation.SNV_P3M2_ID, mutation.SNV_P3M3_ID, }
                        );
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return DataUtilities.FilterDropdownList(valuesList, prefixText, true);
        }

        protected void Generate(object sender, EventArgs e)
        {
            Response.Redirect("SNVDrugResult.aspx?query_string=" + snv_specification_textbox.Text, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void Reset(object sender, EventArgs e)
        {
            snv_specification_textbox.Text = string.Empty;
        }
    }
}