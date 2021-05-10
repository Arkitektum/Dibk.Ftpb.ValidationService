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
        public MatrikkelValidator() : base()
        {
        }
        public ValidationResult Validate(string parentContext, Matrikkel matrikkel)
        {
            string context = $"{parentContext}/eiendomsidentifikasjon";
            InitializeValidationRules(context);
            ValidateEntityFields(matrikkel, context);

            return ValidationResponse;
        }

        public override void InitializeValidationRules(string context)
        {
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "kommunenummer_utfylt", Xpath = $"{context}/Kommunenummer" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "gaardsnummer_utfylt", Xpath = $"{context}/Gaardsnummer" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "bruksnummer_utfylt", Xpath = $"{context}/Bruksnummer" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "festenummer_utfylt", Xpath = $"{context}/Festenummer", ChecklistReference = "13.1" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "seksjonsnummer_utfylt", Xpath =   $"{context}/Seksjonsnummer" });
        }

        public void ValidateEntityFields(Matrikkel matrikkel, string context)
        {
            if (Helpers.ObjectIsNullOrEmpty(matrikkel.Kommunenummer))
                AddMessageFromRule("kommunenummer_utfylt", context);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.Gaardsnummer))
                AddMessageFromRule("gaardsnummer_utfylt", context);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.Bruksnummer))
                AddMessageFromRule("bruksnummer_utfylt", context);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.Festenummer))
                AddMessageFromRule("festenummer_utfylt", context);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.Seksjonsnummer))
                AddMessageFromRule("seksjonsnummer_utfylt", context);
        }
    }
}
