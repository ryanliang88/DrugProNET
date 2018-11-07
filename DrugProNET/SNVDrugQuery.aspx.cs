using DrugProNET.Advertisement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        protected void Generate_Table_Button_Click(object sender, EventArgs e)
        {

        }

        protected void Reset_Button_Click(object sender, EventArgs e)
        {
            snv_specification_textbox.Text = string.Empty;
        }
    }
}