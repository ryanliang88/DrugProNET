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

namespace DrugProNET.Advertisement
{
    public class XmlParser
    {
        XmlDocument xml;

        public XmlNode Parse(string path, string baseSearchTag)
        {
            xml = new XmlDocument();
            xml.Load(path);
            XmlNode node = xml.DocumentElement.SelectSingleNode(baseSearchTag);
            return node;
        }

        public List<XmlNode> GetAllUnder(XmlNode n)
        {
            List<XmlNode> nodes = new List<XmlNode>();
            foreach (XmlNode node in n.ChildNodes)
            {
                nodes.Add(node);
            }

            return nodes;
        }
    }
}
