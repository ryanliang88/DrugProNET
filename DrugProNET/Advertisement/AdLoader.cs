using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

namespace DrugProNET.Advertisement
{
    public class AdLoader
    {
        /// <summary>
        /// Author: Andy Tang
        /// </summary>
        public static void SetAdvertisement(Image adBanner, HyperLink adLink, string nodePath)
        {
            XmlParser xmlp = new XmlParser();
            XmlNode node = xmlp.Parse(nodePath, "/Advertisements");
            List<XmlNode> nodes = xmlp.GetAllUnder(node);
            Ad ad = new Ad(RandomPicker.PickRandom(nodes, 0, nodes.Count));

            string url = ad.GetImageURL();
            url = url.Replace("~", "");
            adBanner.ImageUrl = "Advertisement" + url;

            adBanner.AlternateText = ad.GetAlternateText();

            adLink.NavigateUrl = ad.GetNavigateURL();
        }
    }
}