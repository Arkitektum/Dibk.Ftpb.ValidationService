using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Logic.Deserializers;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    [FormData(DataFormatVersion = "45957")]
    public class ArbeidstilsynetsSamtykke2_45957_Validator : IFormValidator
    {
        private readonly IMunicipalityValidator _municipalityValidator;
        private readonly ICodeListService _codeListService;
        public ArbeidstilsynetsSamtykke2Form_45957_ValidationEntity ArbeidstilsynetsSamtykke2Form45957 { get; set; }
        public ArbeidstilsynetsSamtykkeType _form { get; set; }

        private ValidationResult _validationResult;

        //private readonly string _xPath = "ArbeidstilsynetsSamtykke";
        public ArbeidstilsynetsSamtykke2_45957_Validator(IMunicipalityValidator municipalityValidator, ICodeListService codeListService)
        {
            _municipalityValidator = municipalityValidator;
            _codeListService = codeListService;
        }

        public ValidationResult StartValidation(ValidationInput validationInput)
        {
            //Get Arbeidstilsynets Samtykke v2 Dfv45957 class
            _form = new ArbeidstilsynetsSamtykke2_45957_Deserializer().Deserialize(validationInput.FormData);

            // map to arbeidstilsynet formEntity 
            ArbeidstilsynetsSamtykke2Form45957 = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().GetFormEntity(_form);
            Validate(ArbeidstilsynetsSamtykke2Form45957, validationInput);

            return _validationResult;
        }
        public ArbeidstilsynetsSamtykkeType DeserializeDataForm(string xmlData)
        {
            //TODO add error massages if.... :thinkingFace
            return SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
        }

        public ValidationResult Validate(ArbeidstilsynetsSamtykke2Form_45957_ValidationEntity form, ValidationInput validationInput)
        {


            //Runner runner = new Runner();
            //runner.Run();
            //runner.FindNew();

            var eiendomValidator = new EiendomValidator(_municipalityValidator);

            foreach (var eiendomValidationEntity in form.ModelData.EiendomValidationEntities)
            {
                int index = form.ModelData.EiendomValidationEntities.ToList().IndexOf(eiendomValidationEntity);
                var eiendomValidationResult = eiendomValidator.Validate(eiendomValidationEntity);
                UpdateValidationResult(eiendomValidationResult);
            }

            var arbeidsplasser = new ArbeidsplasserValidator();

            var attachments = Helpers.ObjectIsNullOrEmpty(validationInput.Attachments) ? null : validationInput.Attachments.Select(a => a.AttachmentTypeName).ToList();
            var arbeidsplasserValidator = arbeidsplasser.Validate(form.ModelData.ArbeidsplasserValidationEntity, attachments);
            UpdateValidationResult(arbeidsplasserValidator);

            var tiltakshaverResult = new TiltakshaverValidator(_codeListService).Validate(form.ModelData.TiltakshaverValidationEntity);
            UpdateValidationResult(tiltakshaverResult);

            var fakturamottakerResult = new FakturamottakerValidator().Validate(form.ModelData.FakturamottakerValidationEntity);
            UpdateValidationResult(fakturamottakerResult);

            return _validationResult;
        }

        internal void UpdateValidationResult(ValidationResult validationResult)
        {
            _validationResult ??= new ValidationResult();
            _validationResult.ValidationRules ??= new List<ValidationRule>();
            _validationResult.ValidationMessages ??= new List<ValidationMessage>();

            //var whereNotAlreadyExists = validationResult.ValidationRules.Where(x => _validationResult.ValidationRules.All(y => y.Xpath != x.Xpath));
            var whereNotAlreadyExists = validationResult.ValidationRules.Where(x => !_validationResult.ValidationRules.Any(y => y.Xpath == x.Xpath && y.Id == x.Id));
            _validationResult.ValidationRules.AddRange(whereNotAlreadyExists);
            _validationResult.ValidationMessages.AddRange(validationResult.ValidationMessages);
        }
    }

    public class Person
    {
        public int ID { get; set; }
        public string LastName { get; set; }
    }
    public class Runner
    {
        List<Person> peopleList1 = new List<Person>();
        List<Person> peopleList2 = new List<Person>();
        public void Run()
        {
            peopleList1.Add(new Person() { ID = 1, LastName = "A" });
            peopleList1.Add(new Person() { ID = 2, LastName = "B" });
            peopleList1.Add(new Person() { ID = 3, LastName = "C" });
                                                 
            peopleList2.Add(new Person() { ID = 1, LastName = "A" });
            peopleList2.Add(new Person() { ID = 2, LastName = "B" });
            peopleList2.Add(new Person() { ID = 3, LastName = "W" });
            peopleList2.Add(new Person() { ID = 4, LastName = "D" });
            peopleList2.Add(new Person() { ID = 5, LastName = "E" });
        }

        public void FindNew()
        {
            //var result = peopleList2.Where(p => peopleList1.All(p2 => p2.ID != p.ID && p2.LastName != p.LastName));
            var result = peopleList2.Where(p => !peopleList1.Any(p2 => p2.ID == p.ID && p2.LastName == p.LastName));
        }
    }


}
