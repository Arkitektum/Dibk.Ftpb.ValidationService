using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Deserializers;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests
{
    public class ArbeidstilsynetsSamtykke245957ValidatorTest
    {
        IMunicipalityValidator _municipalityValidator;
        private ICodeListService _codeListService;
        ArbeidstilsynetsSamtykke2_45957_Validator _formValidator;
        private readonly string _rootDirTestResults = @"C:\ATIL_testresults";
        private readonly bool WriteValidationResultsToJsonFile = true;
        public ArbeidstilsynetsSamtykke245957ValidatorTest()
        {
            _municipalityValidator = MockDataSource.MunicipalityValidatorResult(MunicipalityValidationEnum.Ok);
            _codeListService = MockDataSource.IsCodeListValid(FtbCodeListNames.Partstype, true);

            _formValidator = new ArbeidstilsynetsSamtykke2_45957_Validator(_municipalityValidator,_codeListService);

            if (WriteValidationResultsToJsonFile && !Directory.Exists(_rootDirTestResults))
            {
                Directory.CreateDirectory(_rootDirTestResults);
            }
        }


        [Fact]
        public void ValidatortestHeleSkjemaet()
        {
            ValidationInput validationInput = new ValidationInput();
            validationInput.FormData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><eiendom><adresse><adresselinje1>Bøgata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3800</postnr><poststed>Bø i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>148</gaardsnummer><bruksnummer>283</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendom></eiendomByggested></ArbeidstilsynetsSamtykke>";
           
            var validationResult = _formValidator.StartValidation(validationInput);

            var validationMessage = validationResult.ValidationMessages.Where(x => x.Reference.Equals("eiendomsAdresse_adresselinje2_utfylt")).FirstOrDefault();
            validationMessage.Reference.Should().NotBe(null);

            validationMessage = validationResult.ValidationMessages.Where(x => x.Reference.Equals("eiendomsAdresse_adresselinje3_utfylt")).FirstOrDefault();
            validationMessage.Reference.Should().NotBe(null);

            validationMessage = validationResult.ValidationMessages.Where(x => x.Reference.Equals("eiendomsAdresse_gatenavn_utfylt")).FirstOrDefault();
            validationMessage.Reference.Should().NotBe(null);

            validationMessage = validationResult.ValidationMessages.Where(x => x.Reference.Equals("eiendomsAdresse_husnr_utfylt")).FirstOrDefault();
            validationMessage.Reference.Should().NotBe(null);
            
            validationMessage = validationResult.ValidationMessages.Where(x => x.Reference.Equals("eiendomsAdresse_bokstav_utfylt")).FirstOrDefault();
            validationMessage.Reference.Should().NotBe(null);

            var validationComposer = new ValidationMessageComposer();
            var newValidationReport = validationComposer.ComposeValidationReport(validationResult, "NO");

            if (WriteValidationResultsToJsonFile)
            {
                var jsonString = JsonConvert.SerializeObject(newValidationReport);
                File.WriteAllText(_rootDirTestResults + @"\validatortest_hele_skjemaet_" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".json", jsonString);
            }
        }

        [Fact]
        public void EiendomsValidatorIkke4SifferIPostnr()
        {
            string xmlData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><eiendom><adresse><adresselinje1>Bøgata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>xxx3800</postnr><poststed>Bø i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>148</gaardsnummer><bruksnummer>283</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendom></eiendomByggested></ArbeidstilsynetsSamtykke>";

            var dataModel = new ArbeidstilsynetsSamtykke2_45957_Deserializer().Deserialize(xmlData);
            var formEntity = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().GetFormEntity(dataModel);
            var validationResultForEiendomsAdresse = new EiendomsAdresseValidator("Test").Validate(formEntity.ModelData.EiendomValidationEntities.ToList()[0].ModelData.Adresse);

            var validationMessage = validationResultForEiendomsAdresse.ValidationMessages.Where(x => x.Reference.Equals("eiendomsAdresse_postnr_4siffer")).FirstOrDefault();
            validationMessage.Reference.Should().NotBe(null);

            var validationComposer = new ValidationMessageComposer();
            var newValidationReport = validationComposer.ComposeValidationReport(validationResultForEiendomsAdresse, "NO");

            if (WriteValidationResultsToJsonFile)
            {
                var jsonString = JsonConvert.SerializeObject(newValidationReport);
                File.WriteAllText(_rootDirTestResults + @"\eiendomValidator_ikke_4_siffer_i_postnr_" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".json", jsonString);
            }
        }
        [Fact]
        public void EiendomsValidatorMedUgyldigPostnrIKommune()
        {
            string xmlData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><eiendom><adresse><adresselinje1>Bøgata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3810</postnr><poststed>Bø i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>148</gaardsnummer><bruksnummer>283</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendom></eiendomByggested></ArbeidstilsynetsSamtykke>";

            var dataModel = new ArbeidstilsynetsSamtykke2_45957_Deserializer().Deserialize(xmlData);
            var formEntity = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().GetFormEntity(dataModel);
            var validationResultForEiendom = new EiendomValidator("Test", _municipalityValidator).Validate(formEntity.ModelData.EiendomValidationEntities);

            var validationMessage = validationResultForEiendom.ValidationMessages.Where(x => x.Reference.Equals("tillatte_postnr_i_kommune")).FirstOrDefault();
            //validationMessage.Reference.Should().NotBe(null);
            validationMessage.Should().NotBe(null);

            var validationComposer = new ValidationMessageComposer();
            var newValidationReport = validationComposer.ComposeValidationReport(validationResultForEiendom, "NO");

            if (WriteValidationResultsToJsonFile)
            {
                var jsonString = JsonConvert.SerializeObject(newValidationReport);
                File.WriteAllText(_rootDirTestResults + @"\eiendomValidator_med_ugyldig_postnr_" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".json", jsonString);
            }
        }

        [Fact]
        public void EiendomsValidatorFor2Eiendommer()
        {
            string xmlData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><eiendom><adresse><adresselinje1>B�gata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3810</postnr><poststed>B� i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>148</gaardsnummer><bruksnummer>283</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendom><eiendom><adresse><adresselinje1>B�gata 2</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3809</postnr><poststed>B� i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>248</gaardsnummer><bruksnummer>283</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendom></eiendomByggested></ArbeidstilsynetsSamtykke>";

            var dataModel = new ArbeidstilsynetsSamtykke2_45957_Deserializer().Deserialize(xmlData);
            var formEntity = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().GetFormEntity(dataModel);
            var validationResultForEiendom = new EiendomValidator("Test", _municipalityValidator).Validate(formEntity.ModelData.EiendomValidationEntities);

            var validationMessage = validationResultForEiendom.ValidationMessages.Where(x => x.Reference.Equals("tillatte_postnr_i_kommune")).FirstOrDefault();
            //validationMessage.Reference.Should().NotBe(null);
            validationMessage.Should().NotBe(null);

            var validationComposer = new ValidationMessageComposer();
            var newValidationReport = validationComposer.ComposeValidationReport(validationResultForEiendom, "NO");

            if (WriteValidationResultsToJsonFile)
            {
                var jsonString = JsonConvert.SerializeObject(newValidationReport);
                File.WriteAllText(_rootDirTestResults + @"\eiendomValidator_for_2_eiendommer_" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".json", jsonString);
            }
        }

        [Fact]
        public void ValidatortestFor2Eiendommer()
        {
            string xmlData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><eiendom><adresse><adresselinje1>B�gata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3810</postnr><poststed>B� i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>148</gaardsnummer><bruksnummer>283</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendom><eiendom><adresse><adresselinje1>B�gata 2</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3809</postnr><poststed>B� i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>248</gaardsnummer><bruksnummer>283</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendom></eiendomByggested></ArbeidstilsynetsSamtykke>";

            ValidationInput validationInput = new();
            validationInput.FormData = xmlData;
            var result = _formValidator.StartValidation(validationInput);

            var validationComposer = new ValidationMessageComposer();
            var newValidationReport = validationComposer.ComposeValidationReport(result, "NO");

            if (WriteValidationResultsToJsonFile)
            {
                var jsonString = JsonConvert.SerializeObject(newValidationReport);
                File.WriteAllText(_rootDirTestResults + @"\validatortest_for_eiendom_2_eiendommer_" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".json", jsonString);
            }
        }
    }
}
