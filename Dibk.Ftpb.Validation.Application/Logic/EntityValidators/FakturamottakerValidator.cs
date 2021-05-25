using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class FakturamottakerValidator : EntityValidatorBase
    {

        private EnkelAdresseValidator _enkelAdresseValidator;
        public FakturamottakerValidator() : base()
        {
            _enkelAdresseValidator = new EnkelAdresseValidator();
            
        }
        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule("fakturamottaker_utfylt", xPathForEntity);
        }

        public ValidationResult Validate(FakturamottakerValidationEntity fakturamottaker = null)
        {
            InitializeValidationRules(fakturamottaker.DataModelXpath);            

            if (Helpers.ObjectIsNullOrEmpty(fakturamottaker.ModelData))
            {
                AddMessageFromRule("fakturamottaker_utfylt");
            }
            else
            {
                var adresseValidationResult =  _enkelAdresseValidator.Validate(fakturamottaker.ModelData.Adresse);
                UpdateValidationResultWithSubValidations(adresseValidationResult);
            }
            return _validationResult;
        }
    }
}
