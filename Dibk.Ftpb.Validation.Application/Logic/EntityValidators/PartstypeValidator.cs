using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class PartstypeValidator : EntityValidatorBase
    {
        public PartstypeValidator(string parentXPath):base(parentXPath, "partstype")
        {
            InitializeValidationRules();
        }

        public sealed override void InitializeValidationRules()
        {
            AddValidationRule("partstype_utfylt", EntityXPath, "Kodeverdi");
            AddValidationRule("Kodeverdien_Ugyldig", EntityXPath, "Kodeverdi");
        }

        public ValidationResult Validate(string xPath, PartstypeCode partstype)
        {

            if (Helpers.ObjectIsNullOrEmpty(partstype?.Kodeverdi))
            {
                AddMessageFromRule("partstype_utfylt");

            }
            else
            {
                //if (!_codeListService.IsCodelistValid("Partstype", tiltakshaver.partstype?.kodeverdi))
                //{
                //    _validationResult.AddMessage("4845.1.6.2", new[] {tiltakshaver.partstype?.kodeverdi});
                //}
                var gyldigPartsType = new List<string>() { "Foretak", "Offentlig myndighet", "Organisasjon", "Privatperson" };

                if (!gyldigPartsType.Any(p => p.Equals(partstype.Kodeverdi)))
                {
                    AddMessageFromRule("partstype_ugyldig"); //error
                }
                else
                {
                    //if(isLableValid)
                    //    AddMessageFromRule("partstype_lable_ugyldig"); // Warning
                }
            }

            return ValidationResult;
        }
    }
}
