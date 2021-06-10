using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EiendomByggestedValidator : EntityValidatorBase, IEiendomByggestedValidator
    {
        private IEiendomsAdresseValidator _eiendomsAdresseValidator;
        private readonly IMatrikkelValidator _matrikkelValidator;
        private readonly IMunicipalityValidator _municipalityValidator;
        public override string ruleXmlElement { get { return "/eiendomByggested{0}"; } }

        ValidationResult IEiendomByggestedValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public EiendomByggestedValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, IEiendomsAdresseValidator eiendomsAdresseValidator, IMatrikkelValidator matrikkelValidator, IMunicipalityValidator municipalityValidator) 
            : base(entityValidatorOrchestrator)
        {
            _municipalityValidator = municipalityValidator;
            _eiendomsAdresseValidator = eiendomsAdresseValidator;
            _matrikkelValidator = matrikkelValidator;
        }

        protected override void InitializeValidationRules(string xPathToEntity)
        {
            AddValidationRule(ValidationRuleEnum.eiendom_utfylt, xPathToEntity);
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_bygningsnummer_utfylt, xPathToEntity, "bygningsnummer");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_bolignummer_utfylt, xPathToEntity, "bolignummer");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_kommunenavn_utfylt, xPathToEntity, "kommunenavn");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_tillatte_postnr_i_kommune, xPathToEntity, "postnr");
        }

        public ValidationResult Validate(IEnumerable<EiendomValidationEntity> eiendomValidationEntities)
        {
            if (Helpers.ObjectIsNullOrEmpty(eiendomValidationEntities) || eiendomValidationEntities.Count() == 0)
            {
                AddMessageFromRuleIfCollectionIsEmpty(ValidationRuleEnum.eiendom_utfylt);
            }
            else
            {

                foreach (var eiendomValidationEntity in eiendomValidationEntities)
                {

                    ValidateEntityFields(eiendomValidationEntity);

                    var eiendomsAdresseValidationResult = _eiendomsAdresseValidator.Validate(eiendomValidationEntity.ModelData.Adresse);
                    _validationResult.ValidationMessages.AddRange(eiendomsAdresseValidationResult.ValidationMessages);

                    var matrikkelValidationResult = _matrikkelValidator.Validate(eiendomValidationEntity.ModelData.Matrikkel);
                    _validationResult.ValidationMessages.AddRange(matrikkelValidationResult.ValidationMessages);

                    ValidateDataRelations(eiendomValidationEntity);
                }
            }

            return _validationResult;
        }

        private void ValidateDataRelations(EiendomValidationEntity eiendomValidationEntity)
        {
            if (!TillattPostnrIKommune(eiendomValidationEntity.ModelData.Kommunenavn, eiendomValidationEntity.ModelData.Adresse.ModelData?.Postnr))
            {
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_tillatte_postnr_i_kommune, eiendomValidationEntity.DataModelXpath, new List<string>() { eiendomValidationEntity.ModelData.Adresse.ModelData?.Postnr, eiendomValidationEntity.ModelData.Kommunenavn });
            }
        }

        private void ValidateEntityFields(EiendomValidationEntity eiendomValidationEntity)
        {
            var xPath = eiendomValidationEntity.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(eiendomValidationEntity.ModelData?.Bygningsnummer))
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_bygningsnummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomValidationEntity.ModelData?.Bolignummer))
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_bolignummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomValidationEntity.ModelData?.Kommunenavn))
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_kommunenavn_utfylt, xPath);
        }

        private bool TillattPostnrIKommune(string kommunenavn, string postnr)
        {
            var kommunenavnOgTillattePostnr = new List<(string kommune, List<string> postnrListe)>();
            kommunenavnOgTillattePostnr.Add(("Midt Telemark", new List<string>() { "3800", "3801", "3802", "3803", "3804" }));
            var funnetKommune = kommunenavnOgTillattePostnr.Where(x => x.kommune.Equals(kommunenavn)).FirstOrDefault();

            return funnetKommune.postnrListe.Contains(postnr);
        }
    }
}
