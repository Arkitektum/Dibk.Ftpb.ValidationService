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

        public PartstypeValidator(ICodeListService codeListService):base()
        {
            _codeListService = codeListService;
        }

        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule(ValidationRuleEnum.partstype_utfylt, xPathForEntity, "kodeverdi");
            AddValidationRule(ValidationRuleEnum.kodeverdi_ugyldig, xPathForEntity, "kodeverdi");
        }

        public ValidationResult Validate(ParttypeCodeValidationEntity partstype)
        {
            InitializeValidationRules(partstype.DataModelXpath);
            if (Helpers.ObjectIsNullOrEmpty(partstype.ModelData?.Kodeverdi))
            {
                AddMessageFromRule(ValidationRuleEnum.partstype_utfylt);
            }
            else
            {
                //TODO Sjekk hva "partstypeKodeverdi_ugyldig" er. Den er ikke initialisert....

                //if (!_codeListService.IsCodelistValid(FtbCodeListNames.Partstype, partstype.ModelData?.Kodeverdi))
                //{
                //    AddMessageFromRule("partstypeKodeverdi_ugyldig");
                //}
                //var gyldigPartsType = new List<string>() { "Foretak", "Offentlig myndighet", "Organisasjon", "Privatperson" };

                //if (!gyldigPartsType.Any(p => p.Equals(partstype.ModelData?.Kodeverdi)))
                //{
                //    AddMessageFromRule(ValidationRuleEnum.partstype_utfylt);
                //}
            }

            return ValidationResult;
        }
    }
}
