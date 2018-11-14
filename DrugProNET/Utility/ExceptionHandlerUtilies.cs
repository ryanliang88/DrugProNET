using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DrugProNET.Utility
{
    public static class ExceptionHandler
    {
        private const string DEFAULT_MSG = "Could not complete your request";

        public static void DisplayAlert(Page page, string url, string message = DEFAULT_MSG)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "_alert_", "alert('" + message + "');", true);
            page.Response.Redirect(url, true);
        }
    }
}