using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EiendomByggestedValidator : EntityValidatorBase, IEiendomByggestedValidator
    {
        private IEiendomsAdresseValidator _eiendomsAdresseValidator;
        private readonly IMatrikkelValidator _matrikkelValidator;

        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public EiendomByggestedValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, IEiendomsAdresseValidator eiendomsAdresseValidator, IMatrikkelValidator matrikkelValidator)
            : base(entityValidatorTree, nodeId)
        {
            _eiendomsAdresseValidator = eiendomsAdresseValidator;
            _matrikkelValidator = matrikkelValidator;
        }


        protected override void InitializeValidationRules()
        {

            //AddValidationRule(EiendomValidationEnums.utfylt, "adresse");

            AddValidationRule(EiendomValidationEnum.utfylt, null);
            AddValidationRule(EiendomValidationEnum.bygningsnummer_utfylt, "bygningsnummer");
            AddValidationRule(EiendomValidationEnum.bolignummer_utfylt, "bolignummer");
            AddValidationRule(EiendomValidationEnum.kommunenavn_utfylt, "kommunenavn");
        }

        public ValidationResult Validate(IEnumerable<EiendomValidationEntity> eiendomValidationEntities)
        {
            if (Helpers.ObjectIsNullOrEmpty(eiendomValidationEntities) || eiendomValidationEntities.Count() == 0)
            {
                AddMessageFromRule(EiendomValidationEnum.utfylt);
            }
            else
            {

                foreach (var eiendomValidationEntity in eiendomValidationEntities)
                {
                    ValidateEntityFields(eiendomValidationEntity);

                    var matrikkelValidationResult = _matrikkelValidator.Validate(eiendomValidationEntity.ModelData.Matrikkel);
                    _validationResult.ValidationMessages.AddRange(matrikkelValidationResult.ValidationMessages);


                    var eiendomsAdresseValidationResult = _eiendomsAdresseValidator.Validate(eiendomValidationEntity.ModelData.Adresse);
                    _validationResult.ValidationMessages.AddRange(eiendomsAdresseValidationResult.ValidationMessages);



                    ValidateDataRelations(eiendomValidationEntity);
                }
            }

            return _validationResult;
        }

        private void ValidateDataRelations(EiendomValidationEntity eiendomValidationEntity)
        {
            var xPath = eiendomValidationEntity.DataModelXpath;

            if (!TillattPostnrIKommune(eiendomValidationEntity.ModelData.Kommunenavn, eiendomValidationEntity.ModelData.Adresse.ModelData?.Postnr))
            {
                AddMessageFromRule(EiendomValidationEnum.tillatte_postnr_i_kommune, eiendomValidationEntity.DataModelXpath, new[] { eiendomValidationEntity.ModelData.Adresse.ModelData?.Postnr, eiendomValidationEntity.ModelData.Kommunenavn });
            }
        }

        private void ValidateEntityFields(EiendomValidationEntity eiendomValidationEntity)
        {
            var xPath = eiendomValidationEntity.DataModelXpath;
            if (!Helpers.ObjectIsNullOrEmpty(eiendomValidationEntity.ModelData?.Bygningsnummer))
            {
                long bygningsnrLong = 0;
                if (!long.TryParse(eiendomValidationEntity.ModelData?.Bygningsnummer, out bygningsnrLong))
                {
                    AddMessageFromRule(EiendomValidationEnum.bygningsnummer_ugyldig, xPath, new[] { eiendomValidationEntity.ModelData?.Bygningsnummer });
                }
            }

            if (Helpers.ObjectIsNullOrEmpty(eiendomValidationEntity.ModelData?.Bolignummer))
                AddMessageFromRule(EiendomValidationEnum.bolignummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomValidationEntity.ModelData?.Kommunenavn))
                AddMessageFromRule(EiendomValidationEnum.kommunenavn_utfylt, xPath);
        }

        private bool TillattPostnrIKommune(string kommunenavn, string postnr)
        {
            var kommunenavnOgTillattePostnr = new List<(string kommune, List<string> postnrListe)>();
            kommunenavnOgTillattePostnr.Add(("Midt Telemark", new List<string>() { "3800", "3801", "3802", "3803", "3804" }));
            var funnetKommune = kommunenavnOgTillattePostnr.Where(x => x.kommune.Equals(kommunenavn)).FirstOrDefault();

            return funnetKommune.postnrListe.Contains(postnr);
        }
    }
}
