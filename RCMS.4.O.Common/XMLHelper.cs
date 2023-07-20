using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;


namespace RCMS._4.O.Common
{
    public class XMLHelper
    {
        XMLHelper() { }

        public static string ToXML(object oObject)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }
        public static Object XMLToObject(string XMLString, Object oObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(oObject.GetType());
            oObject = oXmlSerializer.Deserialize(new StringReader(XMLString));
            return oObject;
        }

        public static string ToXML<T>(T oObject)
        {
            XmlDocument xmlDoc = new System.Xml.XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }
        public static T XMLToObject<T>(string XMLString, T oObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(oObject.GetType());
            oObject = (T)oXmlSerializer.Deserialize(new StringReader(XMLString));
            return oObject;
        }
    }
}