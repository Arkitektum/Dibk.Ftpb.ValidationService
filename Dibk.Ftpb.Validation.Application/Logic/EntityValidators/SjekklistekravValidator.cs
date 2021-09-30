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
        
        public ValidationResult Validate(string dataFormatVersion, IEnumerable<SjekklistekravValidationEntity> formsSjekkliste, IChecklistService checklistService)
        {
            try
            {
                if (Helpers.ObjectIsNullOrEmpty(formsSjekkliste) || formsSjekkliste.Count() == 0)
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt);
                }
                else
                {
                    //Validate if all checkpoints in form is valid
                    foreach (var sjekklisteKrav in formsSjekkliste)
                    {
                        var sjekklistepunktValidationResult = _sjekklistepunktValidator.Validate(sjekklisteKrav.ModelData.Sjekklistepunkt);
                        UpdateValidationResultWithSubValidations(sjekklistepunktValidationResult);
                    }


                    //Note: Validates 1.17 in Arbeidsplasser validator
                    //Validate if all checkpoints in Sjekklisten are present in form

                    var checkpointsFromAPI = checklistService.GetChecklist(dataFormatVersion, "metadataid=1");
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


        private void ValidateCheckpoint(IEnumerable<SjekklistekravValidationEntity> formsSjekkliste, Sjekk sjekklistepunkt)
        {
            if (SjekklistepunktFinnesOgErBesvart(formsSjekkliste, sjekklistepunkt.Id))
            {
                var yesAction = sjekklistepunkt.Utfall.FirstOrDefault(x => x.Utfallverdi.Equals(true)).Utfalltypekode;
                var noAction = sjekklistepunkt.Utfall.Where(x => x.Utfallverdi.Equals(false)).FirstOrDefault();

                if (SjekklistepunktBesvartMedJa(formsSjekkliste, sjekklistepunkt.Id))
                {
                    if (yesAction.Equals("DOK"))
                    {
                        SjekklistepunktDokumentasjonFinnes(formsSjekkliste, sjekklistepunkt.Id);
                    }
                    else if (yesAction.Equals("SU"))
                    {
                        foreach (var undersjekkpunkt in sjekklistepunkt.Undersjekkpunkter)
                        {
                            ValidateCheckpoint(formsSjekkliste, undersjekkpunkt);
                        }
                    }
                }
                else
                {
                    if (noAction.Equals("DOK"))
                    {
                        SjekklistepunktDokumentasjonFinnes(formsSjekkliste, sjekklistepunkt.Id);
                    }
                    else if (noAction.Equals("SU"))
                    {
                        foreach (var undersjekkpunkt in sjekklistepunkt.Undersjekkpunkter)
                        {
                            ValidateCheckpoint(formsSjekkliste, undersjekkpunkt);
                        }
                    }
                }
            }
        }

        private bool SjekklistepunktBesvartMedJa(IEnumerable<SjekklistekravValidationEntity> kravliste, string sjekklistepunktnr)
        {
            var kravEntity = kravliste.FirstOrDefault(x => x.ModelData.Sjekklistepunkt.ModelData.Kodeverdi.Equals(sjekklistepunktnr));
            //var xPath = kravEntity.DataModelXpath;
            if (kravEntity != null)
            {
                return (bool)kravEntity.ModelData.Sjekklistepunktsvar;
            }
            else
            {
                throw new ArgumentNullException($"Checklist number {sjekklistepunktnr} doesn't exist.");
            }
        }

        private bool SjekklistepunktDokumentasjonFinnes(IEnumerable<SjekklistekravValidationEntity> kravliste, string sjekklistepunktnr)
        {
            var kravEntity = kravliste.FirstOrDefault(x => x.ModelData.Sjekklistepunkt.ModelData.Kodeverdi.Equals(sjekklistepunktnr));
            //var xPath = kravEntity.DataModelXpath;
            if (kravEntity != null)
            {
                if (string.IsNullOrEmpty(kravEntity.ModelData.Dokumentasjon))
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


        private bool SjekklistepunktFinnesOgErBesvart(IEnumerable<SjekklistekravValidationEntity> formsKravliste, string sjekklistepunktnr)
        {
            var kravEntity = formsKravliste.FirstOrDefault(x => x.ModelData.Sjekklistepunkt.ModelData.Kodeverdi.Equals(sjekklistepunktnr));
            //var xPath = formsKravliste.First().DataModelXpath;
            if (kravEntity == null)
            {
                //AddMessageFromRule(ValidationRuleEnum.utfylt, xPath.Replace("krav[0]", "krav[]") + "/sjekklistepunkt/kodeverdi", new[] { sjekklistepunktnr });
                
                return false;
            }
            else
            {
                var kravet = kravEntity.ModelData;
                var xPath2 = kravet.Sjekklistepunkt.DataModelXpath;
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
