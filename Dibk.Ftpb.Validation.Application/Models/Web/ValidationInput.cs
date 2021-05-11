using System.ComponentModel.DataAnnotations;

namespace Dibk.Ftpb.Validation.Application.Models.Web
{
    public class ValidationInput
    {
        /// <summary>
        /// The Form data. https://www.altinn.no/api/help
        /// </summary>
        [Required(ErrorMessage = "FormData må inneholde XML skjema data")]
        public string FormData { get; set; }

        /// <summary>
        /// List of all subforms name used to validate required documentation. V2 enables more detailed validation of altinn rules for each attachement. Size, file extension and count of attachmenttypes.
        /// </summary>
        public SubFormInfo[] SubForms { get; set; }

        /// <summary>
        /// List of all attachment types and forms/subforms name used to validate required documentation. V2 enables more detailed validation of altinn rules for each attachement. Size, file extension and count of attachmenttypes.
        /// </summary>
        public AttachmentInfo[] Attachments { get; set; }
    }
}
