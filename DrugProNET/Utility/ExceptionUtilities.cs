using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DrugProNET.Utility
{
    public static class ExceptionUtilities
    {
        private const string DEFAULT_MSG = "Could not complete your request";

        // Cannot display message as the page will still load but with blanks
        public static void DisplayAlert(Page page, string url, string message = DEFAULT_MSG)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), 
                "_redirect_", "alert('" + message + "');" + "window.location='" + page.Request.ApplicationPath + url + "';", true);
        }

        public static void Redirect(Page page, string url)
        {
            page.Response.Redirect(url);
            page.Response.End();
        }
    }
}