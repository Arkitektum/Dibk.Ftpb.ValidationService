using System;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.GeneralValidations;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EnkelAdresseValidator : EntityValidatorBase, IEnkelAdresseValidator
    {
        //public override string ruleXmlElement { get { return "adresse"; } set { ruleXmlElement = value; } }

        ValidationResult IEntityBaseValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public EnkelAdresseValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId)
            : base(entityValidatorTree, nodeId)
        {
        }


        protected override void InitializeValidationRules()
        {
            AddValidationRule(EnkelAdresseValidationEnums.adresse_utfylt, "adresse");
            AddValidationRule(EnkelAdresseValidationEnums.adresse_utfylt, "adresse");
            AddValidationRule(EnkelAdresseValidationEnums.adresselinje1_utfylt, "adresselinje1");
            AddValidationRule(EnkelAdresseValidationEnums.landkode_utfylt, "landkode");
            AddValidationRule(EnkelAdresseValidationEnums.postnr_utfylt, "postnr");
        }

        public ValidationResult Validate(EnkelAdresseValidationEntity enkelAdresse = null)
        {
            var xpath = enkelAdresse.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(enkelAdresse?.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.adresse_utfylt, xpath);
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
                            AddMessageFromRule(EnkelAdresseValidationEnums.postnr_kontrollSiffer, xPath);
                        }
                        else
                        {
                            var postnrValidation = new PostalCode.BringPostalCodeProvider().ValidatePostnr(postNr, landkode);
                            if (postnrValidation != null)
                            {
                                if (!postnrValidation.Valid)
                                {
                                    _validationResult.AddMessage("4845.1.6.12", null);
                                }
                                else
                                {
                                    if (!postnrValidation.Result.Equals(tiltakshaver.adresse.poststed, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        _validationResult.AddMessage("4845.1.6.13", new[] { postNr, tiltakshaver.adresse.poststed, postnrValidation.Result });
                                    }
                                }
                            }
                            else
                            {
                                _validationResult.AddMessage("4845.1.6.14", null);
                            }
                        }
                    }
                }

            }


        }

        private bool HerBurDetGalningar(string input)
        {
            if (int.TryParse(input, out var number))
                return (number >= 0 && number <= 1111);

            return false;
        }
    }
}
