using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DrugProNET.Advertisement
{
    public class AdvertiseablePage : System.Web.UI.Page
    {
        private Image adBanner;
        private HyperLink adLink;

        protected void Page_Load(object sender, EventArgs e)
        {
            adBanner = (Image)FindControl("ad_banner");
            adLink = (HyperLink)FindControl("ad_link");
        }

        protected void RenewAdvertisement(object sender, EventArgs e)
        {
            AdLoader.SetAdvertisement(adBanner, adLink, Server.MapPath("./Advertisement/Images/ads_xml.xml"));
        }
    }
}
