using RCMS._4.O.Entities.RCMSEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RCMS._4.O.Common
{
    public static class XMLHelperExtentions
    {
        static XMLHelperExtentions()
        { }

        public static string GenerateXML(this List<StudentEntity> dataList)
        {
            XDocument xDoc = new XDocument(new XElement("list"));
            string xmlName = "temp";
            if (dataList != null && dataList.Count > 0)
            {
                foreach (var item in dataList)
                {
                    xDoc.Descendants("list").First().Add(
                                    (
                                        new XElement(xmlName,
                                            new XAttribute("Id", item.Id),
                                            new XAttribute("Name", item.Name),
                                            new XAttribute("Address", item.Address)
                                    )
                                )
                       );
                }
            }
            return xDoc.ToString();
        }
    }
}
