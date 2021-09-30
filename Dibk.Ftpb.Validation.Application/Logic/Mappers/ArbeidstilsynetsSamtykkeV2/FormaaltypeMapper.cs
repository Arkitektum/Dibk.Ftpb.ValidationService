using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class FormaaltypeMapper
    {
        public static FormaaltypeValidationEntity Map(FormaalType mapFrom)
        {
            FormaaltypeValidationEntity formaaltype = null;
            if (mapFrom != null)
            {
                formaaltype = new FormaaltypeValidationEntity
                {
                    BeskrivPlanlagtFormaal = mapFrom.beskrivPlanlagtFormaal,
                    Anleggstype = KodelisteValidationEntityMapper.Map(mapFrom.anleggstype),
                    Naeringsgruppe = KodelisteValidationEntityMapper.Map(mapFrom.naeringsgruppe),
                    Bygningstype = KodelisteValidationEntityMapper.Map(mapFrom.bygningstype),
                    Tiltaksformaal = KodelisteValidationEntitiesMapper.Map(mapFrom.tiltaksformaal)
                };
            }

            return formaaltype;
        }
    }
}
