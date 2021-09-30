using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class MetadataMapper
    {
        public Dibk.Ftpb.Validation.Application.Models.ValidationEntities.MetadataValidationEntity Map(MetadataType mapFrom)
        {
            Dibk.Ftpb.Validation.Application.Models.ValidationEntities.MetadataValidationEntity metadataValidationEntity = null;
            if (mapFrom != null)
                metadataValidationEntity = new Dibk.Ftpb.Validation.Application.Models.ValidationEntities.MetadataValidationEntity()
                {
                    ErNorskSvenskDansk = mapFrom.erNorskSvenskDansk,
                    FraSluttbrukersystem = mapFrom.fraSluttbrukersystem,
                    FtbId = mapFrom.ftbId,
                    Hovedinnsendingsnummer = mapFrom.hovedinnsendingsnummer,
                    KlartForSigneringFraSluttbrukersystem = mapFrom.klartForSigneringFraSluttbrukersystem,
                    Prosjektnavn = mapFrom.prosjektnavn,
                    SluttbrukersystemUrl = mapFrom.sluttbrukersystemUrl,
                    UnntattOffentlighet = mapFrom.unntattOffentlighet
                };

            return (metadataValidationEntity);
        }
    }
}