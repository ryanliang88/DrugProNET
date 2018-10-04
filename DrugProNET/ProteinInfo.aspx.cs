using DrugProNET.Advertisement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DrugProNET
{
    public partial class ProteinInfo : AdvertiseablePage
    {
        protected void retrieve_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProteinInfoResult.aspx?query_string=" + search_textBox.Text);
        }

        protected void reset_button_Click(object sender, EventArgs e)
        {
            search_textBox.Text = string.Empty;
            var updatePanel = Master.FindControl("BodyContentPlaceHolder").FindControl("button_update_panel") as UpdatePanel;
            updatePanel.Update();
        }
    }
}