using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist
{
    public class FormPropertiesSettings
    {
        public List<FormProperties> FormProperties { get; set; }
    }
    public class FormProperties
    {
        public string DataFormatVersion { get; set; }
        public string ServiceAuthority { get; set; }
        public string Soknadstype { get; set; }
    }
}
