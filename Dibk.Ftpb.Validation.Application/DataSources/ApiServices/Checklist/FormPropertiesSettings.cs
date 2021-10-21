using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist
{
    public class FormPropertiesSettings
    {
        public List<FormProperties> FormProperties { get; set; }
    }
    public class FormProperties
    {
        public string DataFormatId { get; set; }
        public string DataFormatVersion { get; set; }
        public string ServiceAuthority { get; set; }
        public string ProcessCategory { get; set; }
    }
}
