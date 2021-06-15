using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class TiltakstypeValidator : KodelisteValidator
    {
        public override string ruleXmlElement { get { return "type"; } set { ruleXmlElement = value; } }

        public TiltakstypeValidator(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum parentValidator, ICodeListService codeListService)
            : base(formValidatorConfiguration, parentValidator, codeListService)
        {
            _codeListService = codeListService;
        }


        //IEnumerable
        public ValidationResult Validate(IEnumerable<KodelisteValidationEntity> modeldata)
        {
            if (Helpers.ObjectIsNullOrEmpty(modeldata) || modeldata.Count() == 0)
            {
                AddMessageFromRuleIfCollectionIsEmpty(ValidationRuleEnum.beskrivelseAvTiltak_tiltakstype_kode_utfylt);
            }
            else
            {
                foreach (var tiltakstype in modeldata)
                {
                    ValidateEntityFields(tiltakstype);
                }
            }

            return _validationResult;
        }
        private void ValidateEntityFields(KodelisteValidationEntity tiltakstypeValidationEntity)
        {
            var xPath = tiltakstypeValidationEntity.DataModelXpath;

            if (string.IsNullOrEmpty(tiltakstypeValidationEntity.ModelData.Kodeverdi))
            {
                AddMessageFromRule(ValidationRuleEnum.beskrivelseAvTiltak_tiltakstype_kode_utfylt, xPath);
            }
            else
            {
                ValidateCodeFromRegister(tiltakstypeValidationEntity);
            }
            
            if (string.IsNullOrEmpty(tiltakstypeValidationEntity.ModelData.Kodebeskrivelse))
                AddMessageFromRule(ValidationRuleEnum.beskrivelseAvTiltak_tiltakstype_beskrivelse_utfylt, xPath);


        }

        private void ValidateCodeFromRegister(KodelisteValidationEntity tiltakstypeValidationEntity)
        {
            if (tiltakstypeValidationEntity.ModelData.Kodeverdi != "XX")
                AddMessageFromRule(ValidationRuleEnum.beskrivelseAvTiltak_tiltakstype_gyldig_kode, tiltakstypeValidationEntity.DataModelXpath, new List<string>() { tiltakstypeValidationEntity.ModelData.Kodeverdi });

        }
    }
}
