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
        private string _xPath;
        private const string _entityName = "tiltakshaver";

        public PartstypeValidator(string parentXPath)
        {
            _xPath = $"{parentXPath}/{_entityName}";
            InitializeValidationRules();
        }

        public override void InitializeValidationRules(string parentXPath = null)
        {
            AddValidationRule("Kodeverdien_utfylt", _xPath, "Kodeverdi");
            AddValidationRule("Kodeverdien_Ugyldig", _xPath, "Kodeverdi");
        }

        public ValidationResult Validate(string xPath, PartstypeCode partstype)
        {

            if (Helpers.ObjectIsNullOrEmpty(partstype?.Kodeverdi))
            {
                AddMessageFromRule("Kodeverdien_utfylt");

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
                    AddMessageFromRule("Kodeverdien_ugyldig");
                }
            }

            return ValidationResult;
        }
    }
}
