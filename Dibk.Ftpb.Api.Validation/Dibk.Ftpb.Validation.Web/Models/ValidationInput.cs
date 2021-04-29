using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Web.Models
{


    //{
    //"FormData": "<?xml version=\"1.0\" encoding=\"utf-8\"?> <Nabovarsel  ... </xml>",
    //"SubForms": [
    //  {
    //    "Name": "Situasjonsplan",
    //  }, 
    //  {
    //    "Name": "TegningNyFasade",
    //  }
    // ],
    //"Attachments": [
    //  {
    //    "Name": "Situasjonsplan",
    //    "Filename": "minSitplan.pdf",
    //    "FileSize": 30000
    //  }, 
    //  {
    //    "Name": "TegningNyFasade",
    //    "Filename": "tegning293456.pdf",
    //    "FileSize": 500000
    //  }
    // ], 
    //}



    public class ValidationInput
    {

        /// <summary>
        /// The Form data. https://www.altinn.no/api/help
        /// </summary>
        [Required]
        public string FormData { get; set; }

        /// <summary>
        /// List of all subforms name used to validate required documentation. V2 enables more detailed validation of altinn rules for each attachement. Size, file extension and count of attachmenttypes.
        /// </summary>
        [Required]
        public SubFormInfo[] SubForms { get; set; }

        /// <summary>
        /// List of all attachment types and forms/subforms name used to validate required documentation. V2 enables more detailed validation of altinn rules for each attachement. Size, file extension and count of attachmenttypes.
        /// </summary>
        [Required]
        public AttachmentInfo[] Attachments { get; set; }




    }
}
