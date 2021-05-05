using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Xunit;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Dibk.Ftpb.Validation.Application.Tests
{
    public class ArbeidstilsynetsSamtykke245957ValidatorTest
    {
        IMunicipalityValidator _municipalityValidator;
        ArbeidstilsynetsSamtykke2_45957_Validator _formValidator;
        private readonly string rootDirTestResults = @"C:\ATIL_testresults";
        public ArbeidstilsynetsSamtykke245957ValidatorTest()
        {
            _municipalityValidator = MockDataSource.MunicipalityValidatorResult(MunicipalityValidationEnum.Ok);
            _formValidator = new ArbeidstilsynetsSamtykke2_45957_Validator(_municipalityValidator);
        }

        [Fact]
        public void Test1()
        {
            var dataFomr = _formValidator.DeserializeDataForm(@"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'></ArbeidstilsynetsSamtykke>");
        }

        [Fact]
        public void ValidateArbeidstilsynetsSamtykke2()
        {
            string xmlData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><adresse><adresselinje1>Bøgata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3800</postnr><poststed>Bø i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>917</gaardsnummer><bruksnummer>135</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendomByggested></ArbeidstilsynetsSamtykke>";

            var validationResults = _formValidator.StartValidation(xmlData);
            var jsonString = JsonConvert.SerializeObject(validationResults);

            if (!Directory.Exists(rootDirTestResults))
            {
                Directory.CreateDirectory(rootDirTestResults);
            }

            File.WriteAllText(@rootDirTestResults + @"\validationResults_" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".json", jsonString);
        }
    }
}
