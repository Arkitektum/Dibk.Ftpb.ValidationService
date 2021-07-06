using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Deserializers;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    [FormData(DataFormatVersion = "41999")]
    public class ArbeidstilsynetsSamtykke_41999_Validator : FormValidatorBase, IFormValidator
    {
        private readonly FormValidatorConfiguration _formValidatorConfiguration;
        private IList<EntityValidatorNode> _entityValidatorTree;

        private readonly IMunicipalityValidator _municipalityValidator;
        private readonly ICodeListService _codeListService;
        private ArbeidstilsynetsSamtykke_41999_ValidationEntity _validationForm { get; set; }
        private IEiendomsAdresseValidator _eiendomsAdresseValidator;
        private IMatrikkelValidator _matrikkelValidator;
        private IEiendomByggestedValidator _eiendomByggestedValidator;
        private IArbeidsplasserValidator _arbeidsplasserValidator;
        private IEnkelAdresseValidator _tiltakshaverEnkelAdresseValidator;
        private IKontaktpersonValidator _kontaktpersonValidator;
        private IKodelisteValidator _partstypeValidator;
        private IAktoerValidator _tiltakshaverValidator;
        private IEnkelAdresseValidator _fakturamottakerEnkelAdresseValidator;
        private IFakturamottakerValidator _fakturamottakerValidator;

        protected override string XPathRoot => "ArbeidstilsynetsSamtykke";

        public ArbeidstilsynetsSamtykke_41999_Validator(FormValidatorConfiguration formValidatorConfiguration, IMunicipalityValidator municipalityValidator, ICodeListService codeListService)
        {
            _formValidatorConfiguration = formValidatorConfiguration;
            _municipalityValidator = municipalityValidator;
            _codeListService = codeListService;
        }

        public override ValidationResult StartValidation(ValidationInput validationInput)
        {
            ArbeidstilsynetsSamtykkeType formModel = new ArbeidstilsynetsSamtykke_41999_Deserializer().Deserialize(validationInput.FormData);
            _validationForm = new ArbeidstilsynetsSamtykke_41999_Mapper().GetFormEntity(formModel, XPathRoot);

            base.StartValidation(validationInput);

            return ValidationResult;
        }

        protected override void InitializeValidatorConfig()
        {
            //_formValidatorConfiguration.ValidatorFormName = this.GetType().Name;
            //_formValidatorConfiguration.FormXPathRoot = XPathRoot;

            //_formValidatorConfiguration.Validators = new List<EntityValidatorInfo>();

            ////Eiendombyggested
            //_formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EiendomByggestedValidator));
            //_formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EiendomsAdresseValidator, EntityValidatorEnum.EiendomByggestedValidator));
            //_formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.MatrikkelValidator, EntityValidatorEnum.EiendomByggestedValidator));

            ////Tiltakshaver
            //_formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.TiltakshaverValidator));
            //_formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EnkelAdresseValidator, EntityValidatorEnum.TiltakshaverValidator));
            //_formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.KontaktpersonValidator, EntityValidatorEnum.TiltakshaverValidator));
            //_formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.PartstypeValidator, EntityValidatorEnum.TiltakshaverValidator));

            ////Fakturamottaker
            //_formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.FakturamottakerValidator));
            //_formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EnkelAdresseValidator, EntityValidatorEnum.FakturamottakerValidator));

            ////Arbeidsplasser
            //_formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.ArbeidsplasserValidator));


            //New tree structure

            var entityValidatorNodes = new List<EntityValidatorNode>()
            {
                //Eiendombyggested
                new ()
                {
                    Id = 1,
                    EnumId = EntityValidatorEnum.EiendomByggestedValidator,
                    ParentID = null,
                }, //root node
                new ()
                {
                    Id = 2,
                    EnumId = EntityValidatorEnum.EiendomsAdresseValidator,
                    ParentID = 1,
                },
                new ()
                {
                    Id = 3,
                    EnumId = EntityValidatorEnum.MatrikkelValidator,
                    ParentID = 1,
                },
                
                //Tiltakshaver
                new ()
                {
                Id = 4,
                EnumId = EntityValidatorEnum.TiltakshaverValidator,
                ParentID = null,
                }, //root node
                new () {
                Id = 5,
                EnumId = EntityValidatorEnum.KontaktpersonValidator,
                ParentID = 4,

                },
                new () {
                Id = 6,
                EnumId = EntityValidatorEnum.PartstypeValidator,
                ParentID = 4,
                },
                new () {
                Id = 7,
                EnumId = EntityValidatorEnum.EnkelAdresseValidator,
                ParentID = 4,
            },
            //Fakturamottaker
            new ()
            {
                Id = 8,
                EnumId = EntityValidatorEnum.FakturamottakerValidator,
                ParentID = 4,
            },
            new ()
            {
                Id = 9,
                EnumId = EntityValidatorEnum.EnkelAdresseValidator,
                ParentID = 8,
            },
            //Arbeidsplasser
            new ()
            {
                Id = 10,
                EnumId = EntityValidatorEnum.ArbeidsplasserValidator,
                ParentID = null,
            },
            };

            _entityValidatorTree = EntityValidatiorTree.BuildTree(entityValidatorNodes);
        }

        protected override void InstantiateValidators()
        {
            //EiendomByggested
            _matrikkelValidator = new MatrikkelValidator(_entityValidatorTree, 3);
            _eiendomsAdresseValidator = new EiendomsAdresseValidator(_entityValidatorTree, 2);
            _eiendomByggestedValidator = new EiendomByggestedValidator(_entityValidatorTree,1, _eiendomsAdresseValidator, _matrikkelValidator, _municipalityValidator);

            //Tiltakshaver
            _tiltakshaverEnkelAdresseValidator = new EnkelAdresseValidator(_entityValidatorTree, 7);
            _kontaktpersonValidator = new KontaktpersonValidator(_entityValidatorTree, 5);
            _partstypeValidator = new PartstypeValidator(_entityValidatorTree, 6, _codeListService);
            _tiltakshaverValidator = new TiltakshaverValidator(_entityValidatorTree,4, _tiltakshaverEnkelAdresseValidator, _kontaktpersonValidator, _partstypeValidator, _codeListService);
            //Arbeidsplasser
            _arbeidsplasserValidator = new ArbeidsplasserValidator(_entityValidatorTree,10);

            //Fakturamottaker
            _fakturamottakerEnkelAdresseValidator = new EnkelAdresseValidator(_entityValidatorTree,9);
            _fakturamottakerValidator = new FakturamottakerValidator(_entityValidatorTree,8, _fakturamottakerEnkelAdresseValidator);

        }
        protected override void DefineValidationRules()
        {
            AccumulateValidationRules(_eiendomByggestedValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_eiendomsAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_matrikkelValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_arbeidsplasserValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_tiltakshaverValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_partstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_tiltakshaverEnkelAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_kontaktpersonValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_fakturamottakerValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_fakturamottakerEnkelAdresseValidator.ValidationResult.ValidationRules);
        }

        public ArbeidstilsynetsSamtykkeType DeserializeDataForm(string xmlData)
        {
            //TODO add error massages if.... :thinkingFace
            return SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
        }

        protected override void Validate(ValidationInput validationInput)
        {
            var eiendomValidationResult = _eiendomByggestedValidator.Validate(_validationForm.ModelData.EiendomValidationEntities);
            AccumulateValidationMessages(eiendomValidationResult.ValidationMessages);

            var attachments = Helpers.ObjectIsNullOrEmpty(validationInput.Attachments) ? null : validationInput.Attachments.Select(a => a.AttachmentTypeName).ToList();
            var arbeidsplasserValidationResult = _arbeidsplasserValidator.Validate(_validationForm.ModelData.ArbeidsplasserValidationEntity, attachments);
            AccumulateValidationMessages(arbeidsplasserValidationResult.ValidationMessages);

            var tiltakshaverValidationResult = _tiltakshaverValidator.Validate(_validationForm.ModelData.TiltakshaverValidationEntity);
            AccumulateValidationMessages(tiltakshaverValidationResult.ValidationMessages);

            var fakturamottakerValidationResult = _fakturamottakerValidator.Validate(_validationForm.ModelData.FakturamottakerValidationEntity);
            AccumulateValidationMessages(fakturamottakerValidationResult.ValidationMessages);

            //return ValidationResult;
        }
    }
}
