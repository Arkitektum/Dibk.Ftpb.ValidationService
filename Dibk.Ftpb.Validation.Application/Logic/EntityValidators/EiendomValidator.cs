﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources;
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
        private readonly IMunicipalityValidator _municipalityValidator;

        public EiendomValidator(string templateXPath, IMunicipalityValidator municipalityValidator) : base()
        {
            InitializeValidationRules(templateXPath);
            
            _eiendomsAdresseValidator = new EiendomsAdresseValidator($"{templateXPath}/adresse");
            ValidationResult.ValidationRules.AddRange(_eiendomsAdresseValidator.ValidationResult.ValidationRules);

            _matrikkelValidator = new MatrikkelValidator($"{templateXPath}/eiendomsidentifikasjon");
            ValidationResult.ValidationRules.AddRange(_matrikkelValidator.ValidationResult.ValidationRules);
            this._municipalityValidator = municipalityValidator;
        }

        public override void InitializeValidationRules(string xPath)
        {
            AddValidationRule("bygningsnummer_utfylt", xPath, "bygningsnummer");
            AddValidationRule("bolignummer_utfylt", xPath, "bolignummer");
            AddValidationRule("kommunenavn_utfylt", xPath, "kommunenavn");
            AddValidationRule("tillatte_postnr_i_kommune", xPath, "postnr");
        }

        public ValidationResult Validate(string xPath, Eiendom eiendom)
        {
            ValidateEntityFields(xPath, eiendom);

            var eiendomsAdresseValidationResponse = _eiendomsAdresseValidator.Validate($"{xPath}/adresse", eiendom.Adresse);
            ValidationResult.ValidationMessages.AddRange(eiendomsAdresseValidationResponse.ValidationMessages);

            var matrikkelValidationRules = _matrikkelValidator.Validate($"{xPath}/eiendomsidentifikasjon", eiendom.Matrikkel);
            ValidationResult.ValidationMessages.AddRange(matrikkelValidationRules.ValidationMessages);

            ValidateDataRelations(xPath, eiendom);

            return ReturnValidationResult(ValidationResult);
        }

        private void ValidateDataRelations(string xPath, Eiendom eiendom)
        {
            if (!TillattPostnrIKommune(eiendom.Kommunenavn, eiendom.Adresse.Postnr))
            {
                AddMessageFromRule("tillatte_postnr_i_kommune", xPath, new List<string>() { eiendom.Adresse.Postnr, eiendom.Kommunenavn });
            }
        }

        protected void ValidateEntityFields(string xPath, Eiendom eiendom)
        {
            if (Helpers.ObjectIsNullOrEmpty(eiendom.Bygningsnummer))
                AddMessageFromRule("bygningsnummer_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendom.Bolignummer))
                AddMessageFromRule("bolignummer_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendom.Kommunenavn))
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
