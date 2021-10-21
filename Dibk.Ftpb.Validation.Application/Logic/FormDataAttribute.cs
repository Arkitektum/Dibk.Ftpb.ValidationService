using System;

namespace Dibk.Ftpb.Validation.Application.Logic
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FormDataAttribute : Attribute
    {
        //public string DataFormatId { get; set; }
        public string DataFormatVersion { get; set; }

        //public string ServiceCode { get; set; }
    }
}
