using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EiendomValidator : EntityValidatorBase
    {
        private EiendomsAdresseValidator _eiendomsAdresseValidator;
        private MatrikkelValidator _matrikkelValidator;
        public EiendomValidator() : base()
        {
            _eiendomsAdresseValidator = new EiendomsAdresseValidator();
            _matrikkelValidator = new MatrikkelValidator();
        }
        public List<ValidationRule> Validate(string context, Eiendom eiendom)
        {
            string newContext = $"{context}/eiendomByggested";
            InitializeValidationRules(newContext);
            ValidateEntityFields(eiendom);

            var eiendomsAdresseValidationRules = _eiendomsAdresseValidator.Validate(newContext, eiendom.Adresse);
            ValidationRules.AddRange(eiendomsAdresseValidationRules);

            var matrikkelValidationRules = _matrikkelValidator.Validate(newContext, eiendom.Matrikkel);
            ValidationRules.AddRange(matrikkelValidationRules);
            
            ValidateDataRelations(eiendom);

            return ValidationRules;
        }

        private void ValidateDataRelations(Eiendom eiendom)
        {
            ValidationRules.Where(crit => crit.Id.Equals("tillatte_postnr_i_kommune")).FirstOrDefault().ValidationResult
                = !TillattPostnrIKommune(eiendom.Kommunenavn, eiendom.Adresse.Postnr) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;
            ValidationRules.Where(crit => crit.Id.Equals("tillatte_postnr_i_kommune")).FirstOrDefault().MessageParameters
                = new List<string>() { eiendom.Adresse.Postnr, eiendom.Kommunenavn  };
        }


        public override void InitializeValidationRules(string context)
        {
            ValidationRules.Add(new ValidationRule() { Id = "bygningsnummer_utfylt", Xpath = $"{context}/Bygningsnummer", ValidationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { Id = "bolignummer_utfylt", Xpath = $"{context}/Bolignummer", ValidationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { Id = "kommunenavn_utfylt", Xpath = $"{context}/Kommunenavn", ValidationResult = ValidationResultEnum.Unused });

            ValidationRules.Add(new ValidationRule() { Id = "tillatte_postnr_i_kommune", Xpath = $"{context}", ValidationResult = ValidationResultEnum.Unused });
        }

        public override void ValidateEntityFields(object entityData)
        {
            Eiendom eiendom = (Eiendom)entityData;
            ValidationRules.Where(crit => crit.Id.Equals("bygningsnummer_utfylt")).FirstOrDefault().ValidationResult
                = (string.IsNullOrEmpty(eiendom.Bygningsnummer)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.Id.Equals("bolignummer_utfylt")).FirstOrDefault().ValidationResult
                = (string.IsNullOrEmpty(eiendom.Bolignummer)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.Id.Equals("kommunenavn_utfylt")).FirstOrDefault().ValidationResult
                = (string.IsNullOrEmpty(eiendom.Kommunenavn)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;
        }

        private bool TillattPostnrIKommune(string kommunenavn, string postnr)
        {
            var kommunenavnOgTillattePostnr = new List<(string kommune, List<string> postnrListe)>();
            kommunenavnOgTillattePostnr.Add(("Midt Telemark" , new List<string>() { "3800", "3801", "3802", "3803", "3804" }));
            var funnetKommune = kommunenavnOgTillattePostnr.Where(x => x.kommune.Equals(kommunenavn)).FirstOrDefault();
            
            return funnetKommune.postnrListe.Contains(postnr);
        }
    }
}
