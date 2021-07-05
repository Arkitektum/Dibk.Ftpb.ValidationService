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
        public override string ruleXmlElement { get { return "bruk"; } set { ruleXmlElement = value; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }
        private readonly AnleggstypeValidator _anleggstypeValidator;
        //private readonly NaeringsgruppeValidator _naeringsgruppeValidator;
        //private readonly BygningstypeValidator _bygningstypeValidator;
        //private readonly TiltaksformaalValidator _tiltaksformaalValidator;

        private readonly ICodeListService _anleggstypeCodeListService;
        private readonly ICodeListService _naeringsgruppeCodeListService;
        private readonly ICodeListService _bygningstypeCodeListService;
        private readonly ICodeListService _tiltaksformaalCodeListService;

        protected IKodelisteValidator _anleggstypeCodeListServiceNew;


        public FormaaltypeValidator(IList<EntityValidatorNode> entityValidationGroup,
            int nodeId,
            AnleggstypeValidator anleggstypeValidator, ICodeListService anleggstypeCodeListService
            //NaeringsgruppeValidator naeringsgruppeValidator, ICodeListService naeringsgruppeCodeListService,
            //BygningstypeValidator bygningstypeValidator, ICodeListService bygningstypeCodeListService,
            //TiltaksformaalValidator tiltaksformaalValidator, ICodeListService tiltaksformaalCodeListService
        )
            : base(entityValidationGroup, nodeId)
        {
            _anleggstypeValidator = anleggstypeValidator;
            //_naeringsgruppeValidator = naeringsgruppeValidator;
            //_bygningstypeValidator = bygningstypeValidator;
            //_tiltaksformaalValidator = tiltaksformaalValidator;

            _anleggstypeCodeListService = anleggstypeCodeListService;
            //_naeringsgruppeCodeListService = naeringsgruppeCodeListService;
            //_bygningstypeCodeListService = bygningstypeCodeListService;
            //_tiltaksformaalCodeListService = tiltaksformaalCodeListService;
        }
        public FormaaltypeValidator(FormValidatorConfiguration formValidatorConfiguration,
                                    EntityValidatorEnum parentValidator,
                                    AnleggstypeValidator anleggstypeValidator, ICodeListService anleggstypeCodeListService,
                                    NaeringsgruppeValidator naeringsgruppeValidator, ICodeListService naeringsgruppeCodeListService,
                                    BygningstypeValidator bygningstypeValidator, ICodeListService bygningstypeCodeListService,
                                    TiltaksformaalValidator tiltaksformaalValidator, ICodeListService tiltaksformaalCodeListService
                                    )
            : base(formValidatorConfiguration, parentValidator)
        {
            _anleggstypeValidator = anleggstypeValidator;
            //_naeringsgruppeValidator = naeringsgruppeValidator;
            //_bygningstypeValidator = bygningstypeValidator;
            //_tiltaksformaalValidator = tiltaksformaalValidator;

            _anleggstypeCodeListService = anleggstypeCodeListService;
            _naeringsgruppeCodeListService = naeringsgruppeCodeListService;
            _bygningstypeCodeListService = bygningstypeCodeListService;
            _tiltaksformaalCodeListService = tiltaksformaalCodeListService;
        }
        protected override void InitializeValidationRules()
        {

            AddValidationRule(FormaaltypeValidationEnums.beskrivelseAvTiltak_anleggstype_utfylt, "anleggstype");


            AddValidationRule(ValidationRuleEnum.beskrivelseAvTiltak_anleggstype_kode_utfylt, "anleggstype");
            AddValidationRule(ValidationRuleEnum.beskrivelseAvTiltak_naeringsgruppe_kode_utfylt, "naeringsgruppe");
            AddValidationRule(ValidationRuleEnum.beskrivelseAvTiltak_bygningstype_kode_utfylt, "bygningstype");
            AddValidationRule(ValidationRuleEnum.beskrivelseAvTiltak_tiltakformaal_kode_utfylt, "tiltaksformaal");
            AddValidationRule(ValidationRuleEnum.beskrivelseAvTiltak_beskrivPlanlagtFormaal_utfylt, "beskrivPlanlagtFormaal");
        }

        public ValidationResult Validate(FormaaltypeValidationEntity formaaltypeValEntity = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(formaaltypeValEntity.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.beskrivelseAvTiltak_formaaltype_utfylt, formaaltypeValEntity.DataModelXpath);
            }
            else
            {
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
                var anleggstypeValidationResult = _anleggstypeValidator.Validate(formaaltype.Anleggstype);
                UpdateValidationResultWithSubValidations(anleggstypeValidationResult);
            }

        }
    }
}
