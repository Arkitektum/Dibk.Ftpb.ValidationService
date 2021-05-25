using Dibk.Ftpb.Validation.Application.DataSources;
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
            AddValidationRule("bygningsnummer_utfylt", xPathForEntity, "bygningsnummer");
            AddValidationRule("bolignummer_utfylt", xPathForEntity, "bolignummer");
            AddValidationRule("kommunenavn_utfylt", xPathForEntity, "kommunenavn");
            AddValidationRule("tillatte_postnr_i_kommune", xPathForEntity, "postnr");
        }

        public ValidationResult Validate(EiendomValidationEntity eiendom)
        {
            base.ResetValidationMessages();
            InitializeValidationRules(eiendom.DataModelXpath);
            

            ValidateEntityFields(eiendom);
                        
            var eiendomsAdresseValidationResult = _eiendomsAdresseValidator.Validate(eiendom.ModelData.Adresse);
            _validationResult.ValidationMessages.AddRange(eiendomsAdresseValidationResult.ValidationMessages);
            _validationResult.ValidationRules.AddRange(eiendomsAdresseValidationResult.ValidationRules);

            var matrikkelValidationResult = _matrikkelValidator.Validate(eiendom.ModelData.Matrikkel);
            _validationResult.ValidationMessages.AddRange(matrikkelValidationResult.ValidationMessages);
            _validationResult.ValidationRules.AddRange(matrikkelValidationResult.ValidationRules);

            ValidateDataRelations(eiendom);

            return _validationResult;
        }

        private void ValidateDataRelations(EiendomValidationEntity eiendom)
        {
            if (!TillattPostnrIKommune(eiendom.ModelData.Kommunenavn, eiendom.ModelData.Adresse.ModelData.Postnr))
            {
                AddMessageFromRule("tillatte_postnr_i_kommune", eiendom.DataModelXpath, new List<string>() { eiendom.ModelData.Adresse.ModelData.Postnr, eiendom.ModelData.Kommunenavn });
            }
        }

        protected void ValidateEntityFields(EiendomValidationEntity eiendom)
        {
            var xPath = eiendom.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(eiendom.ModelData?.Bygningsnummer))
                AddMessageFromRule("bygningsnummer_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendom.ModelData?.Bolignummer))
                AddMessageFromRule("bolignummer_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendom.ModelData?.Kommunenavn))
                AddMessageFromRule("kommunenavn_utfylt", xPath);
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
