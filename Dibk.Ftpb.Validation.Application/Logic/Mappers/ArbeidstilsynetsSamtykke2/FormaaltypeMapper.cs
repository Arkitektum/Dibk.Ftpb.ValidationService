using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
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

                formaaltype.Anleggstype = new AnleggstypeMapper().Map(mapFrom.anleggstype, $"{parentElementXpath}/bruk");
                formaaltype.Naeringsgruppe = new NaeringsgruppeMapper().Map(mapFrom.naeringsgruppe, $"{parentElementXpath}/bruk");
                formaaltype.Bygningstype = new BygningstypeMapper().Map(mapFrom.bygningstype, $"{parentElementXpath}/bruk");
                formaaltype.Tiltaksformaal = new TiltaksformaalMapper().Map(mapFrom.tiltaksformaal, $"{parentElementXpath}/bruk");
            }

            return new FormaaltypeValidationEntity(formaaltype, "bruk", parentElementXpath);
        }

        private class AnleggstypeMapper : ModelToValidationEntityMapper<KodeType, KodelisteValidationEntity>
        {
            public override KodelisteValidationEntity Map(KodeType mapFrom, string parentElementXpath = null)
            {
                Kodeliste kodeliste = null;
                if (mapFrom != null)
                    kodeliste = new Kodeliste()
                    {
                        Kodebeskrivelse = mapFrom.kodebeskrivelse,
                        Kodeverdi = mapFrom.kodeverdi
                    };

                return new KodelisteValidationEntity(kodeliste, "anleggstype", parentElementXpath);
            }
        }

        private class NaeringsgruppeMapper : ModelToValidationEntityMapper<KodeType, KodelisteValidationEntity>
        {
            public override KodelisteValidationEntity Map(KodeType mapFrom, string parentElementXpath = null)
            {
                Kodeliste kodeliste = null;
                if (mapFrom != null)
                    kodeliste = new Kodeliste()
                    {
                        Kodebeskrivelse = mapFrom.kodebeskrivelse,
                        Kodeverdi = mapFrom.kodeverdi
                    };

                return new KodelisteValidationEntity(kodeliste, "naeringsgruppe", parentElementXpath);
            }
        }
        private class BygningstypeMapper : ModelToValidationEntityMapper<KodeType, KodelisteValidationEntity>
        {
            public override KodelisteValidationEntity Map(KodeType mapFrom, string parentElementXpath = null)
            {
                Kodeliste kodeliste = null;
                if (mapFrom != null)
                    kodeliste = new Kodeliste()
                    {
                        Kodebeskrivelse = mapFrom.kodebeskrivelse,
                        Kodeverdi = mapFrom.kodeverdi
                    };

                return new KodelisteValidationEntity(kodeliste, "bygningstype", parentElementXpath);
            }
        }
        private class TiltaksformaalMapper : ModelToValidationEntityMapper<KodeType[], IEnumerable<KodelisteValidationEntity>>
        {
            public override IEnumerable<KodelisteValidationEntity> Map(KodeType[] mapFrom, string parentElementXpath = null)
            {

                if (mapFrom == null) return null;
                var retVal = new List<KodelisteValidationEntity>();

                for (int i = 0; i < mapFrom.Count(); i++)
                {
                    var kode = mapFrom[i];

                    var kodeliste = new Kodeliste()
                    {
                        Kodebeskrivelse = kode.kodebeskrivelse,
                        Kodeverdi = kode.kodeverdi
                    };

                    var kodelisteValidationEntity = new KodelisteValidationEntity(kodeliste, $"tiltaksformaal[{i}]", parentElementXpath);

                    retVal.Add(kodelisteValidationEntity);
                }

                return retVal;
            }
        }
    }
}
