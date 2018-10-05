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
        UpdatePanel panel;

        protected void Page_Loaded(object sender, EventArgs e)
        {
            panel = Master.FindControl("BodyContentPlaceHolder").FindControl("button_update_panel") as UpdatePanel;
            panel.Update();
        }

        protected void RetrieveData(object sender, EventArgs e)
        {
            Response.Redirect("ProteinInfoResult.aspx?query_string=" + search_textBox.Text);
        }

        protected void ResetForm(object sender, EventArgs e)
        {
            search_textBox.Text = string.Empty;
            panel.Update();
        }

        // Consider using an interface!
        [WebMethod]
        public static List<string> GetAutoCompleteData(string value)
        {
            Debug.WriteLine(value);
            List<string> values = new List<string>
            {
                "Dog",
                "Giraffe",
                "Hippo",
                "Elephant",
                "Cat",
                "Zebra",
                "Kangaroo",
                "Koala",
                "Ant",
                "Whale",
                "Dolphin",
                "Shark",
                "Eagle",
                "Java",
                "Python",
                "C#"
            };

            return FuzzySearch.Search(value, values);
        }
    }
}