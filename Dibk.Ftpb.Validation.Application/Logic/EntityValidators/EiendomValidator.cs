using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EiendomValidator : EntityValidatorBase
    {
        private EiendomsAdresseValidator _eiendomsAdresseValidator;
        private MatrikkelValidator _matrikkelValidator;
        private readonly IMunicipalityValidator _municipalityValidator;

        public EiendomValidator(string xPath, IMunicipalityValidator municipalityValidator) : base()
        {
            _municipalityValidator = municipalityValidator;
            InitializeValidationRules(xPath);

            _eiendomsAdresseValidator = new EiendomsAdresseValidator($"{xPath}/adresse");
            this.ValidationResult.ValidationRules.AddRange(_eiendomsAdresseValidator.ValidationResult.ValidationRules);

            _matrikkelValidator = new MatrikkelValidator($"{xPath}/eiendomsidentifikasjon");
            this.ValidationResult.ValidationRules.AddRange(_matrikkelValidator.ValidationResult.ValidationRules);
        }

        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule(ValidationRuleEnum.eiendom_utfylt, xPathForEntity);
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_bygningsnummer_utfylt, xPathForEntity, "bygningsnummer");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_bolignummer_utfylt, xPathForEntity, "bolignummer");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_kommunenavn_utfylt, xPathForEntity, "kommunenavn");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_tillatte_postnr_i_kommune, xPathForEntity, "postnr");
        }

        public ValidationResult Validate(IEnumerable<EiendomValidationEntity> eiendomValidationEntities)
        {
            //base.ResetValidationMessages();

            if (Helpers.ObjectIsNullOrEmpty(eiendomValidationEntities) || eiendomValidationEntities.Count() == 0)
            {
                AddMessageFromRule(ValidationRuleEnum.eiendom_utfylt);
            }
            else
            {

                foreach (var eiendomValidationEntity in eiendomValidationEntities)
                {

                    ValidateEntityFields(eiendomValidationEntity);

                    var eiendomsAdresseValidationResult = _eiendomsAdresseValidator.Validate(eiendomValidationEntity.ModelData.Adresse);
                    ValidationResult.ValidationMessages.AddRange(eiendomsAdresseValidationResult.ValidationMessages);

                    var matrikkelValidationResult = _matrikkelValidator.Validate(eiendomValidationEntity.ModelData.Matrikkel);
                    ValidationResult.ValidationMessages.AddRange(matrikkelValidationResult.ValidationMessages);

                    ValidateDataRelations(eiendomValidationEntity);
                }
            }

            return ValidationResult;
        }

        private void ValidateDataRelations(EiendomValidationEntity eiendomValidationEntity)
        {
            if (!TillattPostnrIKommune(eiendomValidationEntity.ModelData.Kommunenavn, eiendomValidationEntity.ModelData.Adresse.ModelData?.Postnr))
            {
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_tillatte_postnr_i_kommune, eiendomValidationEntity.DataModelXpath, new List<string>() { eiendomValidationEntity.ModelData.Adresse.ModelData?.Postnr, eiendomValidationEntity.ModelData.Kommunenavn });
            }
        }

        protected void ValidateEntityFields(EiendomValidationEntity eiendomValidationEntity)
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
