using System;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
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
        private readonly NaeringsgruppeValidator _naeringsgruppeValidator;
        private readonly BygningstypeValidator _bygningstypeValidator;
        private readonly TiltaksformaalValidator _tiltaksformaalValidator;

        private readonly ICodeListService _anleggstypeCodeListService;
        private readonly ICodeListService _naeringsgruppeCodeListService;
        private readonly ICodeListService _bygningstypeCodeListService;
        private readonly ICodeListService _tiltaksformaalCodeListService;

        public FormaaltypeValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, 
                                    EntityValidatorEnum parentValidator,
                                    AnleggstypeValidator anleggstypeValidator,
                                    ICodeListService anleggstypeCodeListService,
                                    NaeringsgruppeValidator naeringsgruppeValidator,
                                    ICodeListService naeringsgruppeCodeListService,
                                    BygningstypeValidator bygningstypeValidator,
                                    ICodeListService bygningstypeCodeListService,
                                    TiltaksformaalValidator tiltaksformaalValidator,
                                    ICodeListService tiltaksformaalCodeListService
                                    )
            : base(entityValidatorOrchestrator, parentValidator)
        {
            _anleggstypeValidator = anleggstypeValidator;
            _naeringsgruppeCodeListService = naeringsgruppeCodeListService;
            _bygningstypeCodeListService = bygningstypeCodeListService;
            _tiltaksformaalValidator = tiltaksformaalValidator;

            _anleggstypeCodeListService = anleggstypeCodeListService;
            _naeringsgruppeCodeListService = naeringsgruppeCodeListService;
            _bygningstypeCodeListService = bygningstypeCodeListService;
            _tiltaksformaalCodeListService = tiltaksformaalCodeListService;
        }
        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.anleggstype_utfylt, "anleggstype");
            AddValidationRule(ValidationRuleEnum.naeringsgruppe_utfylt, "naeringsgruppe");
            AddValidationRule(ValidationRuleEnum.bygningstype_utfylt, "bygningstype");
            AddValidationRule(ValidationRuleEnum.tiltaksformaal_utfylt, "tiltaksformaal");
            AddValidationRule(ValidationRuleEnum.beskrivPlanlagtFormaal_utfylt, "beskrivPlanlagtFormaal");
        }

        public ValidationResult Validate(FormaaltypeValidationEntity formaaltypeValEntity = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(formaaltypeValEntity.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.formaaltype_utfylt, formaaltypeValEntity.DataModelXpath);
            }
            else
            {
                ValidateEntityFields(formaaltypeValEntity);
            }

            return _validationResult;
        }
        private void ValidateEntityFields(FormaaltypeValidationEntity formaaltypeValEntity = null)
        {
            var xPath = formaaltypeValEntity.DataModelXpath;
            var formaaltype = formaaltypeValEntity.ModelData;

            if (Helpers.ObjectIsNullOrEmpty(formaaltype.Anleggstype))
            {
                AddMessageFromRule(ValidationRuleEnum.formaaltype_utfylt, xPath);
            }

        }
    }
}
