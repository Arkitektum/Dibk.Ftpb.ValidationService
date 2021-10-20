using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Services;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class SjekklistekravValidator : EntityValidatorBase, ISjekklistekravValidator
    {
        private readonly ICodeListService _codeListService;

        private readonly IKodelisteValidator _sjekklistepunktValidator;

        public ValidationResult ValidationResult { get => _validationResult; }

        public SjekklistekravValidator(IList<EntityValidatorNode> entityValidatorTree, IKodelisteValidator sjekklistepunktValidator, ICodeListService codeListService)
            : base(entityValidatorTree)
        {
            _codeListService = codeListService;
            _sjekklistepunktValidator = sjekklistepunktValidator;
        }

        protected override void InitializeValidationRules()
        {
            this.AddValidationRule(ValidationRuleEnum.utfylt);
            this.AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.sjekklistepunktsvar);
            this.AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.dokumentasjon);
        }

        public ValidationResult Validate(string dataFormatId, string dataFormatVersion, SjekklistekravValidationEntity[] formsSjekkliste, IChecklistService checklistService)
        {
            try
            {
                if (Helpers.ObjectIsNullOrEmpty(formsSjekkliste) || formsSjekkliste.Count() == 0)
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt);
                }
                else
                {
                    var index = GetArrayIndex(formsSjekkliste);

                    for (int i = 0; i < index; i++)
                    {
                        var sjekkliste = Helpers.ObjectIsNullOrEmpty(formsSjekkliste) ? null : formsSjekkliste[i];
                        var sjekklistepunktValidationResult = _sjekklistepunktValidator.Validate(sjekkliste?.Sjekklistepunkt);
                        UpdateValidationResultWithSubValidations(sjekklistepunktValidationResult, i);
                    }

                    //Note: Validates 1.17 in Arbeidsplasser validator
                    //Validate if all checkpoints in Sjekklisten are present in form

                    var checkpointsFromAPI = checklistService.GetChecklist(dataFormatId, dataFormatVersion, "metadataid=1");
                    //var checkpointsFromAPI = checklistService.GetChecklist(dataFormatVersion);

                    if (checkpointsFromAPI.Count() == 0)
                    {
                        throw new ArgumentNullException($"Checklist service {checklistService}");
                    }
                    else
                    {
                        foreach (var checkpoint in checkpointsFromAPI)
                        {
                            ValidateCheckpoint(formsSjekkliste, checkpoint);
                        }

                    }

                }

                return _validationResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private void ValidateCheckpoint(SjekklistekravValidationEntity[] formsSjekkliste, Sjekk sjekklistepunkt)
        {
            if (SjekklistepunktFinnesOgErBesvart(formsSjekkliste, sjekklistepunkt.Id))
            {
                string yesOutcome = "";
                string noOutcome = "";

                var yesAction = sjekklistepunkt.Utfall.Where(x => x.Utfallverdi.Equals(true)).FirstOrDefault();
                if (yesAction != null)
                {
                    yesOutcome = yesAction.Utfalltypekode;
                }

                var noAction = sjekklistepunkt.Utfall.Where(x => x.Utfallverdi.Equals(false)).FirstOrDefault();
                if (noAction != null)
                {
                    noOutcome = noAction.Utfalltypekode;
                }

                if (SjekklistepunktBesvartMedJa(formsSjekkliste, sjekklistepunkt.Id))
                {
                    if (yesAction != null && yesOutcome.Equals("DOK"))
                    {
                        SjekklistepunktDokumentasjonFinnes(formsSjekkliste, sjekklistepunkt.Id);
                    }
                    else if (yesAction != null && yesOutcome.Equals("SU"))
                    {
                        foreach (var undersjekkpunkt in sjekklistepunkt.Undersjekkpunkter)
                        {
                            ValidateCheckpoint(formsSjekkliste, undersjekkpunkt);
                        }
                    }
                }
                else
                {
                    if (noAction != null && noOutcome.Equals("DOK"))
                    {
                        SjekklistepunktDokumentasjonFinnes(formsSjekkliste, sjekklistepunkt.Id);
                    }
                    else if (noAction != null && noOutcome.Equals("SU"))
                    {
                        foreach (var undersjekkpunkt in sjekklistepunkt.Undersjekkpunkter)
                        {
                            ValidateCheckpoint(formsSjekkliste, undersjekkpunkt);
                        }
                    }
                }
            }
        }

        private bool SjekklistepunktBesvartMedJa(SjekklistekravValidationEntity[] kravliste, string sjekklistepunktnr)
        {
            var kravEntity = kravliste.FirstOrDefault(x => x.Sjekklistepunkt.Kodeverdi.Equals(sjekklistepunktnr));
            //var xPath = kravEntity.DataModelXpath;
            if (kravEntity != null)
            {
                return (bool)kravEntity.Sjekklistepunktsvar;
            }
            else
            {
                throw new ArgumentNullException($"Checklist number {sjekklistepunktnr} doesn't exist.");
            }
        }

        private bool SjekklistepunktDokumentasjonFinnes(SjekklistekravValidationEntity[] kravliste, string sjekklistepunktnr)
        {
            var kravEntity = kravliste.FirstOrDefault(x => x.Sjekklistepunkt.Kodeverdi.Equals(sjekklistepunktnr));
            //var xPath = kravEntity.DataModelXpath;
            if (kravEntity != null)
            {
                if (string.IsNullOrEmpty(kravEntity.Dokumentasjon))
                {
                    //AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.dokumentasjon}");
                    return false;
                }
                return true;
            }
            else
            {
                throw new ArgumentNullException($"Checklist number {sjekklistepunktnr} doesn't exist.");
            }
        }


        private bool SjekklistepunktFinnesOgErBesvart(SjekklistekravValidationEntity[] formsKravliste, string sjekklistepunktnr)
        {
            var kravEntity = formsKravliste.FirstOrDefault(x => x.Sjekklistepunkt.Kodeverdi.Equals(sjekklistepunktnr));
            //var xPath = formsKravliste.First().DataModelXpath;
            if (kravEntity == null)
            {
                //AddMessageFromRule(ValidationRuleEnum.utfylt, xPath.Replace("krav[0]", "krav[]") + "/sjekklistepunkt/kodeverdi", new[] { sjekklistepunktnr });

                return false;
            }
            else
            {
                var kravet = kravEntity;
                //var xPath2 = kravet.Sjekklistepunkt.DataModelXpath;
                if (kravet.Sjekklistepunktsvar == null)
                {
                    //AddMessageFromRule(ValidationRuleEnum.utfylt, xPath.Replace("krav[0]", "krav[]") + "/sjekklistepunktsvar", new[] { sjekklistepunktnr });
                    return false;
                }

                //TODO: Maybe not: Use sjekklistepunktValidator to verfy correct description from GeoNorge ?? Necessary ???
                //Here......
            }

            return true;
        }
    }
}
