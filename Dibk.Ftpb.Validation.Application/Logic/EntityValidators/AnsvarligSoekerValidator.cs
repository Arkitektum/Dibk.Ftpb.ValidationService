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
    public sealed class AnsvarligSoekerValidator : EntityValidatorBase, ITiltakshaverValidator
    {
        public override string ruleXmlElement { get { return "/ansvarligSoeker"; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        private readonly ICodeListService _codeListService;

        private readonly IEnkelAdresseValidator _enkelAdresseValidator;
        private readonly IKontaktpersonValidator _kontaktpersonValidator;
        private readonly IPartstypeValidator _partstypeValidator;

        public AnsvarligSoekerValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, IEnkelAdresseValidator enkelAdresseValidator, 
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
            AddValidationRule(ValidationRuleEnum.ansvarligSoeker_utfylt, xPathToEntity);
            AddValidationRule(ValidationRuleEnum.ansvarligSoeker_foedselnummer_utfylt, xPathToEntity, "foedselsnummer");
            AddValidationRule(ValidationRuleEnum.ansvarligSoeker_foedselnummer_dekryptering, xPathToEntity, "foedselsnummer");
            AddValidationRule(ValidationRuleEnum.ansvarligSoeker_foedselnummer_kontrollsiffer, xPathToEntity, "foedselsnummer");
            AddValidationRule(ValidationRuleEnum.ansvarligSoeker_foedselnummer_ugyldig, xPathToEntity, "foedselsnummer");
            AddValidationRule(ValidationRuleEnum.ansvarligSoeker_organisasjonsnummer_utfylt, xPathToEntity, "organisasjonsnummer");
            AddValidationRule(ValidationRuleEnum.ansvarligSoeker_organisasjonsnummer_kontrollsiffer, xPathToEntity, "organisasjonsnummer");
            AddValidationRule(ValidationRuleEnum.ansvarligSoeker_organisasjonsnummer_ugyldig, xPathToEntity, "organisasjonsnummer");
            AddValidationRule(ValidationRuleEnum.ansvarligSoeker_telmob_utfylt, xPathToEntity);
            AddValidationRule(ValidationRuleEnum.ansvarligSoeker_epost_utfylt, xPathToEntity, "epost");
            AddValidationRule(ValidationRuleEnum.ansvarligSoeker_navn_utfylt, xPathToEntity, "navn");
        }


        public ValidationResult Validate(AktoerValidationEntity ansvarligSoeker = null)
        {
            var xpath = ansvarligSoeker.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(ansvarligSoeker.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.ansvarligSoeker_utfylt, xpath);
            }
            else
            {
                //var partstypeValidatinResults = new PartstypeValidator(_codeListService).Validate(tiltakshaver.ModelData.Partstype);
                var partstypeValidatinResults = _partstypeValidator.Validate(ansvarligSoeker.ModelData.Partstype);

                UpdateValidationResultWithSubValidations(partstypeValidatinResults);

                //TODO diskutere hvordan man bruke svaret 
                //if validation message have any with tiltakshaver.Partstype.Kodeverdi (ok)
                if (!partstypeValidatinResults.ValidationMessages.Any())
                {
                    ValidateEntityFields(ansvarligSoeker);
                }
            }
            return _validationResult;
        }

        private void ValidateEntityFields(AktoerValidationEntity aktoerValidationEntity)
        {
            var xpath = aktoerValidationEntity.DataModelXpath;
            var ansvarligSoeker = aktoerValidationEntity.ModelData;
            if (ansvarligSoeker.Partstype.ModelData.Kodeverdi == "Privatperson")
            {
                var foedselsnummerValidation = NorskStandardValidator.Validate_foedselsnummer(ansvarligSoeker.Foedselsnummer);
                switch (foedselsnummerValidation)
                {
                    case FoedselnumerValidation.Empty:
                        AddMessageFromRule(ValidationRuleEnum.ansvarligSoeker_foedselnummer_utfylt, xpath);
                        break;
                    case FoedselnumerValidation.InvalidEncryption:
                        AddMessageFromRule(ValidationRuleEnum.ansvarligSoeker_foedselnummer_dekryptering, xpath);
                        break;
                    case FoedselnumerValidation.InvalidDigitsControl:
                        AddMessageFromRule(ValidationRuleEnum.ansvarligSoeker_foedselnummer_kontrollsiffer, xpath);
                        break;
                    case FoedselnumerValidation.Invalid:
                        AddMessageFromRule(ValidationRuleEnum.ansvarligSoeker_foedselnummer_ugyldig, xpath);
                        break;
                }
            }
            else
            {
                var organisasjonsnummerValidation = NorskStandardValidator.Validate_OrgnummerEnum(ansvarligSoeker.Organisasjonsnummer);
                switch (organisasjonsnummerValidation)
                {
                    case OrganisasjonsnummerValidation.Empty:
                        AddMessageFromRule(ValidationRuleEnum.ansvarligSoeker_organisasjonsnummer_utfylt, xpath);
                        break;
                    case OrganisasjonsnummerValidation.InvalidDigitsControl:
                        AddMessageFromRule(ValidationRuleEnum.ansvarligSoeker_organisasjonsnummer_kontrollsiffer, xpath);
                        break;
                    case OrganisasjonsnummerValidation.Invalid:
                        AddMessageFromRule(ValidationRuleEnum.ansvarligSoeker_organisasjonsnummer_ugyldig, xpath);
                        break;
                }

                var enkeladressResult = _enkelAdresseValidator.Validate(ansvarligSoeker.Adresse);
                UpdateValidationResultWithSubValidations(enkeladressResult);

                //var kontaktpersonResult = new KontaktpersonValidator().Validate(tiltakshaver.Kontaktperson);
                var kontaktpersonValidationResult = _kontaktpersonValidator.Validate(ansvarligSoeker.Kontaktperson);
                UpdateValidationResultWithSubValidations(kontaktpersonValidationResult);

                if (string.IsNullOrEmpty(ansvarligSoeker.Mobilnummer) && string.IsNullOrEmpty(ansvarligSoeker.Telefonnummer))
                    AddMessageFromRule(ValidationRuleEnum.ansvarligSoeker_telmob_utfylt, xpath);


                if (string.IsNullOrEmpty(ansvarligSoeker.Epost))
                    AddMessageFromRule(ValidationRuleEnum.ansvarligSoeker_epost_utfylt, xpath);

                if (string.IsNullOrEmpty(ansvarligSoeker.Navn))
                    AddMessageFromRule(ValidationRuleEnum.ansvarligSoeker_navn_utfylt, xpath);

            }
        }

        private void UpdateValidationResultWithSubValidations(ValidationResult newValudationResult)
        {
            _validationResult.ValidationRules.AddRange(newValudationResult.ValidationRules);
            _validationResult.ValidationMessages.AddRange(newValudationResult.ValidationMessages);
        }
    }
}
