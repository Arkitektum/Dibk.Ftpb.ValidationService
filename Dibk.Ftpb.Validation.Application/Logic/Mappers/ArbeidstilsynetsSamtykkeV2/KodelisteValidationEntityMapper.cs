﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class KodelisteValidationEntityMapper
    {
        public static KodelisteValidationEntity Map(KodeType mapFrom)
        {
            KodelisteValidationEntity kodeliste = null;
            if (mapFrom != null)
                kodeliste = new KodelisteValidationEntity()
                {
                    Kodebeskrivelse = mapFrom.kodebeskrivelse,
                    Kodeverdi = mapFrom.kodeverdi
                };

            return kodeliste;
        }
    }
}
