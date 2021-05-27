using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public abstract class ModelToValidationEntityMapper<T, U>
    {
        public abstract U Map(T mapFrom, string parentElementXpath = null);

        //protected static string GetXmlElementName(object xmlElement)
        //{
        //    if (xmlElement == null) return null;

        //    Type type = xmlElement.GetType();
        //    var a = type.GetCustomAttributes(typeof(System.Xml.Serialization.XmlRootAttribute), false).FirstOrDefault() as System.Xml.Serialization.XmlRootAttribute;

        //    if (a != null)
        //        return a.ElementName;

        //    return null;
        //}
    }
}
