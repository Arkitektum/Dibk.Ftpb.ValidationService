using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessage
    {

        public string id;
        public string xpath;
        public ValidationMessageTypeEnum messagetype;


        public string message;
        public string preCondition;
        public string checklistReference;
    }
}
