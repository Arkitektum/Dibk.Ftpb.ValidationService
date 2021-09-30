using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class FormaaltypeMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.FormaalType, FormaaltypeValidationEntity>
    {
        public override FormaaltypeValidationEntity Map(FormaalType mapFrom, string parentElementXpath = null)
        {
            Formaaltype formaaltype = null;
            if (mapFrom != null)
            {
                formaaltype = new Formaaltype()
                {
                    BeskrivPlanlagtFormaal = mapFrom.beskrivPlanlagtFormaal
                };

                formaaltype.Anleggstype = KodelisteValidationEntityMapper.Map(mapFrom.anleggstype);
                formaaltype.Naeringsgruppe = KodelisteValidationEntityMapper.Map(mapFrom.naeringsgruppe);
                
                formaaltype.Bygningstype = KodelisteValidationEntityMapper.Map(mapFrom.bygningstype);
                formaaltype.Tiltaksformaal = KodelisteValidationEntitiesMapper.Map(mapFrom.tiltaksformaal);
            }

            return new FormaaltypeValidationEntity(formaaltype, "bruk", parentElementXpath);
        }
    }
}
