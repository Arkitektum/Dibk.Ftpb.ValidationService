using System;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Text.RegularExpressions;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Logic.GeneralValidations;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EnkelAdresseValidator : EntityValidatorBase, IEnkelAdresseValidator
    {
        private readonly IPostalCodeService _postalCodeService;

        ValidationResult IEntityBaseValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public EnkelAdresseValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, IPostalCodeService postalCodeService)
            : base(entityValidatorTree, nodeId)
        {
            _postalCodeService = postalCodeService;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(EnkelAdresseValidationEnums.utfylt);
            AddValidationRule(EnkelAdresseValidationEnums.adresselinje1_utfylt, "adresselinje1");
            AddValidationRule(EnkelAdresseValidationEnums.landkode_utfylt, "landkode");
            AddValidationRule(EnkelAdresseValidationEnums.postnr_utfylt, "postnr");
        }

        public ValidationResult Validate(EnkelAdresseValidationEntity enkelAdresse = null)
        {
            var xpath = enkelAdresse.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(enkelAdresse?.ModelData))
            {
                AddMessageFromRule(EnkelAdresseValidationEnums.utfylt, xpath);
            }
            else
            {
                ValidateEntityFields(enkelAdresse);
            }

            return _validationResult;
        }

        public void ValidateEntityFields(EnkelAdresseValidationEntity adresseValidationEntity)
        {
            var xPath = adresseValidationEntity.DataModelXpath;
            var adresse = adresseValidationEntity.ModelData;

            if (string.IsNullOrEmpty(adresse.Adresselinje1))
            {
                AddMessageFromRule(EnkelAdresseValidationEnums.adresselinje1_utfylt, xPath);
            }
            else
            {
                if (!CountryCodeHandler.IsCountryNorway(adresse.Landkode))
                {
                    if (!CountryCodeHandler.VerifyCountryCode(adresse.Landkode))
                    {
                        AddMessageFromRule(EnkelAdresseValidationEnums.landkode_ugyldug, xPath);
                    }
                }
                else
                {
                    var postNr = adresse.Postnr;
                    var landkode = adresse.Landkode;

                    if (string.IsNullOrEmpty(postNr))
                    {
                        AddMessageFromRule(EnkelAdresseValidationEnums.postnr_utfylt, xPath);
                    }
                    else
                    {
                        Match isPostNrValid = Regex.Match(postNr, "^([0-9])([0-9])([0-9])([0-9])$");
                        if (!isPostNrValid.Success)
                        {
                            AddMessageFromRule(EnkelAdresseValidationEnums.postnr_kontrollsiffer, xPath, new[] { postNr });
                        }
                        else
                        {
                            var postnrValidation = _postalCodeService.ValidatePostnr(postNr, landkode);
                            if (postnrValidation != null)
                            {
                                if (!postnrValidation.Valid)
                                {
                                    AddMessageFromRule(EnkelAdresseValidationEnums.postnr_ugyldig, xPath, new[] { postNr });
                                }
                                else
                                {
                                    if (!postnrValidation.Result.Equals(adresse.Poststed, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        AddMessageFromRule(EnkelAdresseValidationEnums.postnr_stemmerIkke, xPath, new[] { postNr, adresse.Poststed, postnrValidation.Result });
                                    }
                                }
                            }
                            else
                            {
                                AddMessageFromRule(EnkelAdresseValidationEnums.postnr_ikke_validert, xPath);
                            }
                        }
                    }
                }
            }
        }
    }
}
