using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

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

        public ValidationResult Validate(string parentContext, List<Eiendom> eiendommer)
        {
            string xPath = string.Concat(parentContext, "/eiendomByggested{0}");
            InitializeValidationRules(xPath);

            ValidateEntityFields(eiendommer);
            foreach (var eiendom in eiendommer)
            {
                var eiendomsAdresseValidationResponse = _eiendomsAdresseValidator.Validate(xPath, eiendom.Adresse);

                ValidationResponse.ValidationRules.AddRange(eiendomsAdresseValidationResponse.ValidationRules);
                ValidationResponse.ValidationMessages.AddRange(eiendomsAdresseValidationResponse.ValidationMessages);

                var matrikkelValidationRules = _matrikkelValidator.Validate(xPath, eiendom.Matrikkel);

                ValidationResponse.ValidationRules.AddRange(matrikkelValidationRules.ValidationRules);
                ValidationResponse.ValidationMessages.AddRange(matrikkelValidationRules.ValidationMessages);



            }
            ValidateDataRelations(eiendommer);

            return ValidationResponse;
        }

        private void ValidateDataRelations(List<Eiendom> eiendommer)
        {
            int index = 0;
            foreach (var eiendom in eiendommer)
            {
                if (!TillattPostnrIKommune(eiendom.Kommunenavn, eiendom.Adresse.Postnr))
                {
                    AddMessageFromRule("tillatte_postnr_i_kommune", index, new List<string>() { eiendom.Adresse.Postnr, eiendom.Kommunenavn });
                }
            }
        }

        public override void InitializeValidationRules(string xPath)
        {
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "bygningsnummer_utfylt", Xpath = $"{xPath}/bygningsnummer" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "bolignummer_utfylt", Xpath = $"{xPath}/bolignummer" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "kommunenavn_utfylt", Xpath = $"{xPath}/kommunenavn" });

            //ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "tillatte_postnr_i_kommune", Xpath = $"{xPath}/" });
        }

        protected void ValidateEntityFields(List<Eiendom> eiendommer)
        {
            int index = 0;

            foreach (var eiendom in eiendommer)
            {
                if (Helpers.ObjectIsNullOrEmpty(eiendom.Bygningsnummer))
                    AddMessageFromRule("bygningsnummer_utfylt",index);

                if (Helpers.ObjectIsNullOrEmpty(eiendom.Bolignummer))
                    AddMessageFromRule("bolignummer_utfylt",index);

                if (Helpers.ObjectIsNullOrEmpty(eiendom.Kommunenavn))
                    AddMessageFromRule("kommunenavn_utfylt", index);

                index++;
            }
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
