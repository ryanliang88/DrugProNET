using DrugProNET.Advertisement;
using DrugProNET.Scripts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DrugProNET
{
    public partial class ProteinInfo : AdvertiseablePage
    {
        protected void RetrieveData(object sender, EventArgs e)
        {
            Response.Redirect("ProteinInfoResult.aspx?query_string=" + search_textBox.Text);
        }

        protected void ResetForm(object sender, EventArgs e)
        {
            search_textBox.Text = string.Empty;
            var updatePanel = Master.FindControl("BodyContentPlaceHolder").FindControl("button_update_panel") as UpdatePanel;
            updatePanel.Update();
        }

        [WebMethod]
        public static List<string> GetAutoCompleteData(string value)
        {
            Debug.WriteLine(value);
            List<string> values = new List<string>();
            values.Add("Dog");
            values.Add("Giraffe");
            values.Add("Hippo");
            values.Add("Elephant");

            return FuzzySearch.Search(value, values);
        }
    }
}