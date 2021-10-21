using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Newtonsoft.Json;
using System;
using System.IO;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Xunit;
using Dibk.Ftpb.Validation.Application.Services;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Tests
{
    public class ArbeidstilsynetsSamtykke2_45957_Validator_Test
    {
        IMunicipalityValidator _municipalityValidator;
        private ICodeListService _codeListService;
        private readonly IPostalCodeService _postalCodeService;
        private IChecklistService _checklistService;

        ArbeidstilsynetsSamtykke2_45957_Validator _formValidator;
        private readonly string _rootDirTestResults = @"C:\ATIL_testresults";
        private readonly bool WriteValidationResultsToJsonFile = true;
        private readonly ValidationMessageComposer _validationMessageComposer;

        public ArbeidstilsynetsSamtykke2_45957_Validator_Test()
        {
            _municipalityValidator = MockDataSource.MunicipalityValidatorResult(MunicipalityValidationEnum.Ok);
            _codeListService = MockDataSource.IsCodeListValid(FtbKodeListeEnum.Partstype, true);
            _postalCodeService = MockDataSource.ValidatePostnr(true, "Bø i Telemark", "true");
            FormValidatorConfiguration formValidatorConfiguration = new FormValidatorConfiguration();
            _checklistService = MockDataSource.GetCheckpoints("AT");
            _validationMessageComposer = new ValidationMessageComposer();
            _formValidator = new ArbeidstilsynetsSamtykke2_45957_Validator(_validationMessageComposer, _municipalityValidator, _codeListService, _postalCodeService, _checklistService);

            if (WriteValidationResultsToJsonFile && !Directory.Exists(_rootDirTestResults))
            {
                Directory.CreateDirectory(_rootDirTestResults);
            }

        }


        [Fact]
        public void ValidatortestHeleSkjemaet()
        {
            //ValidationInput validationInput = new ValidationInput();
            //validationInput.FormData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><eiendom><adresse><adresselinje1>Bøgata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3800</postnr><poststed>Bø i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>148</gaardsnummer><bruksnummer>283</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendom></eiendomByggested></ArbeidstilsynetsSamtykke>";

            //var validationResult = _formValidator.StartValidation("45957", validationInput);

            //var validationMessage = validationResult.ValidationMessages.Where(x => x.Rule.Equals("eiendomsAdresse_adresselinje2_utfylt")).FirstOrDefault();
            //validationMessage.Rule.Should().NotBe(null);

            //validationMessage = validationResult.ValidationMessages.Where(x => x.Rule.Equals("eiendomsAdresse_adresselinje3_utfylt")).FirstOrDefault();
            //validationMessage.Rule.Should().NotBe(null);

            //validationMessage = validationResult.ValidationMessages.Where(x => x.Rule.Equals("eiendomsAdresse_gatenavn_utfylt")).FirstOrDefault();
            //validationMessage.Rule.Should().NotBe(null);

            //validationMessage = validationResult.ValidationMessages.Where(x => x.Rule.Equals("eiendomsAdresse_husnr_utfylt")).FirstOrDefault();
            //validationMessage.Rule.Should().NotBe(null);

            //validationMessage = validationResult.ValidationMessages.Where(x => x.Rule.Equals("eiendomsAdresse_bokstav_utfylt")).FirstOrDefault();
            //validationMessage.Rule.Should().NotBe(null);

            //if (WriteValidationResultsToJsonFile)
            //{
            //    var jsonString = JsonConvert.SerializeObject(validationResult);
            //    File.WriteAllText(_rootDirTestResults + @"\validatortest_hele_skjemaet_" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".json", jsonString);
            //}
        }

        [Fact]
        public void EiendomsValidatorIkke4SifferIPostnr()
        {
            string xmlData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><eiendom><adresse><adresselinje1>Bøgata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>xxx3800</postnr><poststed>Bø i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>148</gaardsnummer><bruksnummer>283</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendom></eiendomByggested></ArbeidstilsynetsSamtykke>";

            var dataModel = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykke2_45957_Form>(xmlData);


            FormValidatorConfiguration formValidatorConfiguration = new FormValidatorConfiguration();
            //var validationResultForEiendomsAdresse = new EiendomsAdresseValidator(formValidatorConfiguration, EntityValidatorEnum.EiendomByggestedValidator).Validate(formEntity.ModelData.EiendomValidationEntities.ToList()[0].ModelData.Adresse);

            //var validationMessage = validationResultForEiendomsAdresse.ValidationMessages.Where(x => x.Reference.Equals("eiendomsAdresse_postnr_4siffer")).FirstOrDefault();
            //validationMessage.Reference.Should().NotBe(null);

            //var validationComposer = new ValidationMessageComposer();
            //var newValidationReport = validationComposer.ComposeValidationReport("45957", validationResultForEiendomsAdresse, "NO");

            //if (WriteValidationResultsToJsonFile)
            //{
            //    var jsonString = JsonConvert.SerializeObject(newValidationReport);
            //    File.WriteAllText(_rootDirTestResults + @"\eiendomByggestedValidator_ikke_4_siffer_i_postnr_" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".json", jsonString);
            //}
        }
        [Fact]
        public void EiendomsValidatorMedUgyldigPostnrIKommune()
        {
            string xmlData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><eiendom><adresse><adresselinje1>Bøgata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3810</postnr><poststed>Bø i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>148</gaardsnummer><bruksnummer>283</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendom></eiendomByggested></ArbeidstilsynetsSamtykke>";

            var formEntity = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykke2_45957_Form>(xmlData);
            FormValidatorConfiguration formValidatorConfiguration = new FormValidatorConfiguration();
            //IEiendomsAdresseValidator eiendomsAdresseValidator = new EiendomsAdresseValidator(formValidatorConfiguration, EntityValidatorEnum.EiendomByggestedValidator);
            //IMatrikkelValidator matrikkelValidator = new MatrikkelValidator(formValidatorConfiguration, EntityValidatorEnum.EiendomByggestedValidator);

            //var validationResultForEiendom = new EiendomByggestedValidator(formValidatorConfiguration, eiendomsAdresseValidator, matrikkelValidator, _municipalityValidator).Validate(formEntity.ModelData.EiendomValidationEntities);

            //var validationMessage = validationResultForEiendom.ValidationMessages.Where(x => x.Reference.Equals("tillatte_postnr_i_kommune")).FirstOrDefault();
            ////validationMessage.Reference.Should().NotBe(null);
            //validationMessage.Should().NotBe(null);

            //var validationComposer = new ValidationMessageComposer();
            //var newValidationReport = validationComposer.ComposeValidationReport("45957", validationResultForEiendom, "NO");

            //if (WriteValidationResultsToJsonFile)
            //{
            //    var jsonString = JsonConvert.SerializeObject(newValidationReport);
            //    File.WriteAllText(_rootDirTestResults + @"\eiendomByggestedValidator_med_ugyldig_postnr_" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".json", jsonString);
            //}
        }

        [Fact]
        public void EiendomsValidatorFor2Eiendommer()
        {
            string xmlData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><eiendom><adresse><adresselinje1>B�gata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3810</postnr><poststed>B� i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>148</gaardsnummer><bruksnummer>283</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendom><eiendom><adresse><adresselinje1>B�gata 2</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3809</postnr><poststed>B� i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>248</gaardsnummer><bruksnummer>283</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendom></eiendomByggested></ArbeidstilsynetsSamtykke>";

            var formEntity = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykke2_45957_Form>(xmlData);
            
            //FormValidatorConfiguration formValidatorConfiguration = new FormValidatorConfiguration();
            //IEiendomsAdresseValidator eiendomsAdresseValidator = new EiendomsAdresseValidator(formValidatorConfiguration, EntityValidatorEnum.EiendomByggestedValidator);
            //IMatrikkelValidator matrikkelValidator = new MatrikkelValidator(formValidatorConfiguration, EntityValidatorEnum.EiendomByggestedValidator);
            //var validationResultForEiendom = new EiendomByggestedValidator(formValidatorConfiguration, eiendomsAdresseValidator, matrikkelValidator, _municipalityValidator).Validate(formEntity.ModelData.EiendomValidationEntities);

            //var validationMessage = validationResultForEiendom.ValidationMessages.Where(x => x.Reference.Equals("tillatte_postnr_i_kommune")).FirstOrDefault();
            ////validationMessage.Reference.Should().NotBe(null);
            //validationMessage.Should().NotBe(null);

            //var validationComposer = new ValidationMessageComposer();
            //var newValidationReport = validationComposer.ComposeValidationReport("45957", validationResultForEiendom, "NO");

            //if (WriteValidationResultsToJsonFile)
            //{
            //    var jsonString = JsonConvert.SerializeObject(newValidationReport);
            //    File.WriteAllText(_rootDirTestResults + @"\eiendomByggestedValidator_for_2_eiendommer_" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".json", jsonString);
            //}
        }

        [Fact]
        public void ValidatortestFor2Eiendommer()
        {
            //string xmlData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><eiendom><adresse><adresselinje1>B�gata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3810</postnr><poststed>B� i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>148</gaardsnummer><bruksnummer>283</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendom><eiendom><adresse><adresselinje1>B�gata 2</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3809</postnr><poststed>B� i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>248</gaardsnummer><bruksnummer>283</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendom></eiendomByggested></ArbeidstilsynetsSamtykke>";
            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");

            ValidationInput validationInput = new();
            validationInput.FormData = xmlData;
            var newValidationReport = _formValidator.StartValidation(validationInput);

            if (WriteValidationResultsToJsonFile)
            {
                var jsonString = JsonConvert.SerializeObject(newValidationReport);
                File.WriteAllText(_rootDirTestResults + @"\validatortest_for_eiendom_2_eiendommer_" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".json", jsonString);
            }
        }
    }
}
