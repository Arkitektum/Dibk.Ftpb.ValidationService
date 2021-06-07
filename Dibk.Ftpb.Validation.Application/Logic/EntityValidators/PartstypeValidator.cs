﻿using System;
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
    public class PartstypeValidator : EntityValidatorBase, IPartstypeValidator
    {
        private readonly ICodeListService _codeListService;
        public override string ruleXmlElement { get { return "/partstype"; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        public PartstypeValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, string parent, ICodeListService codeListService) 
            : base(entityValidatorOrchestrator, parent)
        {
            _codeListService = codeListService;
            InitializeValidationRules(EntityXPath);
        }

        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule(ValidationRuleEnum.partstype_utfylt, xPathForEntity, "kodeverdi");
            AddValidationRule(ValidationRuleEnum.kodeverdi_ugyldig, xPathForEntity, "kodeverdi");
        }

        public ValidationResult Validate(ParttypeCodeValidationEntity partstype)
        {
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
