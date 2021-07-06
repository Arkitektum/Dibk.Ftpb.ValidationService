using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System;
using System.Linq;
using System.Reflection;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.GeneralValidations;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class FakturamottakerValidator : EntityValidatorBase, IFakturamottakerValidator
    {
        //public override string ruleXmlElement { get { return "fakturamottaker"; } set { ruleXmlElement = value; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        private readonly IEnkelAdresseValidator _enkelAdresseValidator;

        //public FakturamottakerValidator(FormValidatorConfiguration formValidatorConfiguration, IEnkelAdresseValidator enkelAdresseValidator) 
        //    : base(formValidatorConfiguration)
        //{
        //    //TODO: Automize this?
        //    _enkelAdresseValidator = enkelAdresseValidator;
        //}

        public FakturamottakerValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, IEnkelAdresseValidator enkelAdresseValidator) 
            : base(entityValidatorTree, nodeId)
        {
            //TODO: Automize this?
            _enkelAdresseValidator = enkelAdresseValidator;
        }


        protected override void InitializeValidationRules()
        {
            AddValidationRule(FakturamottakerValidationEnums.fakturamottaker_utfylt, "fakturamottaker");
            AddValidationRule(FakturamottakerValidationEnums.organisasjonsnummer_utfylt, "organisasjonsnummer");
            AddValidationRule(FakturamottakerValidationEnums.organisasjonsnummer_kontrollsiffer, "organisasjonsnummer");
            AddValidationRule(FakturamottakerValidationEnums.organisasjonsnummer_ugyldig, "organisasjonsnummer");

        }

        public ValidationResult Validate(FakturamottakerValidationEntity fakturamottaker = null)
        {
            var xpath = fakturamottaker.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(fakturamottaker.ModelData))
            {
                AddMessageFromRule(FakturamottakerValidationEnums.fakturamottaker_utfylt, xpath);
            }
            else
            {
                if (string.IsNullOrEmpty(fakturamottaker.ModelData.Organisasjonsnummer))
                {
                    AddMessageFromRule(FakturamottakerValidationEnums.organisasjonsnummer_utfylt, xpath);
                }
                else
                {
                    var organisasjonsnummerValidation = NorskStandardValidator.Validate_OrgnummerEnum(fakturamottaker.ModelData.Organisasjonsnummer);
                    switch (organisasjonsnummerValidation)
                    {
                        case OrganisasjonsnummerValidation.Empty:
                            AddMessageFromRule(FakturamottakerValidationEnums.organisasjonsnummer_utfylt, xpath);
                            break;
                        case OrganisasjonsnummerValidation.InvalidDigitsControl:
                            AddMessageFromRule(FakturamottakerValidationEnums.organisasjonsnummer_kontrollsiffer, xpath);
                            break;
                        case OrganisasjonsnummerValidation.Invalid:
                            AddMessageFromRule(FakturamottakerValidationEnums.organisasjonsnummer_ugyldig, xpath);
                            break;
                    }
                }

                var adresseValidationResult = _enkelAdresseValidator.Validate(fakturamottaker.ModelData.Adresse);
                UpdateValidationResultWithSubValidations(adresseValidationResult);
            }
            return ValidationResult;
        }
    }
}
