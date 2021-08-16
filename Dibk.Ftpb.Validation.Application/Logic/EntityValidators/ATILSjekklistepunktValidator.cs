using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class ATILSjekklistepunktValidator : KodelisteValidator
    {
        public ATILSjekklistepunktValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, ICodeListService codeListService)
            : base(entityValidatorTree, nodeId, FtbKodeListeEnum.Sjekklistepunkttype, RegistryType.Arbeidstilsynet, codeListService)
        {
            _codeListService = codeListService;
        }

        protected override void InitializeValidationRules()
        {
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_14_kodeverdi_utfylt, "kodeverdi");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_14_kodeverdi_gyldig, "kodeverdi");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_14_kodebeskrivelse_utfylt, "kodebeskrivelse");

            this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_17_kodeverdi_utfylt, "kodeverdi");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_17_kodeverdi_gyldig, "kodeverdi");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_1_17_kodebeskrivelse_utfylt, "kodebeskrivelse");

            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_1_kodeverdi_utfylt, "kodeverdi");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_1_kodeverdi_gyldig, "kodeverdi");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_1_kodebeskrivelse_utfylt, "kodebeskrivelse");

            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_2_kodeverdi_utfylt, "kodeverdi");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_2_kodeverdi_gyldig, "kodeverdi");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_2_kodebeskrivelse_utfylt, "kodebeskrivelse");

            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_3_kodeverdi_utfylt, "kodeverdi");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_3_kodeverdi_gyldig, "kodeverdi");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_3_kodebeskrivelse_utfylt, "kodebeskrivelse");

            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_4_kodeverdi_utfylt, "kodeverdi");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_4_kodeverdi_gyldig, "kodeverdi");
            this.AddValidationRule(ATILSjekklistekravEnum.pkt_10_4_kodebeskrivelse_utfylt, "kodebeskrivelse");



        }

        //public ValidationResult Validate(KodelisteValidationEntity kodeEntry)
        //{
        //    base.Validate(kodeEntry);



        //    kodeEntry.ModelData.Kodeverdi

        //    base.ResetValidationMessages();

        //    var xpath = kodeliste.DataModelXpath;

        //    if (Helpers.ObjectIsNullOrEmpty(kodeliste?.ModelData))
        //    {
        //        //AddMessageFromRule(KodeListValidationEnum.utfylt, xpath);
        //        AddMessageFromRule(ValidationRuleEnum.utfylt, xpath);
        //    }
        //    else
        //    {
        //        if (Helpers.ObjectIsNullOrEmpty(kodeliste.ModelData.Kodeverdi))
        //        {
        //            //AddMessageFromRule(KodeListValidationEnum.kodeverdi_utfylt, xpath);
        //            AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xpath}/kodeverdi");
        //        }
        //        else
        //        {
        //            var isCodeValid = _codeListService.IsCodelistValid(FtbKodeListeEnum.Sjekklistepunkttype, kodeliste.ModelData?.Kodeverdi, RegistryType.Arbeidstilsynet);
        //            if (!isCodeValid.HasValue)
        //            {
        //                //AddMessageFromRule(KodeListValidationEnum.kode_KanIkkeValidere, xpath);
        //                AddMessageFromRule(ValidationRuleEnum.kodeliste_gyldig, xpath);
        //            }
        //            else
        //            {
        //                if (!isCodeValid.GetValueOrDefault())
        //                {
        //                    //AddMessageFromRule(KodeListValidationEnum.kodeverdi_gyldig, xpath, new[] { kodeliste.ModelData?.Kodeverdi });
        //                    AddMessageFromRule(ValidationRuleEnum.gyldig, xpath, new[] { kodeliste.ModelData?.Kodeverdi });
        //                }
        //            }
        //        }
        //    }

        //    return ValidationResult;
        //}
    }


}
