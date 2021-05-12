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
    public class MatrikkelValidator : EntityValidatorBase
    {
        public MatrikkelValidator(string templateXPath) : base()
        {
            InitializeValidationRules(templateXPath);
        }
        public ValidationResult Validate(string xPath, Matrikkel matrikkel)
        {
            ValidateEntityFields(xPath, matrikkel);

            return ReturnValidationResult(ValidationResult);
        }

        public override void InitializeValidationRules(string xPath)
        {
            AddValidationRule("kommunenummer_utfylt", xPath, "kommunenummer");
            AddValidationRule("gaardsnummer_utfylt", xPath, "gaardsnummer");
            AddValidationRule("bruksnummer_utfylt", xPath, "bruksnummer");
            AddValidationRule("festenummer_utfylt", xPath, "festenummer");
            AddValidationRule("seksjonsnummer_utfylt", xPath, "seksjonsnummer");
        }

        public void ValidateEntityFields(string xPath, Matrikkel matrikkel)
        {
            if (Helpers.ObjectIsNullOrEmpty(matrikkel.Kommunenummer))
                AddMessageFromRule("kommunenummer_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.Gaardsnummer))
                AddMessageFromRule("gaardsnummer_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.Bruksnummer))
                AddMessageFromRule("bruksnummer_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.Festenummer))
                AddMessageFromRule("festenummer_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.Seksjonsnummer))
                AddMessageFromRule("seksjonsnummer_utfylt", xPath);
        }
    }
}
