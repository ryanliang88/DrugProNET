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

        protected void Page_Loaded(object sender, EventArgs e)
        {

        }

        protected void RetrieveData(object sender, EventArgs e)
        {
            Response.Redirect("ProteinInfoResult.aspx?query_string=" + search_textBox.Text);
        }

        protected void ResetForm(object sender, EventArgs e)
        {
            search_textBox.Text = string.Empty;
        }

        // Consider using an interface!
        [WebMethod]
        public static List<string> GetAutoCompleteData(string value)
        {

            if (value == null || value == "")
            {
                Debug.WriteLine("Null");
            }
            else
            {
                Debug.WriteLine(value);
            }

            List<string> values = new List<string>
            {
                "Dog",
                "Giraffe",
                "Hippo",
                "Elephant",
                "Cat"
            };

            return FuzzySearch.Search(value, values);
        }

        protected void InitialPostBack(object sender, EventArgs r)
        {
            InitialPostBackTimer.Interval = int.MaxValue;
        }
    }
}