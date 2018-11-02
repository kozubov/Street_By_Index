using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Server
{
    class XMLfile
    {
        private static XmlDocument doc;
        private static XmlTextWriter writer;
        private static XmlNodeList elemList2;


        static XMLfile()
        {
            doc = new XmlDocument();
            doc.Load("../../index.xml");
            writer = null;
            elemList2 = null;
        }
        public static List<string> Read_xmlfiel(string str)
        {
            List<string> list = new List<string>();
            elemList2 = doc.GetElementsByTagName("City");
            if (str == "index")
            {
                for (int i = 0; i < elemList2.Count; i++)
                {
                    list.Add(elemList2[i].FirstChild.Attributes["value"].Value);
                }
            }
            else
            {
                for (int i = 0; i < elemList2.Count; i++)
                {
                    if (elemList2[i].FirstChild.Attributes["value"].Value == str)
                    {
                        foreach (XmlNode VARIABLE in elemList2[i].ChildNodes)
                        {
                            if (VARIABLE.Attributes["br"] != null)
                            {
                                list.Add(VARIABLE.Attributes["br"].Value);
                            }
                        }
                    }
                }
            }

            return list;
        }
    }
}
