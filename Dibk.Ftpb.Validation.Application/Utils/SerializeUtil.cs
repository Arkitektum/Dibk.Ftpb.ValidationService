using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Utils
{
    public class SerializeUtil
    {
        public static T DeserializeFromString<T>(string objectData)
        {
            return (T)DeserializeFromString(objectData, typeof(T));
        }

        private static object DeserializeFromString(string objectData, Type type)
        {
            object result;

            TextReader reader = null;
            try
            {
                var xmlString = RemoveNamespaces(objectData);

                using var stringReader = new StringReader(xmlString);
                var serializer = new XmlSerializer(type);

                result = serializer.Deserialize(stringReader);
            }
            finally
            {
                reader?.Close();
            }

            return result;
        }
        public static string Serialize(object form)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(form.GetType());
            var stringWriter = new Utf8StringWriter();
            serializer.Serialize(stringWriter, form);
            return stringWriter.ToString();
        }


        //** Remove name spaceses 
        private static string RemoveAllNamespaces(string xmlDocument)
        {
            XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

            return xmlDocumentWithoutNs.ToString();
        }

        //Core recursion function
        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }
            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
        }

        public static string RemoveNamespaces(string xmlString)
        {
            try
            {
                XDocument document = XDocument.Parse(RemoveAllNamespaces(xmlString));
                return document.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
