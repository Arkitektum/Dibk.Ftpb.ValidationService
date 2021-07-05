using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class BeskrivelseAvTiltakValidator : EntityValidatorBase, IBeskrivelseAvTiltakValidator
    {
        //public override string ruleXmlElement { get { return "beskrivelseAvTiltak"; } set { ruleXmlElement = value; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        private readonly FormaaltypeValidator _formaaltypeValidator;
        //private readonly TiltakstypeValidator _tiltakstypeValidator;
        private readonly AnleggstypeValidator _anleggstypeValidator;
        private readonly ICodeListService _anleggstypeCodeListService;

        public BeskrivelseAvTiltakValidator(IList<EntityValidatorNode> entityValidationGroup, FormaaltypeValidator formaaltypeValidator,  AnleggstypeValidator anleggstypeValidator, ICodeListService anleggstypeCodeListService)
            : base(entityValidationGroup, null)
        {
            _formaaltypeValidator = formaaltypeValidator;
            //_tiltakstypeValidator = tiltakstypeValidator;
            _anleggstypeValidator = anleggstypeValidator;
            _anleggstypeCodeListService = anleggstypeCodeListService;
        }
        //public BeskrivelseAvTiltakValidator(FormValidatorConfiguration formValidatorConfiguration, FormaaltypeValidator formaaltypeValidator, TiltakstypeValidator tiltakstypeValidator)
        //    : base(formValidatorConfiguration)
        //{
        //    _formaaltypeValidator = formaaltypeValidator;
        //    //_tiltakstypeValidator = tiltakstypeValidator;
        //}
        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.beskrivelseAvTiltak_utfylt);
        }

        public ValidationResult Validate(BeskrivelseAvTiltakValidationEntity beskrivelseAvTiltakValidationEntity = null)
        {
            var xpath = beskrivelseAvTiltakValidationEntity.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(beskrivelseAvTiltakValidationEntity) || Helpers.ObjectIsNullOrEmpty(beskrivelseAvTiltakValidationEntity.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.beskrivelseAvTiltak_utfylt, xpath);
            }
            else
            {
                var formaaltypeValidationEntity = beskrivelseAvTiltakValidationEntity.ModelData.Formaaltype;

                if (Helpers.ObjectIsNullOrEmpty(formaaltypeValidationEntity) || Helpers.ObjectIsNullOrEmpty(formaaltypeValidationEntity.ModelData))
                {
                    AddMessageFromRule(ValidationRuleEnum.beskrivelseAvTiltak_formaaltype_utfylt, xpath);
                }
                else
                {
                    var formaaltypeValidationResult = _formaaltypeValidator.Validate(formaaltypeValidationEntity);
                    UpdateValidationResultWithSubValidations(formaaltypeValidationResult);
                }

                //var tiltakstypeValidationEntity = beskrivelseAvTiltakValidationEntity.ModelData.Tiltakstype;
                if (Helpers.ObjectIsNullOrEmpty(beskrivelseAvTiltakValidationEntity.ModelData.Tiltakstype) || beskrivelseAvTiltakValidationEntity.ModelData.Tiltakstype.Count() == 0)
                {
                    AddMessageFromRule(ValidationRuleEnum.beskrivelseAvTiltak_tiltakstype_finnes_ikke, xpath);
                }
                else
                {
                    foreach (var tiltakstypeValidationEntity in beskrivelseAvTiltakValidationEntity.ModelData.Tiltakstype)
                    {
                        //var tiltakstypeValidationResult = _tiltakstypeValidator.Validate(tiltakstypeValidationEntity);
                        //UpdateValidationResultWithSubValidations(tiltakstypeValidationResult);
                    } 
                }
            }
            return ValidationResult;
        }
    }
}
