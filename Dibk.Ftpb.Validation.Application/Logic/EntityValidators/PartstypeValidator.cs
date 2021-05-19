using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class PartstypeValidator : EntityValidatorBase
    {
        private readonly ICodeListService _codeListService;

        public PartstypeValidator(string parentXPath,ICodeListService codeListService):base(parentXPath, "partstype")
        {
            _codeListService = codeListService;
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
                if (!_codeListService.IsCodelistValid(FtbCodeListNames.Partstype, partstype?.Kodeverdi))
                {
                    AddMessageFromRule("partstypeKodeverdi_ugyldig");
                }
                var gyldigPartsType = new List<string>() { "Foretak", "Offentlig myndighet", "Organisasjon", "Privatperson" };

                if (!gyldigPartsType.Any(p => p.Equals(partstype?.Kodeverdi)))
                {
                    AddMessageFromRule("partstype_ugyldig");
                }
            }

            return ValidationResult;
        }
    }
}
