using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Web.Models
{
    public class SubFormInfo
    {
        /// <summary>
        /// Name is attachmentType for attachment and form name for form and subforms
        /// </summary>
        [Required(ErrorMessage = "Vedlegget må ha et navn")]
        public string FormName { get; set; }

        /// <summary>
        /// filename with extension
        /// </summary>
        public string SubFormData { get; set; }

    }
}
