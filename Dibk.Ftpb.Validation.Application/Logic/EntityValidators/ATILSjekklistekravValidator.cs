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
    public class ATILSjekklistekravValidator : EntityValidatorBase, ISjekklistekravValidator
    {
        private readonly ICodeListService _codeListService;

        //private readonly ISjekklistepunktValidator _sjekklistepunktValidator;
        private readonly IKodelisteValidator _sjekklistepunktValidator; 
        //private List<KodeEntry> _kodelisten;

        public ValidationResult ValidationResult { get => _validationResult; }

        public ATILSjekklistekravValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeid, IKodelisteValidator sjekklistepunktValidator, ICodeListService codeListService)
            : base(entityValidatorTree, nodeid)
        {
            _codeListService = codeListService;
            _sjekklistepunktValidator = sjekklistepunktValidator;
            //PopulateKodelisten();
        }
        public ATILSjekklistekravValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeid)
            : base(entityValidatorTree, nodeid)
        {
        }
        


        protected override void InitializeValidationRules()
        {
            //this.AddValidationRule(ValidationRuleEnum.krav_utfylt);
            //this.AddValidationRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_utfylt, "sjekklistepunktsvar");
            //this.AddValidationRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_oppfylt, "sjekklistepunktsvar");
            //this.AddValidationRule(ValidationRuleEnum.krav_sjekklistekrav_dokumentasjon_utfylt, "dokumentasjon");
            //this.AddValidationRule(ValidationRuleEnum.krav_sjekklistekrav_dokumentasjon_utfylt, "dokumentasjon");

            //this.AddValidationRule(SjekklistekravEnum.kodeverdi_gyldig);
            //this.AddValidationRule(ATILSjekklistekravEnum.kodeverdi_mangler, "sjekklistepunktsvar");
            //this.AddValidationRule(ATILSjekklistekravEnum.kodeverdi_mangler, "dokumentasjon");


            this.AddValidationRule(ValidationRuleEnum.utfylt);
            this.AddValidationRule(ValidationRuleEnum.utfylt, "sjekklistepunktsvar");
            //this.AddValidationRule(ValidationRuleEnum.sjekklistepunktsvar_utfylt, "sjekklistepunktsvar");
            this.AddValidationRule(ValidationRuleEnum.utfylt, "dokumentasjon");

            //this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_17_sjekklistepunktsvar_utfylt, "sjekklistepunktsvar");
            //this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_17_sjekklistepunktsvar_gyldig, "sjekklistepunktsvar");
            //this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_17_dokumentasjon_utfylt, "dokumentasjon");


            //AddValidationRules();
        }        
        
        public ValidationResult Validate(IEnumerable<SjekklistekravValidationEntity> formsSjekkliste, IChecklistService checklistService)
        {
            try
            {
                if (Helpers.ObjectIsNullOrEmpty(formsSjekkliste) || formsSjekkliste.Count() == 0)
                {
                    //AddMessageFromRuleIfCollectionIsEmpty(ValidationRuleEnum.krav_utfylt);
                    AddMessageFromRule(ValidationRuleEnum.utfylt);
                }
                else
                {
                    //Validate if all checkpoints in form is valid
                    foreach (var sjekklisteKrav in formsSjekkliste)
                    {
                        //sjekklisteKrav.ModelData.Sjekklistepunkt.ModelData.Kodeverdi
                        //var svar = sjekklisteKrav.ModelData.Sjekklistepunktsvar;
                        //var dokumentasjon = sjekklisteKrav.ModelData.Dokumentasjon;

                        var sjekklistepunktValidationResult = _sjekklistepunktValidator.Validate(sjekklisteKrav.ModelData.Sjekklistepunkt);
                        UpdateValidationResultWithSubValidations(sjekklistepunktValidationResult);
                    }


                    //Note: Validates 1.17 in Arbeidsplasser validator
                    //Validate if all checkpoints in Sjekklisten are present in form

                    var checkpointsFromAPI = checklistService.GetAtilCheckpoints("AT?metadataid=1");

                    foreach (var checkpoint in checkpointsFromAPI)
                    {
                        ValidateCheckpoint(formsSjekkliste, checkpoint);
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
            var xPath = kravEntity.DataModelXpath;
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
            var xPath = kravEntity.DataModelXpath;
            if (kravEntity != null)
            {
                if (string.IsNullOrEmpty(kravEntity.ModelData.Dokumentasjon))
                {
                    //var enums = Helpers.GetSjekklistekravEnumFromIndex(sjekklistepunktnr);
                    //var sjekklistekravEnum = enums.First(x => Enum.GetName(typeof(ATILSjekklistekravEnum), x).Contains("dokumentasjon_utfylt"));
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/dokumentasjon");
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
            var xPath = formsKravliste.First().DataModelXpath;
            if (kravEntity == null)
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, xPath.Replace("krav[0]", "krav[]") + "/sjekklistepunkt/kodeverdi", new[] { sjekklistepunktnr });
                
                return false;
            }
            else
            {
                var kravet = kravEntity.ModelData;
                var xPath2 = kravet.Sjekklistepunkt.DataModelXpath;
                if (kravet.Sjekklistepunktsvar == null)
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, xPath.Replace("krav[0]", "krav[]") + "/sjekklistepunktsvar", new[] { sjekklistepunktnr });
                    return false;
                }

                //TODO: Maybe not: Use sjekklistepunktValidator to verfy correct description from GeoNorge ?? Necessary ???
                //Here......
            }

            return true;
        }

        public void ValidateEntityFields(SjekklistekravValidationEntity sjekklistekravValidationEntity)
        {
            //var xpath = sjekklistekravValidationEntity.DataModelXpath;
            //var sjekklistekrav = sjekklistekravValidationEntity.ModelData;

            //if (Helpers.ObjectIsNullOrEmpty(sjekklistekrav.Sjekklistepunktsvar))
            //{
            //    AddMessageFromRule(SjekklistekravEnum.sjekklistepunktSvar_utfylt, xpath, new [] { sjekklistekrav.Sjekklistepunkt.ModelData.Kodeverdi });
            //}



            //if (!sjekklistekrav.Sjekklistepunktsvar == true)
            //{
            //    AddMessageFromRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_oppfylt, xpath, new[] { sjekklistekrav.Sjekklistepunkt.ModelData.Kodeverdi });
            //}
            //if (string.IsNullOrEmpty(sjekklistekrav.Dokumentasjon))
            //{
            //    AddMessageFromRule(ValidationRuleEnum.krav_sjekklistekrav_dokumentasjon_utfylt, xpath);
            //}

        }

        private void AddValidationRules()
        {
            this.AddValidationRule(ValidationRuleEnum.utfylt);

            List<string> checklistNumbers = new List<string>() {"1.13", "1.16", "10.1" };

            foreach (var checklistNumber in checklistNumbers)
            {
                var enums = Helpers.GetSjekklistekravEnumFromIndex(checklistNumber);
                //var xx = enums.First(x => Enum.GetName(typeof(SjekklistekravEnum), x).Contains("kodeverdi_utfylt"));

                this.AddValidationRule(enums.First(x => Enum.GetName(typeof(ATILSjekklistekravEnum), x).Contains("kodeverdi_utfylt")), "sjekklistepunkt/kodeverdi");
                this.AddValidationRule(enums.First(x => Enum.GetName(typeof(ATILSjekklistekravEnum), x).Contains("kodebeskrivelse_gyldig")), "kodebeskrivelse");
                this.AddValidationRule(enums.First(x => Enum.GetName(typeof(ATILSjekklistekravEnum), x).Contains("sjekklistepunktsvar_gyldig")), "sjekklistepunktsvar");
                this.AddValidationRule(enums.First(x => Enum.GetName(typeof(ATILSjekklistekravEnum), x).Contains("dokumentasjon_utfylt")), "dokumentasjon");

            }

            //this.AddValidationRule(SjekklistekravEnum.pkt_1_13_kodeverdi_utfylt, "kodeverdi");
            //this.AddValidationRule(SjekklistekravEnum.pkt_1_13_kodebeskrivelse_gyldig, "kodebeskrivelse");
            //this.AddValidationRule(SjekklistekravEnum.pkt_1_13_sjekklistepunktsvar_gyldig, "sjekklistepunktsvar");
            //this.AddValidationRule(SjekklistekravEnum.pkt_1_13_dokumentasjon_utfylt, "dokumentasjon");

            //this.AddValidationRule(SjekklistekravEnum.pkt_1_16_kodeverdi_utfylt, "kodeverdi");
            //this.AddValidationRule(SjekklistekravEnum.pkt_1_16_kodebeskrivelse_gyldig, "kodebeskrivelse");
            //this.AddValidationRule(SjekklistekravEnum.pkt_1_16_sjekklistepunktsvar_gyldig, "sjekklistepunktsvar");
            //this.AddValidationRule(SjekklistekravEnum.pkt_1_16_dokumentasjon_utfylt, "dokumentasjon");

            //this.AddValidationRule(SjekklistekravEnum.pkt_10_1_kodeverdi_utfylt, "kodeverdi");
            //this.AddValidationRule(SjekklistekravEnum.pkt_10_1_kodebeskrivelse_gyldig, "kodebeskrivelse");
            //this.AddValidationRule(SjekklistekravEnum.pkt_10_1_sjekklistepunktsvar_gyldig, "sjekklistepunktsvar");
            //this.AddValidationRule(SjekklistekravEnum.pkt_10_1_dokumentasjon_utfylt, "dokumentasjon");

        }

    }
    //public class KodeEntry
    //{
    //    public string Kodeverdi { get; set; }
    //    //public string Kodebeskrivelse { get; set; }
    //    public SjekklisteAnswerOutcomeEnum AnswerYesOutcome { get; set; }
    //    public SjekklisteAnswerOutcomeEnum AnswerNoOutcome { get; set; }

    //    public KodeEntry KodeEntryForYes { get; set; }
    //    public KodeEntry KodeEntryForNo { get; set; }
    //}
}
