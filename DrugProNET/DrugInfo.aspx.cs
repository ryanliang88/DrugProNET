using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Xml;
using System.IO;
using System.Diagnostics;

using DrugProNET.Advertisement;

namespace DrugProNET
{
    public partial class DrugInfo : System.Web.UI.Page
    {
        private Image adBanner;
        private HyperLink adLink;

        protected void Page_Load(object sender, EventArgs e)
        {
            adBanner = (Image)FindControl("ad_banner");
            adLink = (HyperLink)FindControl("ad_link");
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            AdLoader.SetAdvertisement(adBanner, adLink, Server.MapPath("./Advertisement/Images/ads_xml.xml"));
        }

        protected void RenewAdvertisement(object sender, EventArgs e)
        {
            AdLoader.SetAdvertisement(adBanner, adLink, Server.MapPath("./Advertisement/Images/ads_xml.xml"));
        }
    }
}