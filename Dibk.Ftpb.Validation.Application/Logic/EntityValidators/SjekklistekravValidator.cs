using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class SjekklistekravValidator : EntityValidatorBase, ISjekklistekravValidator
    {
        //private readonly ISjekklistepunktValidator _sjekklistepunktValidator;
        private readonly IKodelisteValidator _sjekklistepunktValidator;

        public ValidationResult ValidationResult { get => _validationResult; }

        public SjekklistekravValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeid, IKodelisteValidator sjekklistepunktValidator)
            : base(entityValidatorTree, nodeid)
        {
            _sjekklistepunktValidator = sjekklistepunktValidator;
        }
        public SjekklistekravValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeid)
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
            this.AddValidationRule(SjekklistekravEnum.kodeverdi_mangler, "sjekklistepunkt/kodeverdi");

            List<string> checklistNumbers = new List<string>() { "1.13", "1.16", "10.1" };

            foreach (var checklistNumber in checklistNumbers)
            {
                var enums = Helpers.GetSjekklistekravEnumFromIndex(checklistNumber);
                //var xx = enums.First(x => Enum.GetName(typeof(SjekklistekravEnum), x).Contains("kodeverdi_utfylt"));

                this.AddValidationRule(enums.First(x => Enum.GetName(typeof(SjekklistekravEnum), x).Contains("kodeverdi_utfylt")), "sjekklistepunkt/kodeverdi");
                this.AddValidationRule(enums.First(x => Enum.GetName(typeof(SjekklistekravEnum), x).Contains("kodebeskrivelse_gyldig")), "sjekklistepunkt/kodebeskrivelse");
                this.AddValidationRule(enums.First(x => Enum.GetName(typeof(SjekklistekravEnum), x).Contains("sjekklistepunktsvar_gyldig")), "sjekklistepunktsvar");
                this.AddValidationRule(enums.First(x => Enum.GetName(typeof(SjekklistekravEnum), x).Contains("dokumentasjon_utfylt")), "dokumentasjon");

            }
            //AddValidationRules();
        }        
        
        public ValidationResult Validate(IEnumerable<SjekklistekravValidationEntity> sjekklistekrav)
        {
            try
            {
                if (Helpers.ObjectIsNullOrEmpty(sjekklistekrav) || sjekklistekrav.Count() == 0)
                {
                    //AddMessageFromRuleIfCollectionIsEmpty(ValidationRuleEnum.krav_utfylt);
                    AddMessageFromRule(SjekklistekravEnum.utfylt);
                }
                else
                {
                    /* ====== 1.13 ======*/
                    if (SjekklistepunktFinnesOgErBesvart(sjekklistekrav, "1.13"))
                    {
                        SjekklistepunktDokumentasjonFinnes(sjekklistekrav, "1.13");
                    }

                    //Validate 1.16 on form root level, towards arbeidsplasser/utleiebygg

                    /* ====== 10.1 ======*/
                    if (SjekklistepunktFinnesOgErBesvart(sjekklistekrav, "10.1"))
                    {
                        if (SjekklistepunktBesvartMedJa(sjekklistekrav, "10.1"))
                        {
                            SjekklistepunktDokumentasjonFinnes(sjekklistekrav, "10.1");
                        }
                        else
                        {
                            SjekklistepunktFinnesOgErBesvart(sjekklistekrav, "10.2");
                        }
                    }

                    /* ====== 10.3 ======*/
                    if (SjekklistepunktFinnesOgErBesvart(sjekklistekrav, "10.3"))
                    {
                        if (SjekklistepunktBesvartMedJa(sjekklistekrav, "10.3"))
                        {
                            SjekklistepunktDokumentasjonFinnes(sjekklistekrav, "10.3");
                        }
                        else
                        {
                            SjekklistepunktFinnesOgErBesvart(sjekklistekrav, "10.4");
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
                    AddMessageFromRule(SjekklistekravEnum.pkt_10_1_dokumentasjon_utfylt, kravet.Sjekklistepunkt.DataModelXpath, new[] { kravet.Sjekklistepunkt.ModelData.Kodeverdi });
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
                    var sjekklistekravEnum = enums.First(x => Enum.GetName(typeof(SjekklistekravEnum), x).Contains("dokumentasjon_utfylt"));
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
                AddMessageFromRule(SjekklistekravEnum.kodeverdi_mangler, xPath.Replace("krav[0]", "krav[999]"), new[] { sjekklistepunktnr });
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
                    var sjekklistekravEnum = enums.First(x => Enum.GetName(typeof(SjekklistekravEnum), x).Contains("sjekklistepunktsvar_gyldig"));
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
            this.AddValidationRule(SjekklistekravEnum.utfylt);

            List<string> checklistNumbers = new List<string>() {"1.13", "1.16", "10.1" };

            foreach (var checklistNumber in checklistNumbers)
            {
                var enums = Helpers.GetSjekklistekravEnumFromIndex(checklistNumber);
                //var xx = enums.First(x => Enum.GetName(typeof(SjekklistekravEnum), x).Contains("kodeverdi_utfylt"));

                this.AddValidationRule(enums.First(x => Enum.GetName(typeof(SjekklistekravEnum), x).Contains("kodeverdi_utfylt")), "sjekklistepunkt/kodeverdi");
                this.AddValidationRule(enums.First(x => Enum.GetName(typeof(SjekklistekravEnum), x).Contains("kodebeskrivelse_gyldig")), "kodebeskrivelse");
                this.AddValidationRule(enums.First(x => Enum.GetName(typeof(SjekklistekravEnum), x).Contains("sjekklistepunktsvar_gyldig")), "sjekklistepunktsvar");
                this.AddValidationRule(enums.First(x => Enum.GetName(typeof(SjekklistekravEnum), x).Contains("dokumentasjon_utfylt")), "dokumentasjon");

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
}
