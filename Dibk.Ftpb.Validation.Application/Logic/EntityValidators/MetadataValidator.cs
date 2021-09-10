﻿using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class MetadataValidator : EntityValidatorBase, IMetadataValidator
    {
        ValidationResult IMetadataValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public MetadataValidator(IList<EntityValidatorNode> entityValidatorTree)
            : base(entityValidatorTree)
        {
        }
        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.fraSluttbrukersystem);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.erNorskSvenskDansk);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.unntattOffentlighet);
            
            //Usikkert om skal valideres
            //AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.ftbId);
            //AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.prosjektnavn);
            //AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.sluttbrukersystemUrl);
            //AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.hovedinnsendingsnummer);
            //AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.klartForSigneringFraSluttbrukersystem);


        }

        public ValidationResult Validate(MetadataValidationEntity metadata)
        {
            base.ResetValidationMessages();
            var xPath = metadata.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(metadata?.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, xPath);

            }
            else
            {
                ValidateEntityFields(metadata);
            }



            return _validationResult;
        }

        public void ValidateEntityFields(MetadataValidationEntity metadata)
        {

            var xPath = metadata.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(metadata.ModelData.ErNorskSvenskDansk))
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.erNorskSvenskDansk}");

            if (Helpers.ObjectIsNullOrEmpty(metadata.ModelData.FraSluttbrukersystem))
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.fraSluttbrukersystem}");

            if (Helpers.ObjectIsNullOrEmpty(metadata.ModelData.UnntattOffentlighet))
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.unntattOffentlighet}");

            //Usikkert om skal valideres
            //if (Helpers.ObjectIsNullOrEmpty(metadata.ModelData.FtbId))
            //    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.ftbId}");
            
            //if (Helpers.ObjectIsNullOrEmpty(metadata.ModelData.Hovedinnsendingsnummer))
            //    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.hovedinnsendingsnummer}");

            //if (Helpers.ObjectIsNullOrEmpty(metadata.ModelData.KlartForSigneringFraSluttbrukersystem))
            //    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.klartForSigneringFraSluttbrukersystem}");

            //if (Helpers.ObjectIsNullOrEmpty(metadata.ModelData.Prosjektnavn))
            //    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.prosjektnavn}");

            //if (Helpers.ObjectIsNullOrEmpty(metadata.ModelData.SluttbrukersystemUrl))
            //    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.sluttbrukersystemUrl}");

        }
    }
}