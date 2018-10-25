using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace FileSynchroniser
{
    static class OptionsData
    {
        public static string OriginalDirectory
        {
            get { return GetInnerTextOfTeg("originalDir"); }
            set { SetInnerTextOfTeg(value, "originalDir"); }
        }

        public static string TargetDirectory
        {
            get { return GetInnerTextOfTeg("targetDir"); }
            set { SetInnerTextOfTeg(value, "targetDir"); }
        }

        /// <summary>
        /// Initialize empty XML file with name: Options.xml, if it does not exist
        /// </summary>
        static void InitOptionsXml()
        {
            if (!File.Exists("Options.xml"))
            {
                XmlDocument xmlDoc = new XmlDocument();

                XmlDeclaration xmlDec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmlDec);

                XmlElement xmlElemOptionsList = xmlDoc.CreateElement("options");

                XmlElement xmlElemOriginalDir = xmlDoc.CreateElement("originalDir");
                xmlElemOriginalDir.SetAttribute("id", "originalDir");
                xmlElemOriginalDir.InnerText = "empty";

                XmlElement xmlElemTargetDir = xmlDoc.CreateElement("targetDir");
                xmlElemTargetDir.SetAttribute("id", "targetDir");
                xmlElemTargetDir.InnerText = "empty";

                xmlElemOptionsList.AppendChild(xmlElemOriginalDir);
                xmlElemOptionsList.AppendChild(xmlElemTargetDir);

                xmlDoc.AppendChild(xmlElemOptionsList);

                xmlDoc.Save("Options.xml");
            }
        }

        /// <summary>
        /// Set inner text to teg with given Id on Options.xml.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="tegName">ID of teg for writing</param>
        static void SetInnerTextOfTeg(string text, string tegName)
        {
            InitOptionsXml();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load("Options.xml");

            XmlNode xmlElemDir = xmlDoc.SelectSingleNode("options/" + tegName);
            xmlElemDir.InnerText = text;
            xmlDoc.Save("Options.xml");
        }

        /// <summary>
        /// get inner text from teg with given Id on Options.xml.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="tegName">Name of teg for writing</param>
        static string GetInnerTextOfTeg(string tegName)
        {
            InitOptionsXml();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load("Options.xml");

            XmlNode xmlElemDir = xmlDoc.SelectSingleNode("options/" + tegName);

            return xmlElemDir.InnerText;
        }
    }
}
