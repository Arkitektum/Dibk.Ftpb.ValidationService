using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public abstract class KodelisteValidator : EntityValidatorBase, IKodelisteValidator
    {
        protected ICodeListService _codeListService;

        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        public KodelisteValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, EntityValidatorEnum parentValidator, ICodeListService codeListService) 
            : base(entityValidatorOrchestrator, parentValidator)
        {
            _codeListService = codeListService;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.kodeliste_utfylt, "kodeverdi");
            AddValidationRule(ValidationRuleEnum.kodeverdi_ugyldig, "kodeverdi");
        }

        public ValidationResult Validate(KodelisteValidationEntity kodeliste)
        {
            var xpath = kodeliste.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(kodeliste.ModelData?.Kodeverdi))
            {
                AddMessageFromRule(ValidationRuleEnum.kodeliste_utfylt, xpath);
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
