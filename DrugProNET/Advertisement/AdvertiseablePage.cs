using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DrugProNET.Advertisement
{
    public class AdvertiseablePage : System.Web.UI.Page
    {
        private Image adBanner;
        private HyperLink adLink;

        protected void Page_Load(object sender, EventArgs e)
        {
            SetTimerInterval();
            // MUST go through the master page's ContentPlaceHolder tag to find the control in the child page!
            adBanner = Master.FindControl("BodyContentPlaceHolder").FindControl("adBanner") as Image;
            adLink = Master.FindControl("BodyContentPlaceHolder").FindControl("adLink") as HyperLink;
        }

        protected void RenewAdvertisement(object sender, EventArgs e)
        {
            AdLoader.SetAdvertisement(adBanner, adLink, Server.MapPath("./Advertisement/Images/ads_xml.xml"));
        }

        public void SetTimerInterval()
        {
            Timer t = Master.FindControl("BodyContentPlaceHolder").FindControl("ad_refresh_timer") as Timer;
            t.Interval = int.Parse(ConfigurationManager.AppSettings["ad_update_interval"]);
        }
    }
}
