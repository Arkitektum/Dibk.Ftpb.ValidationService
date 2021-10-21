using System;

namespace Dibk.Ftpb.Validation.Application.DataSources.Models
{
    public class MunicipalityViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string OrganizationNumber { get; set; }
        public string PlanningDepartmentSpecificOrganizationNumber { get; set; }
        public string NewMunicipalityCode { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? ValidFrom { get; set; }
    }
}
