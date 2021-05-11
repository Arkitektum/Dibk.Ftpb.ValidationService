using System.ComponentModel.DataAnnotations;

namespace Dibk.Ftpb.Validation.Application.Models.Web
{
    public class SubFormInfo
    {
        /// <summary>
        /// Name is attachmentType for attachment and form name for form and subforms
        /// </summary>
        [Required(ErrorMessage = "Underskjema må ha ett navn")]
        public string FormName { get; set; }

        /// <summary>
        /// filename with extension
        /// </summary>
        public string SubFormData { get; set; }

    }
}
