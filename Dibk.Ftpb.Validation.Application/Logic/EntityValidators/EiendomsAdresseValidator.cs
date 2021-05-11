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
    public class EiendomsAdresseValidator : EntityValidatorBase
    {
        public EiendomsAdresseValidator(string templateXPath) : base()
        {
            InitializeValidationRules(templateXPath);
        }
        public ValidationResult Validate(string xPath, EiendomsAdresse eiendomsAdresse)
        {
            ValidateEntityFields(xPath, eiendomsAdresse);

            return ValidationResult;
        }

        public override void InitializeValidationRules(string xPath)
        {
            AddValidationRule("eiendomsAdresse_adresselinje1_utfylt", xPath,"adresselinje1");
            AddValidationRule("eiendomsAdresse_adresselinje2_utfylt", xPath,"adresselinje2");
            AddValidationRule("eiendomsAdresse_adresselinje3_utfylt", xPath,"adresselinje3");
            AddValidationRule("eiendomsAdresse_landkode_utfylt", xPath,"landkode");
            AddValidationRule("eiendomsAdresse_postnr_utfylt", xPath,"postnr");
            AddValidationRule("eiendomsAdresse_poststed_utfylt", xPath,"poststed");
            AddValidationRule("eiendomsAdresse_gatenavn_utfylt", xPath,"gatenavn");
            AddValidationRule("eiendomsAdresse_husnr_utfylt", xPath,"husnr");
            AddValidationRule("eiendomsAdresse_bokstav_utfylt", xPath,"bokstav");
            AddValidationRule("eiendomsAdresse_postnr_4siffer", xPath,"postnr");
        }

        public void ValidateEntityFields(string xPath, EiendomsAdresse eiendomsAdresse)
        {
            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Adresselinje1))
                AddMessageFromRule("eiendomsAdresse_adresselinje1_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Adresselinje2))
                AddMessageFromRule("eiendomsAdresse_adresselinje2_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Adresselinje3))
                AddMessageFromRule("eiendomsAdresse_adresselinje3_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Landkode))
                AddMessageFromRule("eiendomsAdresse_landkode_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Postnr))
                AddMessageFromRule("eiendomsAdresse_postnr_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Poststed))
                AddMessageFromRule("eiendomsAdresse_poststed_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Gatenavn))
                AddMessageFromRule("eiendomsAdresse_gatenavn_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Husnr))
                AddMessageFromRule("eiendomsAdresse_husnr_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Bokstav))
                AddMessageFromRule("eiendomsAdresse_bokstav_utfylt", xPath);

            if (!StringIs4digitNumber(eiendomsAdresse.Postnr))
                AddMessageFromRule("eiendomsAdresse_postnr_4siffer", xPath);
        }

        private bool StringIs4digitNumber(string input)
        {
            if(int.TryParse(input, out var number))
                return (number >= 0 && number <= 9999);

            return false;
        }
    }
}
