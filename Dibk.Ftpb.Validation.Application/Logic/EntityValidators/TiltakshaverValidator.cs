using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.GeneralValidations;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public sealed class TiltakshaverValidator : EntityValidatorBase, ITiltakshaverValidator
    {
        public override string ruleXmlElement { get { return "/tiltakshaver"; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        private readonly ICodeListService _codeListService;

        private readonly IEnkelAdresseValidator _enkelAdresseValidator;
        private readonly IKontaktpersonValidator _kontaktpersonValidator;
        private readonly IPartstypeValidator _partstypeValidator;

        public TiltakshaverValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, IEnkelAdresseValidator enkelAdresseValidator, 
            IKontaktpersonValidator kontaktpersonValidator, IPartstypeValidator partstypeValidator , ICodeListService codeListService) 
            : base(entityValidatorOrchestrator)
        {
            _codeListService = codeListService;
            _enkelAdresseValidator = enkelAdresseValidator;
            _kontaktpersonValidator = kontaktpersonValidator;
            _partstypeValidator = partstypeValidator;

        }
        protected override void InitializeValidationRules(string xPathToEntity)
        {
            AddValidationRule(ValidationRuleEnum.tiltakshaver_utfylt, xPathToEntity);
            AddValidationRule(ValidationRuleEnum.tiltakshaver_foedselnummer_utfylt, xPathToEntity, "foedselsnummer");
            AddValidationRule(ValidationRuleEnum.tiltakshaver_foedselnummer_dekryptering, xPathToEntity, "foedselsnummer");
            AddValidationRule(ValidationRuleEnum.tiltakshaver_foedselnummer_kontrollsiffer, xPathToEntity, "foedselsnummer");
            AddValidationRule(ValidationRuleEnum.tiltakshaver_foedselnummer_ugyldig, xPathToEntity, "foedselsnummer");
            AddValidationRule(ValidationRuleEnum.tiltakshaver_organisasjonsnummer_utfylt, xPathToEntity, "organisasjonsnummer");
            AddValidationRule(ValidationRuleEnum.tiltakshaver_organisasjonsnummer_kontrollsiffer, xPathToEntity, "organisasjonsnummer");
            AddValidationRule(ValidationRuleEnum.tiltakshaver_organisasjonsnummer_ugyldig, xPathToEntity, "organisasjonsnummer");
            AddValidationRule(ValidationRuleEnum.tiltakshaver_telmob_utfylt, xPathToEntity);
            AddValidationRule(ValidationRuleEnum.tiltakshaver_epost_utfylt, xPathToEntity, "epost");
            AddValidationRule(ValidationRuleEnum.tiltakshaver_navn_utfylt, xPathToEntity, "navn");
        }


        public ValidationResult Validate(AktoerValidationEntity tiltakshaver = null)
        {
            var xpath = tiltakshaver.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(tiltakshaver.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.tiltakshaver_utfylt, xpath);
            }
            else
            {
                //var partstypeValidatinResults = new PartstypeValidator(_codeListService).Validate(tiltakshaver.ModelData.Partstype);
                var partstypeValidatinResults = _partstypeValidator.Validate(tiltakshaver.ModelData.Partstype);

                UpdateValidationResultWithSubValidations(partstypeValidatinResults);

                //TODO diskutere hvordan man bruke svaret 
                //if validation message have any with tiltakshaver.Partstype.Kodeverdi (ok)
                if (!partstypeValidatinResults.ValidationMessages.Any())
                {
                    ValidateEntityFields(tiltakshaver);
                }
            }
            return _validationResult;
        }

        private void ValidateEntityFields(AktoerValidationEntity aktoerValidationEntity)
        {
            var xpath = aktoerValidationEntity.DataModelXpath;
            var tiltakshaver = aktoerValidationEntity.ModelData;
            if (tiltakshaver.Partstype.ModelData.Kodeverdi == "Privatperson")
            {
                var foedselsnummerValidation = NorskStandardValidator.Validate_foedselsnummer(tiltakshaver.Foedselsnummer);
                switch (foedselsnummerValidation)
                {
                    case FoedselnumerValidation.Empty:
                        AddMessageFromRule(ValidationRuleEnum.tiltakshaver_foedselnummer_utfylt, xpath);
                        break;
                    case FoedselnumerValidation.InvalidEncryption:
                        AddMessageFromRule(ValidationRuleEnum.tiltakshaver_foedselnummer_dekryptering, xpath);
                        break;
                    case FoedselnumerValidation.InvalidDigitsControl:
                        AddMessageFromRule(ValidationRuleEnum.tiltakshaver_foedselnummer_kontrollsiffer, xpath);
                        break;
                    case FoedselnumerValidation.Invalid:
                        AddMessageFromRule(ValidationRuleEnum.tiltakshaver_foedselnummer_ugyldig, xpath);
                        break;
                }
            }
            else
            {
                var organisasjonsnummerValidation = NorskStandardValidator.Validate_OrgnummerEnum(tiltakshaver.Organisasjonsnummer);
                switch (organisasjonsnummerValidation)
                {
                    case OrganisasjonsnummerValidation.Empty:
                        AddMessageFromRule(ValidationRuleEnum.tiltakshaver_organisasjonsnummer_utfylt, xpath);
                        break;
                    case OrganisasjonsnummerValidation.InvalidDigitsControl:
                        AddMessageFromRule(ValidationRuleEnum.tiltakshaver_organisasjonsnummer_kontrollsiffer, xpath);
                        break;
                    case OrganisasjonsnummerValidation.Invalid:
                        AddMessageFromRule(ValidationRuleEnum.tiltakshaver_organisasjonsnummer_ugyldig, xpath);
                        break;
                }

                var enkeladressResult = _enkelAdresseValidator.Validate(tiltakshaver.Adresse);
                UpdateValidationResultWithSubValidations(enkeladressResult);

                //var kontaktpersonResult = new KontaktpersonValidator().Validate(tiltakshaver.Kontaktperson);
                var kontaktpersonValidationResult = _kontaktpersonValidator.Validate(tiltakshaver.Kontaktperson);
                UpdateValidationResultWithSubValidations(kontaktpersonValidationResult);

                if (string.IsNullOrEmpty(tiltakshaver.Mobilnummer) && string.IsNullOrEmpty(tiltakshaver.Telefonnummer))
                    AddMessageFromRule(ValidationRuleEnum.tiltakshaver_telmob_utfylt, xpath);


                if (string.IsNullOrEmpty(tiltakshaver.Epost))
                    AddMessageFromRule(ValidationRuleEnum.tiltakshaver_epost_utfylt, xpath);

                if (string.IsNullOrEmpty(tiltakshaver.Navn))
                    AddMessageFromRule(ValidationRuleEnum.tiltakshaver_navn_utfylt, xpath);

            }
        }

        private void UpdateValidationResultWithSubValidations(ValidationResult newValudationResult)
        {
            _validationResult.ValidationRules.AddRange(newValudationResult.ValidationRules);
            _validationResult.ValidationMessages.AddRange(newValudationResult.ValidationMessages);
        }
    }
}
