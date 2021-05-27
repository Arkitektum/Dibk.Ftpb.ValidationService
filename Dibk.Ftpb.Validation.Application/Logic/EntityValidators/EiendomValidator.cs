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

        public EiendomValidator(IMunicipalityValidator municipalityValidator) : base()
        {
            _municipalityValidator = municipalityValidator;
            _eiendomsAdresseValidator = new EiendomsAdresseValidator();
            _matrikkelValidator = new MatrikkelValidator();
        }

        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_bygningsnummer_utfylt, xPathForEntity, "bygningsnummer");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_bolignummer_utfylt, xPathForEntity, "bolignummer");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_kommunenavn_utfylt, xPathForEntity, "kommunenavn");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_tillatte_postnr_i_kommune, xPathForEntity, "postnr");
        }

        public ValidationResult Validate(EiendomValidationEntity eiendomValidationEntity)
        {
            base.ResetValidationMessages();
            InitializeValidationRules(eiendomValidationEntity.DataModelXpath);
            

            ValidateEntityFields(eiendomValidationEntity);
                        
            var eiendomsAdresseValidationResult = _eiendomsAdresseValidator.Validate(eiendomValidationEntity.ModelData.Adresse);
            _validationResult.ValidationMessages.AddRange(eiendomsAdresseValidationResult.ValidationMessages);
            _validationResult.ValidationRules.AddRange(eiendomsAdresseValidationResult.ValidationRules);

            var matrikkelValidationResult = _matrikkelValidator.Validate(eiendomValidationEntity.ModelData.Matrikkel);
            _validationResult.ValidationMessages.AddRange(matrikkelValidationResult.ValidationMessages);
            _validationResult.ValidationRules.AddRange(matrikkelValidationResult.ValidationRules);

            ValidateDataRelations(eiendomValidationEntity);

            return _validationResult;
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
