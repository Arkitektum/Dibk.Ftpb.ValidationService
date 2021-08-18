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

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class ATILSjekklistekravValidator : EntityValidatorBase, ISjekklistekravValidator
    {
        private readonly ICodeListService _codeListService;

        //private readonly ISjekklistepunktValidator _sjekklistepunktValidator;
        private readonly IKodelisteValidator _sjekklistepunktValidator; 
        private List<KodeEntry> _kodelisten;

        public ValidationResult ValidationResult { get => _validationResult; }

        public ATILSjekklistekravValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeid, IKodelisteValidator sjekklistepunktValidator, ICodeListService codeListService)
            : base(entityValidatorTree, nodeid)
        {
            _codeListService = codeListService;
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
            _kodelisten.Add(new KodeEntry() { Kodeverdi = "1.14" });
            _kodelisten.Add(new KodeEntry() { Kodeverdi = "1.17" });
            _kodelisten.Add(new KodeEntry() { Kodeverdi = "2.1", AnswerNoOutcome = SjekklisteAnswerOutcomeEnum.Underpunkt, AnswerYesOutcome = SjekklisteAnswerOutcomeEnum.Dokumentasjon,
                             KodeEntryForNo = new KodeEntry() { Kodeverdi= "2.2", AnswerNoOutcome = SjekklisteAnswerOutcomeEnum.Ingen, AnswerYesOutcome = SjekklisteAnswerOutcomeEnum.Ingen }
            });
            _kodelisten.Add(new KodeEntry() { Kodeverdi = "10.1", AnswerYesOutcome = SjekklisteAnswerOutcomeEnum.Dokumentasjon, AnswerNoOutcome = SjekklisteAnswerOutcomeEnum.Underpunkt, 
                            KodeEntryForNo = new KodeEntry() { Kodeverdi = "10.2", AnswerNoOutcome = SjekklisteAnswerOutcomeEnum.Ingen, AnswerYesOutcome = SjekklisteAnswerOutcomeEnum.Ingen }
            });
            _kodelisten.Add(new KodeEntry() { Kodeverdi = "10.3" });
            _kodelisten.Add(new KodeEntry() { Kodeverdi = "10.4" });

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

                ////Checking if all sjekklistepunkter are present i data form
                //var sjekklistepunktXPath = sjekkliste.First().ModelData.Sjekklistepunkt.DataModelXpath;
                //foreach (var kodeEntry in _kodelisten)
                //{
                //    //kodeEntry.Kodeverdi
                //    var kodeverdiFound = sjekkliste.Any(x => x.ModelData.Sjekklistepunkt.ModelData.Kodeverdi.Equals(kodeEntry.Kodeverdi));
                //    var kodebeskrivelseFound = sjekkliste.Any(x => x.ModelData.Sjekklistepunkt.ModelData.Kodeverdi.Equals(kodeEntry.Kodeverdi) && !string.IsNullOrEmpty(x.ModelData.Sjekklistepunkt.ModelData.Kodebeskrivelse));
                //    if (!kodeverdiFound)
                //    {
                //        AddMessageFromRule(ATILSjekklistekravEnum.kodeverdi_mangler, $"{sjekklistepunktXPath}/kodeverdi", new[] { kodeEntry.Kodeverdi });
                //    }

                //    if (!kodebeskrivelseFound)
                //    {
                //        AddMessageFromRule(ATILSjekklistekravEnum.kodevbeskrivelse_mangler, $"{sjekklistepunktXPath}/kodebeskrivelse", new[] { kodeEntry.Kodeverdi });
                //    }
                //}


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

                    //Validates 1.17 in Arbeidsplasser validator


                    //************************************************************************************************************

                    //var questionPair = new List<Tuple<string, string>>();
                    //questionPair.Add(Tuple.Create("2.1", "2.2"));
                    //questionPair.Add(Tuple.Create("2.3", "2.4"));
                    //questionPair.Add(Tuple.Create("2.5", "2.6"));
                    //questionPair.Add(Tuple.Create("2.7", "2.8"));
                    //questionPair.Add(Tuple.Create("2.9", "2.10"));

                    //questionPair.Add(Tuple.Create("3.1", "3.2"));
                    //questionPair.Add(Tuple.Create("3.3", "3.4"));

                    //questionPair.Add(Tuple.Create("4.1", "4.2"));
                    //questionPair.Add(Tuple.Create("4.3", "4.4"));

                    //questionPair.Add(Tuple.Create("5.1", "5.2"));
                    //questionPair.Add(Tuple.Create("5.3", "5.4"));
                    //questionPair.Add(Tuple.Create("5.5", "5.6"));
                    //questionPair.Add(Tuple.Create("5.7", "5.8"));

                    //questionPair.Add(Tuple.Create("6.1", "6.2"));

                    //questionPair.Add(Tuple.Create("7.1", "7.2"));
                    //questionPair.Add(Tuple.Create("7.3", "7.4"));

                    //questionPair.Add(Tuple.Create("8.1", "8.2"));
                    //questionPair.Add(Tuple.Create("8.3", "8.4"));

                    //questionPair.Add(Tuple.Create("9.1", "9.2"));

                    //questionPair.Add(Tuple.Create("10.1", "10.2"));
                    //questionPair.Add(Tuple.Create("10.3", "10.4"));

                    //questionPair.Add(Tuple.Create("11.10", "11.11"));

                    var newList = new List<List<string>>();
                    newList.Add(new List<string>() { "2.1", "2.2" });
                    newList.Add(new List<string>() { "2.3", "2.4" });
                    newList.Add(new List<string>() { "2.5", "2.6" });
                    newList.Add(new List<string>() { "2.7", "2.8" });
                    newList.Add(new List<string>() { "2.9", "2.10" });
                                                                 
                    newList.Add(new List<string>() { "3.1", "3.2" });
                    newList.Add(new List<string>() { "3.3", "3.4" });
                                                                 
                    newList.Add(new List<string>() { "4.1", "4.2" });
                    newList.Add(new List<string>() { "4.3", "4.4" });
                                                                 
                    newList.Add(new List<string>() { "5.1", "5.2" });
                    newList.Add(new List<string>() { "5.3", "5.4" });
                    newList.Add(new List<string>() { "5.5", "5.6" });
                    newList.Add(new List<string>() { "5.7", "5.8" });
                                                                 
                    newList.Add(new List<string>() { "6.1", "6.2" });
                                                                
                    newList.Add(new List<string>() { "7.1", "7.2" });
                    newList.Add(new List<string>() { "7.3", "7.4" });
                                                                 
                    newList.Add(new List<string>() { "8.1", "8.2" });
                    newList.Add(new List<string>() { "8.3", "8.4" });
                                                                 
                    newList.Add(new List<string>() { "9.1", "9.2" });
                                                     
                    newList.Add(new List<string>() { "10.1", "10.2" });
                    newList.Add(new List<string>() { "10.3", "10.4" });
                                                     
                    newList.Add(new List<string>() { "11.10", "11.11" });

                    newList.Add(new List<string>() { "11.1", "11.2", "11.3" });
                    newList.Add(new List<string>() { "11.4", "11.5", "11.6" });
                    newList.Add(new List<string>() { "11.7", "11.8", "11.9" });
                    newList.Add(new List<string>() { "11.12", "11.13", "11.14" });
                    
                    newList.Add(new List<string>() { "11.15", "11.16", "11.17", "11.18" });



                    int i = 1;
                    foreach (var sjekklisteNummer in newList)
                    {
                        DoTheStuffRec(sjekkliste, sjekklisteNummer, 0);
                    }

                    //var questionTriplet = new List<(string, string, string)>();
                    //questionTriplet.Add(("11.1", "11.2", "11.3"));
                    //questionTriplet.Add(("11.4", "11.5", "11.6"));
                    //questionTriplet.Add(("11.7", "11.8", "11.9"));
                    //questionTriplet.Add(("11.12", "11.13", "11.14"));

                    //foreach (var triplet in questionTriplet)
                    //{
                    //    DoTheStuffTriplet(sjekkliste, triplet.Item1, triplet.Item2, triplet.Item3);
                    //}
                    

                    //var questionQuadruple = new List<(string, string, string, string)>();
                    //questionQuadruple.Add(("11.15", "11.16", "11.17", "11.18"));

                    //foreach (var quad in questionQuadruple)
                    //{
                    //    DoTheStuffQuadruple(sjekkliste, quad.Item1, quad.Item2, quad.Item3, quad.Item4);
                    //}
                    

                    #region
                    ///* ====== 2.1 ======*/
                    //if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "2.1"))
                    //{
                    //    if (SjekklistepunktBesvartMedJa(sjekkliste, "2.1"))
                    //    {
                    //        SjekklistepunktDokumentasjonFinnes(sjekkliste, "2.1");
                    //    }
                    //    else
                    //    {
                    //        SjekklistepunktFinnesOgErBesvart(sjekkliste, "2.2");
                    //    }
                    //}

                    ///* ====== 2.3 ======*/
                    //if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "2.3"))
                    //{
                    //    if (SjekklistepunktBesvartMedJa(sjekkliste, "2.3"))
                    //    {
                    //        SjekklistepunktDokumentasjonFinnes(sjekkliste, "2.3");
                    //    }
                    //    else
                    //    {
                    //        SjekklistepunktFinnesOgErBesvart(sjekkliste, "2.4");
                    //    }
                    //}


                    ///* ====== 2.5 ======*/
                    //if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "2.5"))
                    //{
                    //    if (SjekklistepunktBesvartMedJa(sjekkliste, "2.5"))
                    //    {
                    //        SjekklistepunktDokumentasjonFinnes(sjekkliste, "2.5");
                    //    }
                    //    else
                    //    {
                    //        SjekklistepunktFinnesOgErBesvart(sjekkliste, "2.6");
                    //    }
                    //}


                    ///* ====== 2.7 ======*/
                    //if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "2.7"))
                    //{
                    //    if (SjekklistepunktBesvartMedJa(sjekkliste, "2.7"))
                    //    {
                    //        SjekklistepunktDokumentasjonFinnes(sjekkliste, "2.7");
                    //    }
                    //    else
                    //    {
                    //        SjekklistepunktFinnesOgErBesvart(sjekkliste, "2.8");
                    //    }
                    //}

                    ///* ====== 2.9 ======*/
                    //if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "2.9"))
                    //{
                    //    if (SjekklistepunktBesvartMedJa(sjekkliste, "2.9"))
                    //    {
                    //        SjekklistepunktDokumentasjonFinnes(sjekkliste, "2.9");
                    //    }
                    //    else
                    //    {
                    //        SjekklistepunktFinnesOgErBesvart(sjekkliste, "2.10");
                    //    }
                    //}


                    //************************************************************************************************************

                    //            /* ====== 3.1 ======*/
                    //            if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "3.1"))
                    //            {
                    //                if (SjekklistepunktBesvartMedJa(sjekkliste, "3.1"))
                    //                {
                    //                    SjekklistepunktDokumentasjonFinnes(sjekkliste, "3.1");
                    //                }
                    //                else
                    //                {
                    //                    SjekklistepunktFinnesOgErBesvart(sjekkliste, "3.2");
                    //                }
                    //            }


                    //            /* ====== 3.3 ======*/
                    //            if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "3.3"))
                    //            {
                    //                if (SjekklistepunktBesvartMedJa(sjekkliste, "3.3"))
                    //                {
                    //                    SjekklistepunktDokumentasjonFinnes(sjekkliste, "3.3");
                    //                }
                    //                else
                    //                {
                    //                    SjekklistepunktFinnesOgErBesvart(sjekkliste, "3.4");
                    //                }
                    //            }

                    ////************************************************************************************************************

                    //            /* ====== 4.1 ======*/
                    //            if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "4.1"))
                    //            {
                    //                if (SjekklistepunktBesvartMedJa(sjekkliste, "4.1"))
                    //                {
                    //                    SjekklistepunktDokumentasjonFinnes(sjekkliste, "4.1");
                    //                }
                    //                else
                    //                {
                    //                    SjekklistepunktFinnesOgErBesvart(sjekkliste, "4.2");
                    //                }
                    //            }


                    //            /* ====== 4.3 ======*/
                    //            if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "4.3"))
                    //            {
                    //                if (SjekklistepunktBesvartMedJa(sjekkliste, "4.3"))
                    //                {
                    //                    SjekklistepunktDokumentasjonFinnes(sjekkliste, "4.3");
                    //                }
                    //                else
                    //                {
                    //                    SjekklistepunktFinnesOgErBesvart(sjekkliste, "4.4");
                    //                }
                    //            }

                    //            //************************************************************************************************************

                    //            /* ====== 5.1 ======*/
                    //            if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "5.1"))
                    //            {
                    //                if (SjekklistepunktBesvartMedJa(sjekkliste, "5.1"))
                    //                {
                    //                    SjekklistepunktDokumentasjonFinnes(sjekkliste, "5.1");
                    //                }
                    //                else
                    //                {
                    //                    SjekklistepunktFinnesOgErBesvart(sjekkliste, "5.2");
                    //                }
                    //            }

                    //            /* ====== 5.3 ======*/
                    //            if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "5.3"))
                    //            {
                    //                if (SjekklistepunktBesvartMedJa(sjekkliste, "5.3"))
                    //                {
                    //                    SjekklistepunktDokumentasjonFinnes(sjekkliste, "5.3");
                    //                }
                    //                else
                    //                {
                    //                    SjekklistepunktFinnesOgErBesvart(sjekkliste, "5.4");
                    //                }
                    //            }


                    //            /* ====== 5.5 ======*/
                    //            if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "5.5"))
                    //            {
                    //                if (SjekklistepunktBesvartMedJa(sjekkliste, "5.5"))
                    //                {
                    //                    SjekklistepunktDokumentasjonFinnes(sjekkliste, "5.5");
                    //                }
                    //                else
                    //                {
                    //                    SjekklistepunktFinnesOgErBesvart(sjekkliste, "5.6");
                    //                }
                    //            }


                    //            /* ====== 5.7 ======*/
                    //            if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "5.7"))
                    //            {
                    //                if (SjekklistepunktBesvartMedJa(sjekkliste, "5.7"))
                    //                {
                    //                    SjekklistepunktDokumentasjonFinnes(sjekkliste, "5.7");
                    //                }
                    //                else
                    //                {
                    //                    SjekklistepunktFinnesOgErBesvart(sjekkliste, "5.8");
                    //                }
                    //            }


                    //************************************************************************************************************



                    ///* ====== 10.1 ======*/
                    //if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "10.1"))
                    //{
                    //    if (SjekklistepunktBesvartMedJa(sjekkliste, "10.1"))
                    //    {
                    //        SjekklistepunktDokumentasjonFinnes(sjekkliste, "10.1");
                    //    }
                    //    else
                    //    {
                    //        SjekklistepunktFinnesOgErBesvart(sjekkliste, "10.2");
                    //    }
                    //}

                    ///* ====== 10.3 ======*/
                    //if (SjekklistepunktFinnesOgErBesvart(sjekkliste, "10.3"))
                    //{
                    //    if (SjekklistepunktBesvartMedJa(sjekkliste, "10.3"))
                    //    {
                    //        SjekklistepunktDokumentasjonFinnes(sjekkliste, "10.3");
                    //    }
                    //    else
                    //    {
                    //        SjekklistepunktFinnesOgErBesvart(sjekkliste, "10.4");
                    //    }
                    //}
                    #endregion
                }

                return _validationResult;
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        private void DoTheStuffRec(IEnumerable<SjekklistekravValidationEntity> sjekkliste, List<string> sjekkpunktNr, int currentElement)
        {
            string currentSjekkpunktNr = sjekkpunktNr[currentElement];

            if (SjekklistepunktFinnesOgErBesvart(sjekkliste, currentSjekkpunktNr))
            {
                //If this is last element in list, end processing here
                if (currentElement + 1 == sjekkpunktNr.Count)
                {
                    //End processing
                }
                else
                {
                    if (SjekklistepunktBesvartMedJa(sjekkliste, currentSjekkpunktNr))
                    {
                        SjekklistepunktDokumentasjonFinnes(sjekkliste, currentSjekkpunktNr);
                    }
                    else
                    {
                        //Verify that sub-question exists when answer is "No"
                        if (currentElement + 2 <= sjekkpunktNr.Count)       //currentElement + 2: Adding 2 due to zero-indexed list and adding 1 to get next element
                        {
                            //SjekklistepunktFinnesOgErBesvart(sjekkliste, sjekkpunktNr[currentElement+1]);
                            DoTheStuffRec(sjekkliste, sjekkpunktNr, currentElement + 1);
                        }
                    }
                }
            }
        }

        #region commented
        //private void DoTheStuff(IEnumerable<SjekklistekravValidationEntity> sjekkliste, string mainQuestion, string followupQuestion)
        //{
        //    if (SjekklistepunktFinnesOgErBesvart(sjekkliste, mainQuestion))
        //    {
        //        if (SjekklistepunktBesvartMedJa(sjekkliste, mainQuestion))
        //        {
        //            SjekklistepunktDokumentasjonFinnes(sjekkliste, mainQuestion);
        //        }
        //        else
        //        {
        //            SjekklistepunktFinnesOgErBesvart(sjekkliste, followupQuestion);
        //        }
        //    }
        //}

        //private void DoTheStuffTriplet(IEnumerable<SjekklistekravValidationEntity> sjekkliste, string mainQuestion, string followupQuestion, string followupQuestion2)
        //{
        //    if (SjekklistepunktFinnesOgErBesvart(sjekkliste, mainQuestion))
        //    {
        //        if (SjekklistepunktBesvartMedJa(sjekkliste, mainQuestion))
        //        {
        //            SjekklistepunktDokumentasjonFinnes(sjekkliste, mainQuestion);
        //        }
        //        else
        //        {
        //            if (SjekklistepunktFinnesOgErBesvart(sjekkliste, followupQuestion))
        //            {
        //                if (SjekklistepunktBesvartMedJa(sjekkliste, followupQuestion))
        //                {
        //                    SjekklistepunktDokumentasjonFinnes(sjekkliste, followupQuestion);
        //                }
        //                else
        //                {
        //                    SjekklistepunktFinnesOgErBesvart(sjekkliste, followupQuestion2);
        //                }
        //            }
        //        }
        //    }
        //}

        //private void DoTheStuffQuadruple(IEnumerable<SjekklistekravValidationEntity> sjekkliste, string mainQuestion, string followupQuestion, string followupQuestion2, string followupQuestion3)
        //{
        //    if (SjekklistepunktFinnesOgErBesvart(sjekkliste, mainQuestion))
        //    {
        //        if (SjekklistepunktBesvartMedJa(sjekkliste, mainQuestion))
        //        {
        //            SjekklistepunktDokumentasjonFinnes(sjekkliste, mainQuestion);
        //        }
        //        else
        //        {
        //            if (SjekklistepunktFinnesOgErBesvart(sjekkliste, followupQuestion))
        //            {
        //                if (SjekklistepunktBesvartMedJa(sjekkliste, followupQuestion))
        //                {
        //                    SjekklistepunktDokumentasjonFinnes(sjekkliste, followupQuestion);
        //                }
        //                else
        //                {
        //                    if (SjekklistepunktFinnesOgErBesvart(sjekkliste, followupQuestion2))
        //                    {
        //                        if (SjekklistepunktBesvartMedJa(sjekkliste, followupQuestion2))
        //                        {
        //                            SjekklistepunktDokumentasjonFinnes(sjekkliste, followupQuestion2);
        //                        }
        //                        else
        //                        {
        //                            SjekklistepunktFinnesOgErBesvart(sjekkliste, followupQuestion3);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        #endregion


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
                AddMessageFromRule(ValidationRuleEnum.sjekklistepunkt_mangler, xPath.Replace("krav[0]", "krav[]") + "/sjekklistepunkt/kodeverdi", new[] { sjekklistepunktnr });
                return false;
            }
            else
            {
                var kravet = kravEntity.ModelData;
                var xPath2 = kravet.Sjekklistepunkt.DataModelXpath;
                if (kravet.Sjekklistepunktsvar == null)
                {
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
        //public string Kodebeskrivelse { get; set; }
        public SjekklisteAnswerOutcomeEnum AnswerYesOutcome { get; set; }
        public SjekklisteAnswerOutcomeEnum AnswerNoOutcome { get; set; }

        public KodeEntry KodeEntryForYes { get; set; }
        public KodeEntry KodeEntryForNo { get; set; }
    }
}
