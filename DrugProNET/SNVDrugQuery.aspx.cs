using DrugProNET.Advertisement;
using System;
using System.Collections.Generic;
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
            const int minPrefixLength = 3;
            List<string> valuesList = new List<string>();

            if (prefixText.Length >= minPrefixLength)
            {
                try
                {
                    // Add stuff to values list for autocomplete results
                    valuesList.AddRange(new string[] { "A", "B", "C", "D" });
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
            Response.Redirect("SNVDrugResult.aspx?query_string=" + snv_specification_textbox.Text);
        }

        protected void Reset(object sender, EventArgs e)
        {
            snv_specification_textbox.Text = string.Empty;
        }
    }
}