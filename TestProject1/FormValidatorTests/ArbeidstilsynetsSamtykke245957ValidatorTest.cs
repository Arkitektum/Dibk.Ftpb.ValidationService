using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Xunit;
using Newtonsoft.Json;
using System;
using System.IO;
using Dibk.Ftpb.Validation.Application.Logic.Deserializers;
using Dibk.Ftpb.Validation.Application.Logic.Mappers;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using System.Linq;
using FluentAssertions;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Tests
{
    public class ArbeidstilsynetsSamtykke245957ValidatorTest
    {
        IMunicipalityValidator _municipalityValidator;
        ArbeidstilsynetsSamtykke2_45957_Validator _formValidator;
        private readonly string _rootDirTestResults = @"C:\ATIL_testresults";
        private readonly bool WriteValidationResultsToJsonFile = false;
        public ArbeidstilsynetsSamtykke245957ValidatorTest()
        {
            _municipalityValidator = MockDataSource.MunicipalityValidatorResult(MunicipalityValidationEnum.Ok);
            _formValidator = new ArbeidstilsynetsSamtykke2_45957_Validator(_municipalityValidator);

            if (WriteValidationResultsToJsonFile && !Directory.Exists(_rootDirTestResults))
            {
                Directory.CreateDirectory(_rootDirTestResults);
            }
        }


        [Fact]
        public void ValidateArbeidstilsynetsSamtykke2_hele_skjemaet()
        {
            string xmlData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><adresse><adresselinje1>Bøgata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3800</postnr><poststed>Bø i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>917</gaardsnummer><bruksnummer>135</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendomByggested></ArbeidstilsynetsSamtykke>";

            var validationResults = _formValidator.StartValidation(xmlData);

            var validationRule = validationResults.Where(x => x.id.Equals("eiendomsAdresse_adresselinje2_utfylt")).FirstOrDefault();
            validationRule.validationResult.Should().Be(ValidationResultEnum.ValidationFailed);

            validationRule = validationResults.Where(x => x.id.Equals("eiendomsAdresse_adresselinje3_utfylt")).FirstOrDefault();
            validationRule.validationResult.Should().Be(ValidationResultEnum.ValidationFailed);

            validationRule = validationResults.Where(x => x.id.Equals("eiendomsAdresse_gatenavn_utfylt")).FirstOrDefault();
            validationRule.validationResult.Should().Be(ValidationResultEnum.ValidationFailed);

            validationRule = validationResults.Where(x => x.id.Equals("eiendomsAdresse_husnr_utfylt")).FirstOrDefault();
            validationRule.validationResult.Should().Be(ValidationResultEnum.ValidationFailed);

            validationRule = validationResults.Where(x => x.id.Equals("eiendomsAdresse_bokstav_utfylt")).FirstOrDefault();
            validationRule.validationResult.Should().Be(ValidationResultEnum.ValidationFailed);


            var validationComposer = new ValidationMessageComposer();
            var newValidationReport = validationComposer.ComposeValidationReport(validationResults, "NO");

            if (WriteValidationResultsToJsonFile)
            {
                var jsonString = JsonConvert.SerializeObject(validationResults);
                File.WriteAllText(_rootDirTestResults + @"\validationResultsHeleSkjemaet_" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".json", jsonString);
            }
        }

        [Fact]
        public void ValidateArbeidstilsynetsSamtykke2_ikke_4_siffer_i_postnr()
        {
            string xmlData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><adresse><adresselinje1>Bøgata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>x3800</postnr><poststed>Bø i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>917</gaardsnummer><bruksnummer>135</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendomByggested></ArbeidstilsynetsSamtykke>";

            var dataModel = new ArbeidstilsynetsSamtykke2_45957_Deserializer().Deserialize(xmlData);
            var formEntity = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().GetFormEntity(dataModel);
            var validationResultForEiendomsAdresse = new EiendomsAdresseValidator().Validate("Test/eiendom", formEntity.Eiendom.Adresse);

            var validationRule = validationResultForEiendomsAdresse.Where(x => x.id.Equals("eiendomsAdresse_postnr_4siffer")).FirstOrDefault();
            validationRule.validationResult.Should().Be(ValidationResultEnum.ValidationFailed);

            if (WriteValidationResultsToJsonFile)
            {
                var jsonString = JsonConvert.SerializeObject(validationResultForEiendomsAdresse);
                File.WriteAllText(_rootDirTestResults + @"\validationResultsForEiendomsAdresse_" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".json", jsonString);
            }
        }
        [Fact]
        public void ValidateArbeidstilsynetsSamtykke2_med_ugyldig_postnr_i_kommune()
        {
            string xmlData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><adresse><adresselinje1>Bøgata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3810</postnr><poststed>Bø i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>917</gaardsnummer><bruksnummer>135</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendomByggested></ArbeidstilsynetsSamtykke>";

            var dataModel = new ArbeidstilsynetsSamtykke2_45957_Deserializer().Deserialize(xmlData);
            var formEntity = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().GetFormEntity(dataModel);
            var validationResultForEiendom = new EiendomValidator().Validate("ArbeidstilsynetsSamtykke", formEntity.Eiendom);

            var validationRule = validationResultForEiendom.Where(x => x.id.Equals("tillatte_postnr_i_kommune")).FirstOrDefault();
            validationRule.validationResult.Should().Be(ValidationResultEnum.ValidationFailed);

            var validationComposer = new ValidationMessageComposer();
            var newValidationReport = validationComposer.ComposeValidationReport(validationResultForEiendom, "NO");

            if (WriteValidationResultsToJsonFile)
            {
                var jsonString = JsonConvert.SerializeObject(validationResultForEiendom);
                File.WriteAllText(_rootDirTestResults + @"\validationResultsForEiendom_" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".json", jsonString);
            }
        }
    }
}
