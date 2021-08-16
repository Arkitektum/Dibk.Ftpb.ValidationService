using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class ATILSjekklistekravValidator : EntityValidatorBase, ISjekklistekravValidator
    {
        //private readonly ISjekklistepunktValidator _sjekklistepunktValidator;
        private readonly ATILSjekklistepunktValidator _sjekklistepunktValidator; 
        private List<KodeEntry> _kodelisten;

        public ValidationResult ValidationResult { get => _validationResult; }

        public ATILSjekklistekravValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeid, ATILSjekklistepunktValidator sjekklistepunktValidator)
            : base(entityValidatorTree, nodeid)
        {
            _sjekklistepunktValidator = sjekklistepunktValidator;
            PopulateKodelisten();
        }
        public ATILSjekklistekravValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeid)
            : base(entityValidatorTree, nodeid)
        {
        }
        private void PopulateKodelisten()
        {
            _kodelisten = new List<KodeEntry>();
            _kodelisten.Add(new KodeEntry() { Kodeverdi = "1.14", Kodebeskrivelse = "dfasdfsaf" });
            _kodelisten.Add(new KodeEntry() { Kodeverdi = "1.17", Kodebeskrivelse = "sdfsdf" });
            _kodelisten.Add(new KodeEntry() { Kodeverdi = "2.1", Kodebeskrivelse = "Er arbeidstakere sikret mot fall i tråd med følgende krav?", AnswerNoOutcome = SjekklisteAnswerOutcomeEnum.Underpunkt, AnswerYesOutcome = SjekklisteAnswerOutcomeEnum.Dokumentasjon,
                             KodeEntryForNo = new KodeEntry() { Kodeverdi= "2.2", Kodebeskrivelse = "Er fallfare en risiko i det søknadspliktige tiltaket?", AnswerNoOutcome = SjekklisteAnswerOutcomeEnum.Ingen, AnswerYesOutcome = SjekklisteAnswerOutcomeEnum.Ingen }
            });
            _kodelisten.Add(new KodeEntry() { Kodeverdi = "10.1", Kodebeskrivelse = "sdfsjghhgjjhgadf", AnswerYesOutcome = SjekklisteAnswerOutcomeEnum.Dokumentasjon, AnswerNoOutcome = SjekklisteAnswerOutcomeEnum.Underpunkt, 
                            KodeEntryForNo = new KodeEntry() { Kodeverdi = "10.2", Kodebeskrivelse = "Er dagslys og utsyn et tema i det søknadspliktige tiltaket?", AnswerNoOutcome = SjekklisteAnswerOutcomeEnum.Ingen, AnswerYesOutcome = SjekklisteAnswerOutcomeEnum.Ingen }
            });
            _kodelisten.Add(new KodeEntry() { Kodeverdi = "10.3", Kodebeskrivelse = "dfggggg" });
            _kodelisten.Add(new KodeEntry() { Kodeverdi = "10.4", Kodebeskrivelse = "sassseee" });

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


            this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_14_sjekklistepunktsvar_utfylt, "sjekklistepunktsvar");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_14_sjekklistepunktsvar_gyldig, "sjekklistepunktsvar");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_14_dokumentasjon_utfylt, "dokumentasjon");

            this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_17_sjekklistepunktsvar_utfylt, "sjekklistepunktsvar");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_17_sjekklistepunktsvar_gyldig, "sjekklistepunktsvar");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_17_dokumentasjon_utfylt, "dokumentasjon");

            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_1_sjekklistepunktsvar_utfylt, "sjekklistepunktsvar");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_1_sjekklistepunktsvar_gyldig, "sjekklistepunktsvar");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_1_dokumentasjon_utfylt, "dokumentasjon");

            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_2_sjekklistepunktsvar_utfylt, "sjekklistepunktsvar");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_2_sjekklistepunktsvar_gyldig, "sjekklistepunktsvar");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_2_dokumentasjon_utfylt, "dokumentasjon");

            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_3_sjekklistepunktsvar_utfylt, "sjekklistepunktsvar");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_3_sjekklistepunktsvar_gyldig, "sjekklistepunktsvar");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_3_dokumentasjon_utfylt, "dokumentasjon");

            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_4_sjekklistepunktsvar_utfylt, "sjekklistepunktsvar");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_4_sjekklistepunktsvar_gyldig, "sjekklistepunktsvar");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_4_dokumentasjon_utfylt, "dokumentasjon");

            //AddValidationRules();
        }        
        
        public ValidationResult Validate(IEnumerable<SjekklistekravValidationEntity> sjekkliste)
        {
            try
            {

                //Checking if all sjekklistepunkter are present i data form
                var sjekklistepunktXPath = sjekkliste.First().ModelData.Sjekklistepunkt.DataModelXpath;
                foreach (var kodeEntry in _kodelisten)
                {
                    //kodeEntry.Kodeverdi
                    var kodeverdiFound = sjekkliste.Any(x => x.ModelData.Sjekklistepunkt.ModelData.Kodeverdi.Equals(kodeEntry.Kodeverdi));
                    var kodebeskrivelseFound = sjekkliste.Any(x => x.ModelData.Sjekklistepunkt.ModelData.Kodebeskrivelse.Equals(kodeEntry.Kodebeskrivelse));
                    if (!kodeverdiFound)
                    {
                        AddMessageFromRule(ValidationRuleEnum.utfylt, $"{sjekklistepunktXPath}/kodeverdi");
                    }

                    if (!kodebeskrivelseFound)
                    {
                        AddMessageFromRule(ValidationRuleEnum.utfylt, $"{sjekklistepunktXPath}/kodebeskrivelse");
                    }
                }


                //TODO:WIP
                foreach (var sjekklisteKrav in sjekkliste)
                {
                    //sjekklisteKrav.ModelData.Sjekklistepunkt.ModelData.Kodeverdi
                    var svar = sjekklisteKrav.ModelData.Sjekklistepunktsvar;
                    var dokumentasjon = sjekklisteKrav.ModelData.Dokumentasjon;

                    var sjekklistepunktValidationResult = _sjekklistepunktValidator.Validate(sjekklisteKrav.ModelData.Sjekklistepunkt);
                    UpdateValidationResultWithSubValidations(sjekklistepunktValidationResult);
                }


                if (Helpers.ObjectIsNullOrEmpty(sjekkliste) || sjekkliste.Count() == 0)
                {
                    //AddMessageFromRuleIfCollectionIsEmpty(ValidationRuleEnum.krav_utfylt);
                    AddMessageFromRule(ATILSjekklistekravEnum.utfylt);
                }
                else
                {
                    /* ====== 1.13 ======*/
                    if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "1.14"))
                    {
                        SjekklistepunktDokumentasjonFinnes(sjekkliste, "1.14");
                    }

                    //Validate 1.16 on form root level, towards arbeidsplasser/utleiebygg

                    /* ====== 10.1 ======*/
                    if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "10.1"))
                    {
                        if (SjekklistepunktBesvartMedJa(sjekkliste, "10.1"))
                        {
                            SjekklistepunktDokumentasjonFinnes(sjekkliste, "10.1");
                        }
                        else
                        {
                            SjekklistepunktFinnesOgErBesvart(sjekkliste, "10.2");
                        }
                    }

                    /* ====== 10.3 ======*/
                    if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "10.3"))
                    {
                        if (SjekklistepunktBesvartMedJa(sjekkliste, "10.3"))
                        {
                            SjekklistepunktDokumentasjonFinnes(sjekkliste, "10.3");
                        }
                        else
                        {
                            SjekklistepunktFinnesOgErBesvart(sjekkliste, "10.4");
                        }
                    }




                    //bool? svar_1_1 = ValiderSjekklistepunkt(sjekklistekrav, "1.13", false);
                    //bool? svar_1_2 = ValiderSjekklistepunkt(sjekklistekrav, "1.2", false);
                    //if (svar_1_2 != null && svar_1_2 == false)
                    {
                        //bool? svar_1_3 = ValiderSjekklistepunkt(sjekklistekrav, "1.3", false);
                    }
                }

                return _validationResult;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private bool? ValiderSjekklistepunkt(IEnumerable<SjekklistekravValidationEntity> kravliste, string sjekklistepunktnr, bool? detSomErFeilSvar)
        {
            if (SjekklistepunktFinnesOgErBesvart(kravliste, sjekklistepunktnr))
            {
                var kravEntity = kravliste.FirstOrDefault(x => x.ModelData.Sjekklistepunkt.ModelData.Kodeverdi.Equals(sjekklistepunktnr));
                var kravet = kravEntity.ModelData;

                if (kravet.Sjekklistepunktsvar == detSomErFeilSvar)
                {
                    AddMessageFromRule(ATILSjekklistekravEnum.pkt_10_1_dokumentasjon_utfylt, kravet.Sjekklistepunkt.DataModelXpath, new[] { kravet.Sjekklistepunkt.ModelData.Kodeverdi });
                }
                
                return (bool)kravet.Sjekklistepunktsvar;
            }

            return null;
        }

        private bool SjekklistepunktBesvartMedJa(IEnumerable<SjekklistekravValidationEntity> kravliste, string sjekklistepunktnr)
        {
            var kravEntity = kravliste.FirstOrDefault(x => x.ModelData.Sjekklistepunkt.ModelData.Kodeverdi.Equals(sjekklistepunktnr));
            var xPath = kravEntity.DataModelXpath;
            if (kravEntity != null)
            {
                if (kravEntity.ModelData.Sjekklistepunktsvar == true )
                {
                    return true;
                    //var enums = Helpers.GetSjekklistekravEnumFromIndex(sjekklistepunktnr);
                    //var sjekklistekravEnum = enums.First(x => Enum.GetName(typeof(SjekklistekravEnum), x).Contains("sjekklistepunktsvar_gyldig"));
                    //AddMessageFromRule(sjekklistekravEnum, xPath);
                }
                return false;
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
                    var enums = Helpers.GetSjekklistekravEnumFromIndex(sjekklistepunktnr);
                    var sjekklistekravEnum = enums.First(x => Enum.GetName(typeof(ATILSjekklistekravEnum), x).Contains("dokumentasjon_utfylt"));
                    AddMessageFromRule(sjekklistekravEnum, $"{xPath}/dokumentasjon");
                    return false;
                }
                return true;
            }
            else
            {
                throw new ArgumentNullException($"Checklist number {sjekklistepunktnr} doesn't exist.");
            }
        }


        private bool SjekklistepunktFinnesOgErBesvart(IEnumerable<SjekklistekravValidationEntity> kravliste, string sjekklistepunktnr)
        {
            var kravEntity = kravliste.FirstOrDefault(x => x.ModelData.Sjekklistepunkt.ModelData.Kodeverdi.Equals(sjekklistepunktnr));
            var xPath = kravliste.First().DataModelXpath;
            if (kravEntity == null)
            {
                AddMessageFromRule(ATILSjekklistekravEnum.kodeverdi_mangler, xPath.Replace("krav[0]", "krav[999]"), new[] { sjekklistepunktnr });
                return false;
            }
            else
            {
                var kravet = kravEntity.ModelData;
                var xPath2 = kravet.Sjekklistepunkt.DataModelXpath;
                if (kravet.Sjekklistepunktsvar == null)
                {
                    //TODO: THis condition will never arise, due to "Sjekklistepunktsvar" being nullable....
                    var enums = Helpers.GetSjekklistekravEnumFromIndex(sjekklistepunktnr);
                    var sjekklistekravEnum = enums.First(x => Enum.GetName(typeof(ATILSjekklistekravEnum), x).Contains("sjekklistepunktsvar_gyldig"));
                    AddMessageFromRule(sjekklistekravEnum, xPath);
                    return false;
                }

                //TODO: Maybe not: Use sjekklistepunktValidator to verfy correkt description from GeoNorge ?? Necessary ???
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
            this.AddValidationRule(ATILSjekklistekravEnum.utfylt);

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
    public class KodeEntry
    {
        public string Kodeverdi { get; set; }
        public string Kodebeskrivelse { get; set; }
        //public bool AnswerForUnderpoint { get; set; }
        public SjekklisteAnswerOutcomeEnum AnswerYesOutcome { get; set; }
        public SjekklisteAnswerOutcomeEnum AnswerNoOutcome { get; set; }

        public KodeEntry KodeEntryForYes { get; set; }
        public KodeEntry KodeEntryForNo { get; set; }
    }
}
