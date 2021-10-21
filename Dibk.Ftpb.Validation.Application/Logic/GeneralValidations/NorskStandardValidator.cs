using System;
using System.Text.RegularExpressions;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.GeneralValidations
{
    public class NorskStandardValidator
    {
        public static FoedselnumerValidation Validate_foedselsnummer(string foedselsnummer)
        {
            if (string.IsNullOrEmpty(foedselsnummer))
            {
                return FoedselnumerValidation.Empty;
            }
            foedselsnummer = GetDecryptedFoedselnummer(foedselsnummer);

            if (string.IsNullOrEmpty(foedselsnummer))
                return FoedselnumerValidation.InvalidEncryption;

            var isfoedselsnummerMatch = FoedselnumerDigitsValidation(foedselsnummer);
            if (!isfoedselsnummerMatch.Success)
                return FoedselnumerValidation.InvalidDigitsControl;

            if (!FoedselsnummerDigitsControl(isfoedselsnummerMatch))
                return FoedselnumerValidation.Invalid;

            return FoedselnumerValidation.Ok;
        }
        public static OrganisasjonsnummerValidation Validate_OrgnummerEnum(string organisasjonsnummer)
        {
            if (String.IsNullOrEmpty(organisasjonsnummer))
            {
                return OrganisasjonsnummerValidation.Empty;
            }
            else
            {
                // https://www.brreg.no/om-oss/oppgavene-vare/alle-registrene-vare/om-enhetsregisteret/organisasjonsnummeret/
                var orgNumerRegexMatch = OrgnummerDigitsValidation(organisasjonsnummer);

                if (orgNumerRegexMatch.Success)
                {
                    if (!OrgnummerDigitsControlValidation(orgNumerRegexMatch))
                    {
                        return OrganisasjonsnummerValidation.Invalid;
                    }
                }
                else
                {
                    return OrganisasjonsnummerValidation.InvalidDigitsControl;
                }
            }
            return OrganisasjonsnummerValidation.Ok;
        }
        public static string GetDecryptedFoedselnummer(string fodselsnummer)
        {
            try
            {
                if (fodselsnummer.Length > 11)
                {
                    //TODO Decryption
                    //fodselsnummer = Decryption.Instance.DecryptText(fodselsnummer);
                }

                return fodselsnummer;
            }
            catch
            {
                //TODO loger - Fødselsnummer kan ikke dekrypteres
                return null;
            }
        }
        public static GeneralValidationResultEnum Bruksenhetsnummer_StandardValidator(string bruksenhetsnr)
        { 
            if (string.IsNullOrEmpty(bruksenhetsnr))
                return GeneralValidationResultEnum.Empty;

            if (!Regex.Match(bruksenhetsnr, @"^[HKLU]\d{4}$").Success)
                return GeneralValidationResultEnum.Invalid;

            return GeneralValidationResultEnum.Ok;
        }

        //Orgnummer methods
        private static Match OrgnummerDigitsValidation(string orgnum)
        {
            if (string.IsNullOrEmpty(orgnum))
                return Match.Empty;

            Match isOrgNumValid = Regex.Match(orgnum, "^([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])$");
            return isOrgNumValid;
        }
        private static bool OrgnummerDigitsControlValidation(Match isOrgNumValid)
        {
            if (isOrgNumValid == null)
                return false;
            int products = Convert.ToInt32(isOrgNumValid.Groups[1].Value) * 3 +
                           Convert.ToInt32(isOrgNumValid.Groups[2].Value) * 2 +
                           Convert.ToInt32(isOrgNumValid.Groups[3].Value) * 7 +
                           Convert.ToInt32(isOrgNumValid.Groups[4].Value) * 6 +
                           Convert.ToInt32(isOrgNumValid.Groups[5].Value) * 5 +
                           Convert.ToInt32(isOrgNumValid.Groups[6].Value) * 4 +
                           Convert.ToInt32(isOrgNumValid.Groups[7].Value) * 3 +
                           Convert.ToInt32(isOrgNumValid.Groups[8].Value) * 2;

            int controlDigit = 11 - (products % 11);
            if (controlDigit == 11)
            {
                controlDigit = 0;
            }
            return controlDigit == Convert.ToInt32(isOrgNumValid.Groups[9].Value);
        }

        //Fødselnummer methods 
        private static Match FoedselnumerDigitsValidation(string foedselsnummer)
        {
            if (string.IsNullOrWhiteSpace(foedselsnummer))
                return Match.Empty;

            Match isFNrValid = Regex.Match(foedselsnummer, "^([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])([0-9])$");
            return isFNrValid;
        }
        private static bool FoedselsnummerDigitsControl(Match isFNrValid)
        {
            int productsFirstControlDigit = ControlDigitCheck(new[] { 3, 7, 6, 1, 8, 9, 4, 5, 2 }, isFNrValid);
            int firstControlDigit = 11 - (productsFirstControlDigit % 11);

            if (firstControlDigit == 11)
                firstControlDigit = 0;

            int productsSecondControlDigit = ControlDigitCheck(new[] { 5, 4, 3, 2, 7, 6, 5, 4, 3 }, isFNrValid, firstControlDigit, 2);
            int secondControlDigit = 11 - (productsSecondControlDigit % 11);

            if (secondControlDigit == 11)
                secondControlDigit = 0;

            if (firstControlDigit == Convert.ToInt32(isFNrValid.Groups[10].Value) && secondControlDigit == Convert.ToInt32(isFNrValid.Groups[11].Value))
                return true;

            return false; //"Fødselsnummeret har ikke gyldig kontrollsiffer"

        }
        private static int ControlDigitCheck(int[] weightCoefficients, Match isNumberValid, int productsFirstControlDigit = 0, int coefficientForFirstControlNumber = 0)
        {
            int products = 0;

            for (int i = 1; i <= weightCoefficients.Length; i++)
            {
                products += Convert.ToInt32(isNumberValid.Groups[i].Value) * weightCoefficients[i - 1];
            }

            if (coefficientForFirstControlNumber != 0)
            {
                products += productsFirstControlDigit * coefficientForFirstControlNumber;
            }
            return products;
        }
    }
}
