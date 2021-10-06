using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Logic.Deserializers.Ansako;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.Ansako.AnsvarsrettAnsako;
using Dibk.Ftpb.Validation.Application.Models.FormEntities.Ansako;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Services;
using no.kxml.skjema.dibk.ansvarsrettAnsako;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators.Ansako
{
    [FormData(DataFormatVersion = "10000")]
    public class AnsvarsrettAnsako_ANSAKO_10000_Validator : FormValidatorBase, IFormValidator
    {
        //AnsvarligSoeker
        private IAktoerValidator _ansvarligSoekerValidator;
        private IEnkelAdresseValidator _ansvarligSoekerEnkelAdresseValidator;
        private IKodelisteValidator _ansvarligSoekerPartstypeValidator;
        private IKontaktpersonValidator _ansvarligSoekerKontaktpersonValidator;
        private AnsvarsrettAnsako_ANSAKO_10000_Form _validationForm;

        public AnsvarsrettAnsako_ANSAKO_10000_Validator(IValidationMessageComposer validationMessageComposer, IChecklistService checklistService = null) : base(validationMessageComposer, checklistService)
        {
        }

        public override ValidationResult StartValidation(string dataFormatVersion, ValidationInput validationInput)
        {
            ErklaeringAnsvarsrettType formModel = new AnsvarsrettAnsako_ANSAKO_10000__Deserializer().Deserialize(validationInput.FormData);
            _validationForm = new AnsvarsrettAnsako_ANSAKO_10000_Mapper().GetFormEntity(formModel);
            base.StartValidation(dataFormatVersion, validationInput);

            return ValidationResult;
        }

        protected override string XPathRoot { get; }
        protected override void InitializeValidatorConfig()
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<string> GetFormTiltakstyper()
        {
            throw new NotImplementedException();
        }

        protected override void InstantiateValidators()
        {
            throw new NotImplementedException();
        }

        protected override void Validate(ValidationInput validationInput)
        {
            throw new NotImplementedException();
        }

        protected override void DefineValidationRules()
        {
            throw new NotImplementedException();
        }
    }
}
