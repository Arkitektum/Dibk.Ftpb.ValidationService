﻿using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System.IO;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class FakturamottakerValidatorTests
    {
        [Fact]
        public void FakturaTest()
        {
            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke2.xml");
            var form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);

            var fakturamottaker = new FakturamottakerMapper().Map(form.fakturamottaker, null);
            //var fakturamottaker = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().MapFakturamottaker(form.fakturamottaker);
            EntityValidatorOrchestrator entityValidatorOrchestrator = new EntityValidatorOrchestrator();
            IEnkelAdresseValidator enkelAdresseValidator = new EnkelAdresseValidator(entityValidatorOrchestrator, EntityValidatorEnum.FakturamottakerValidator);

            var fakturaResul = new FakturamottakerValidator(entityValidatorOrchestrator, enkelAdresseValidator).Validate(fakturamottaker);

            fakturaResul.Should().NotBeNull();

        }
    }
}
