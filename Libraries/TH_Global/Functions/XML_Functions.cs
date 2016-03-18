﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace TH_Global.Functions
{
    public static class XML_Functions
    {

        public static bool SetInnerText(XmlDocument doc, string xPath, string text)
        {
            bool result = false;

            if (doc != null)
            {
                XmlElement element = doc.DocumentElement;

                var node = element.SelectSingleNode(xPath);
                if (node == null) node = AddNode(doc, xPath);

                node.InnerText = text;
                result = true;
            }

            return result;
        }

        public static bool SetAttribute(XmlDocument doc, string xPath, string attributeName, string attributeValue)
        {
            bool result = false;

            if (doc != null)
            {
                XmlElement element = doc.DocumentElement;

                var node = element.SelectSingleNode(xPath);
                if (node == null) node = AddNode(doc, xPath);

                var attr = doc.CreateAttribute(attributeName);
                attr.Value = attributeValue;

                node.Attributes.SetNamedItem(attr);
                
                result = true;
            }

            return result;
        }

        public static string GetInnerText(XmlDocument doc, string xPath)
        {
            string result = null;

            if (doc != null)
            {
                XmlElement element = doc.DocumentElement;

                var node = element.SelectSingleNode(xPath);
                if (node != null)
                {
                    result = node.InnerText;
                }
            }

            return result;
        }

        public static XmlNode AddNode(XmlDocument doc, string xPath)
        {
            XmlNode result = null;

            if (doc != null)
            {
                XmlElement element = doc.DocumentElement;

                if (doc.DocumentElement != null)
                {
                    var node = makeXPath(doc, doc.DocumentElement as XmlNode, xPath);
                    if (node != null) result = node;
                }
            }

            return result;
        }

        static private XmlNode makeXPath(XmlDocument doc, XmlNode parent, string xPath)
        {
            // grab the next node name in the xpath; or return parent if empty
            string[] partsOfXPath = xPath.Trim('/').Split('/');
            string nextNodeInXPath = partsOfXPath.First();
            if (string.IsNullOrEmpty(nextNodeInXPath))
                return parent;

            // get or create the node from the name
            XmlNode node = parent.SelectSingleNode(nextNodeInXPath);
            if (node == null)
                node = parent.AppendChild(doc.CreateElement(nextNodeInXPath));

            // rejoin the remainder of the array as an xpath expression and recurse
            string rest = String.Join("/", partsOfXPath.Skip(1).ToArray());
            return makeXPath(doc, node, rest);
        }

        public static XmlDocument StringToXmlDocument(string str)
        {
            XmlDocument result = null;

            try
            {
                var xml = new XmlDocument();
                xml.LoadXml(str);

                result = xml;
            }
            catch (Exception ex) { Logger.Log("XML_Functions.StringToXmlDocument :: Exception :: " + ex.Message); }

            return result;
        }

        public static string XmlDocumentToString(XmlDocument doc)
        {
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                doc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }

    }
}
