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

        protected override void InitializeValidationRules()
        {
            //this.AddValidationRule(ValidationRuleEnum.krav_utfylt);
            //this.AddValidationRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_utfylt, "sjekklistepunktsvar");
            //this.AddValidationRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_oppfylt, "sjekklistepunktsvar");
            //this.AddValidationRule(ValidationRuleEnum.krav_sjekklistekrav_dokumentasjon_utfylt, "dokumentasjon");
            //this.AddValidationRule(ValidationRuleEnum.krav_sjekklistekrav_dokumentasjon_utfylt, "dokumentasjon");
            
            this.AddValidationRule(SjekklistekravEnums.utfylt);
            this.AddValidationRule(SjekklistekravEnums.sjekklistepunkt_utfylt, "sjekklistepunkt");
            this.AddValidationRule(SjekklistekravEnums.sjekklistepunktSvar_utfylt, "sjekklistepunktsvar");
            this.AddValidationRule(SjekklistekravEnums.sjekklistepunktSvar_gyldig, "sjekklistepunktsvar");

        }        
        
        public ValidationResult Validate(IEnumerable<SjekklistekravValidationEntity> sjekklistekrav)
        {
            try
            {
                if (Helpers.ObjectIsNullOrEmpty(sjekklistekrav) || sjekklistekrav.Count() == 0)
                {
                    //AddMessageFromRuleIfCollectionIsEmpty(ValidationRuleEnum.krav_utfylt);
                    AddMessageFromRule(SjekklistekravEnums.utfylt);
                }
                else
                {
                    bool? svar_1_1 = ValiderSjekklistepunkt(sjekklistekrav, "1.1", false);
                    bool? svar_1_2 = ValiderSjekklistepunkt(sjekklistekrav, "1.2", false);
                    if (svar_1_2 != null && svar_1_2 == false)
                    {
                        bool? svar_1_3 = ValiderSjekklistepunkt(sjekklistekrav, "1.3", false);
                        bool? svar_1_4 = ValiderSjekklistepunkt(sjekklistekrav, "1.4", false);
                        bool? svar_1_5 = ValiderSjekklistepunkt(sjekklistekrav, "1.5", false);
                        bool? svar_1_6 = ValiderSjekklistepunkt(sjekklistekrav, "1.6", false);
                        bool? svar_1_7 = ValiderSjekklistepunkt(sjekklistekrav, "1.7", false);
                        bool? svar_1_8 = ValiderSjekklistepunkt(sjekklistekrav, "1.8", false);

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
                    AddMessageFromRule(SjekklistekravEnums.sjekklistepunktSvar_gyldig, kravet.Sjekklistepunkt.DataModelXpath, new[] { kravet.Sjekklistepunkt.ModelData.Kodeverdi });
                }
                
                return (bool)kravet.Sjekklistepunktsvar;
            }

            return null;
        }


        private bool SjekklistepunktFinnesOgErBesvart(IEnumerable<SjekklistekravValidationEntity> kravliste, string sjekklistepunktnr)
        {


            var kravEntity = kravliste.FirstOrDefault(x => x.ModelData.Sjekklistepunkt.ModelData.Kodeverdi.Equals(sjekklistepunktnr));
            var xPath = kravliste.First().ModelData.Sjekklistepunkt.DataModelXpath;
            if (kravEntity == null)
            {
                AddMessageFromRule(SjekklistekravEnums.sjekklistepunkt_utfylt, xPath.Replace("krav[0]", "krav[999]"), new[] { sjekklistepunktnr });
                return false;
            }
            else
            {
                var kravet = kravEntity.ModelData;
                var xPath2 = kravet.Sjekklistepunkt.DataModelXpath;
                if (kravet.Sjekklistepunktsvar == null)
                {
                    AddMessageFromRule(SjekklistekravEnums.sjekklistepunktSvar_utfylt, xPath2, new[] { sjekklistepunktnr });
                }

                //TODO: Use sjekklistepunktValidator to verfy correkt description from GeoNorge ?? Necessary ???
                //Here......
            }

            return true;
        }

        public void ValidateEntityFields(SjekklistekravValidationEntity sjekklistekravValidationEntity)
        {
            var xpath = sjekklistekravValidationEntity.DataModelXpath;
            var sjekklistekrav = sjekklistekravValidationEntity.ModelData;

            if (Helpers.ObjectIsNullOrEmpty(sjekklistekrav.Sjekklistepunktsvar))
            {
                AddMessageFromRule(SjekklistekravEnums.sjekklistepunktSvar_utfylt, xpath, new [] { sjekklistekrav.Sjekklistepunkt.ModelData.Kodeverdi });
            }



            if (!sjekklistekrav.Sjekklistepunktsvar == true)
            {
                AddMessageFromRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_oppfylt, xpath, new[] { sjekklistekrav.Sjekklistepunkt.ModelData.Kodeverdi });
            }
            if (string.IsNullOrEmpty(sjekklistekrav.Dokumentasjon))
            {
                AddMessageFromRule(ValidationRuleEnum.krav_sjekklistekrav_dokumentasjon_utfylt, xpath);
            }

        }

    }
}
