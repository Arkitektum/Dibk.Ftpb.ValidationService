﻿using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class ArbeidstilsynetsSamtykke_41999_Mapper
    {
        public ArbeidstilsynetsSamtykke_41999_ValidationEntity GetFormEntity(ArbeidstilsynetsSamtykkeType dataModel, string XPathRoot)
        {
            var arbeidstilsynetsSamtykkeForm41999 = new ArbeidstilsynetsSamtykke_41999_Form();

            arbeidstilsynetsSamtykkeForm41999.EiendomValidationEntities = new EiendomByggestedMapper().Map(dataModel.eiendomByggested, XPathRoot);
            arbeidstilsynetsSamtykkeForm41999.ArbeidsplasserValidationEntity = new ArbeidsplasserMapper().Map(dataModel.arbeidsplasser, XPathRoot);
            arbeidstilsynetsSamtykkeForm41999.TiltakshaverValidationEntity = new TiltakshaverMapper().Map(dataModel.tiltakshaver, XPathRoot);
            arbeidstilsynetsSamtykkeForm41999.FakturamottakerValidationEntity = new FakturamottakerMapper().Map(dataModel.fakturamottaker, XPathRoot);

            return new ArbeidstilsynetsSamtykke_41999_ValidationEntity(arbeidstilsynetsSamtykkeForm41999, XPathRoot, null);
        }
    }
}
