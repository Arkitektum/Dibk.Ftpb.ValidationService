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

            return ValidationResult;
        }

        public override void InitializeValidationRules(string xPath)
        {
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "kommunenummer_utfylt", Xpath = xPath, XmlElement = "/kommunenummer" });
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "gaardsnummer_utfylt", Xpath = xPath, XmlElement = "/gaardsnummer" });
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "bruksnummer_utfylt", Xpath = xPath, XmlElement = "/bruksnummer" });
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "festenummer_utfylt", Xpath = xPath, XmlElement = "/festenummer", ChecklistReference = "13.1" });
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "seksjonsnummer_utfylt", Xpath = xPath, XmlElement = "/seksjonsnummer" });
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
