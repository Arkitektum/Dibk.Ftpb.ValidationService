﻿using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.ansvarsrettAnsako;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.Ansako.AnsvarsrettAnsako
{
    public class KontaktpersonMapper
    {
        public static KontaktpersonValidationEntity Map(KontaktpersonType mapFrom)
        {
            KontaktpersonValidationEntity kontaktperson = null;
            if (mapFrom != null)
                kontaktperson = new KontaktpersonValidationEntity()
                {
                    Epost = mapFrom.epost,
                    Mobilnummer = mapFrom.mobilnummer,
                    Navn = mapFrom.navn,
                    Telefonnummer = mapFrom.telefonnummer
                };

            return kontaktperson;
        }
    }
}
