using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators.Interfaces;
using Dibk.Ftpb.Validation.Application.Process;
using Dibk.Ftpb.Validation.Application.Reporter;
using Moq;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests
{
    public class ValidationOrchestratorTest
    {
        [Fact]
        public void testArbeidstilsynetOrchestrator()
        {

            //TODO found out if we can test orchestrator starting with test motor
            //var ServiceProvider = new Mock<IServiceProvider>();
            //await new ValidationOrchestrator(ServiceProvider.Object).ExecuteAsync("", "45957");

            string xmlData = @"<?xml version='1.0' encoding='utf-8'?><ArbeidstilsynetsSamtykke xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' dataFormatProvider='SERES' dataFormatId='6821' dataFormatVersion='45957' xmlns='http://skjema.kxml.no/dibk/arbeidstilsynetsSamtykke/2.0'><eiendomByggested><adresse><adresselinje1>Bøgata 1</adresselinje1><adresselinje2 xsi:nil='true' /><adresselinje3 xsi:nil='true' /><postnr>3800</postnr><poststed>Bø i Telemark</poststed><landkode>NO</landkode><gatenavn xsi:nil='true' /><husnr xsi:nil='true' /><bokstav xsi:nil='true' /></adresse><eiendomsidentifikasjon><kommunenummer>3817</kommunenummer><gaardsnummer>917</gaardsnummer><bruksnummer>135</bruksnummer><festenummer>0</festenummer><seksjonsnummer>0</seksjonsnummer></eiendomsidentifikasjon><bygningsnummer>80466985</bygningsnummer><bolignummer>H0102</bolignummer><kommunenavn>Midt Telemark</kommunenavn></eiendomByggested></ArbeidstilsynetsSamtykke>";
            List<ValidationRule> Messages;

            IMunicipalityValidator validatorMock = new MunicipalityValidator();
            var formValidator = new ArbeidstilsynetsSamtykke2_45957_Validator(validatorMock);
            Messages = formValidator.StartValidation(xmlData);

        }
    }
}
