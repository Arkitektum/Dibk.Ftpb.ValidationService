using System;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
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

        
        
        //public KodelisteValidator(IList<GroupEnumerable.EntityValidatorNode> entityValidationGroup, EntityValidatorEnum parentValidator, ICodeListService codeListService)
        //    : base(entityValidationGroup, parentValidator.ToString())
        //{
        //    _codeListService = codeListService;
        //} 
        public KodelisteValidator(IList<EntityValidatorNode> entityValidationGroup, int nodeId, ICodeListService codeListService)
            : base(entityValidationGroup, nodeId)
        {
            _codeListService = codeListService;
        }        
        //public KodelisteValidator(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum parentValidator, ICodeListService codeListService)
        //    : base(formValidatorConfiguration, parentValidator)
        //{
        //    _codeListService = codeListService;
        //}        
        //public KodelisteValidator(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum parentValidator, EntityValidatorEnum grandParentValidator, ICodeListService codeListService) 
        //    : base(formValidatorConfiguration, parentValidator, grandParentValidator)
        //{
        //    _codeListService = codeListService;
        //}

        protected override void InitializeValidationRules()
        {
            AddValidationRule(KodeListValidationEnums.kodeverdi_utfylt, "kodeverdi");
            AddValidationRule(KodeListValidationEnums.kodeverdi_ugyldig, "kodeverdi");
            AddValidationRule(KodeListValidationEnums.Kodebeskrivelse_utfylt, "Kodebeskrivelse");
            AddValidationRule(KodeListValidationEnums.kodebeskrivelse_ugyldig, "Kodebeskrivelse");
        }

        public ValidationResult Validate(KodelisteValidationEntity kodeliste)
        {
            if (Helpers.ObjectIsNullOrEmpty(kodeliste.ModelData?.Kodeverdi))
            {
                AddMessageFromRule(KodeListValidationEnums.kodeverdi_utfylt);
            }
            else
            {
                //TODO Sjekk hva "partstypeKodeverdi_ugyldig" er. Den er ikke initialisert....
                if (!_codeListService.IsCodelistValid(FtbCodeListNames.Partstype, kodeliste.ModelData?.Kodeverdi))
                {
                    AddMessageFromRule(KodeListValidationEnums.kodeverdi_ugyldig);
                }
            }
            return ValidationResult;
        }
    }
}
