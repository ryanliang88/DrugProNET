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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            XmlParser xmlp = new XmlParser();
            XmlNode node = xmlp.Parse(Server.MapPath("./Advertisement/Images/ads_xml.xml"), "/Advertisements");
            List<XmlNode> nodes = xmlp.GetAllUnder(node);
            Ad ad = new Ad(RandomPicker.PickRandom(nodes, 0, nodes.Count));

            Image adBanner = (Image)FindControl("ad_banner");
            adBanner.ImageUrl = ad.GetImageURL();
            adBanner.AlternateText = ad.GetAlternateText();

            HyperLink adLink = (HyperLink)FindControl("ad_link");
            adLink.NavigateUrl = ad.GetNavigateURL();
        }
    }
}