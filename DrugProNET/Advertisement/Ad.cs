using System;
using System.Web;
using System.Xml;

namespace DrugProNET.Advertisement
{
    public class Ad
    {
        private string imageURL;
        private string navigateURL;
        private string alternateText;

        public Ad(XmlNode n)
        {
            foreach (XmlNode node in n)
            {
                switch (node.LocalName)
                {
                    case "ImageUrl":
                        imageURL = node.InnerText;
                        break;
                    case "NavigatorUrl":                   
                    case "NavigateUrl":
                        navigateURL = node.InnerText;
                        break;
                    case "AlternateText":
                        alternateText = node.InnerText;
                        break;
                }
            }
        }

        public string GetImageURL()
        {
            return imageURL;
        }

        public string GetNavigateURL()
        {
            return navigateURL;
        }

        public string GetAlternateText()
        {
            return alternateText;
        }
    }
}
