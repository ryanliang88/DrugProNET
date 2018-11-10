using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DrugProNET
{
    public partial class SNVIDQuery : Advertisement.AdvertiseablePage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }



        private static void AddIfExists(List<string> list, params string[] values)
        {
            foreach (string value in values)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    list.Add(value);
                }
            }
        }
    }
}