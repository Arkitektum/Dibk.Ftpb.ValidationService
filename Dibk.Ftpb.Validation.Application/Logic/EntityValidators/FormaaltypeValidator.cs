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
    public class FormaaltypeValidator : EntityValidatorBase, IFormaaltypeValidator
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }
        private readonly AnleggstypeValidator _anleggstypeValidator;
        private readonly NaeringsgruppeValidator _naeringsgruppeValidator;
        //private readonly BygningstypeValidator _bygningstypeValidator;
        //private readonly TiltaksformaalValidator _tiltaksformaalValidator;

        private readonly ICodeListService _anleggstypeCodeListService;
        private readonly ICodeListService _naeringsgruppeCodeListService;
        private readonly ICodeListService _bygningstypeCodeListService;
        private readonly ICodeListService _tiltaksformaalCodeListService;

        protected IKodelisteValidator _anleggstypeCodeListServiceNew;


        public FormaaltypeValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, 
            AnleggstypeValidator anleggstypeValidator, NaeringsgruppeValidator naeringsgruppeValidator
            //BygningstypeValidator bygningstypeValidator, ICodeListService bygningstypeCodeListService,
            //TiltaksformaalValidator tiltaksformaalValidator, ICodeListService tiltaksformaalCodeListService
        )
            : base(entityValidatorTree, nodeId)
        {
            _anleggstypeValidator = anleggstypeValidator;
            _naeringsgruppeValidator = naeringsgruppeValidator;
            //_bygningstypeValidator = bygningstypeValidator;
            //_tiltaksformaalValidator = tiltaksformaalValidator;


            //_naeringsgruppeCodeListService = naeringsgruppeCodeListService;
            //_bygningstypeCodeListService = bygningstypeCodeListService;
            //_tiltaksformaalCodeListService = tiltaksformaalCodeListService;
        }
        //public FormaaltypeValidator(FormValidatorConfiguration formValidatorConfiguration,
        //                            EntityValidatorEnum parentValidator,
        //                            AnleggstypeValidator anleggstypeValidator, ICodeListService anleggstypeCodeListService,
        //                            NaeringsgruppeValidator naeringsgruppeValidator, ICodeListService naeringsgruppeCodeListService,
        //                            BygningstypeValidator bygningstypeValidator, ICodeListService bygningstypeCodeListService,
        //                            TiltaksformaalValidator tiltaksformaalValidator, ICodeListService tiltaksformaalCodeListService
        //                            )
        //    : base(formValidatorConfiguration, parentValidator)
        //{
        //    _anleggstypeValidator = anleggstypeValidator;
        //    //_naeringsgruppeValidator = naeringsgruppeValidator;
        //    //_bygningstypeValidator = bygningstypeValidator;
        //    //_tiltaksformaalValidator = tiltaksformaalValidator;

        //    _anleggstypeCodeListService = anleggstypeCodeListService;
        //    _naeringsgruppeCodeListService = naeringsgruppeCodeListService;
        //    _bygningstypeCodeListService = bygningstypeCodeListService;
        //    _tiltaksformaalCodeListService = tiltaksformaalCodeListService;
        //}


        protected override void InitializeValidationRules()
        {
            AddValidationRule(FormaaltypeValidationEnums.utfylt, null);
            AddValidationRule(FormaaltypeValidationEnums.beskrivPlanlagtFormaal_utfylt, "beskrivPlanlagtFormaal");
        }

        public ValidationResult Validate(FormaaltypeValidationEntity formaaltypeValEntity = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(formaaltypeValEntity?.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.beskrivelseAvTiltak_formaaltype_utfylt, formaaltypeValEntity?.DataModelXpath);
            }
            else
            {
                var anleggstypeValidationResult = _anleggstypeValidator.Validate(formaaltypeValEntity?.ModelData?.Anleggstype);
                UpdateValidationResultWithSubValidations(anleggstypeValidationResult);

                ValidateEntityFields(formaaltypeValEntity);
            }

            return _validationResult;
        }
        private void ValidateEntityFields(FormaaltypeValidationEntity formaaltypeValEntity)
        {
            var xPath = formaaltypeValEntity.DataModelXpath;
            var formaaltype = formaaltypeValEntity.ModelData;

            if (Helpers.ObjectIsNullOrEmpty(formaaltype.Anleggstype))
            {
                AddMessageFromRule(ValidationRuleEnum.beskrivelseAvTiltak_formaaltype_utfylt, xPath);
            }
            else
            {
                
            }

        }
    }
}
