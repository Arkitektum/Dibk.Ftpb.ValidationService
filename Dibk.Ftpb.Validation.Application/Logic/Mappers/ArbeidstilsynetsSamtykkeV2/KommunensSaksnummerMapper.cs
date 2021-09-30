﻿using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class KommunensSaksnummerMapper : ModelToValidationEntityMapper<SaksnummerType, SaksnummerValidationEntity>
    {
        public override SaksnummerValidationEntity Map(SaksnummerType mapFrom, string parentElementXpath = null)
        {
            SaksnummerValidationEntity saksnummer = null;
            if (mapFrom != null)
                saksnummer = new SaksnummerValidationEntity()
                {
                    Saksaar = mapFrom.saksaar,
                    Sakssekvensnummer = mapFrom.sakssekvensnummer
                };

            return saksnummer;
        }
    }
}
