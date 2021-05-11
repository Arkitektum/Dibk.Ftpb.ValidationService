using System.ComponentModel.DataAnnotations;

namespace Dibk.Ftpb.Validation.Application.Models.Web
{
    public class AttachmentInfo
    {
        /// <summary>
        /// Name is attachmentType for attachment and form name for form and subforms
        /// </summary>
        [Required(ErrorMessage = "Vedlegget må ha et navn")]
        public string AttachmentTypeName { get; set; }

        /// <summary>
        /// filename with extension
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Filstørrelse i byte
        /// </summary>
        public int FileSize { get; set; }
    }
}
