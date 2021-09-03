using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class MetadataMapper : ModelToValidationEntityMapper<MetadataType, MetadataValidationEntity>
    {
        public override MetadataValidationEntity Map(MetadataType mapFrom, string parentElementXpath = null)
        {
            Dibk.Ftpb.Validation.Application.Models.ValidationEntities.Metadata metadata = null;
            if (mapFrom != null)
                metadata = new Dibk.Ftpb.Validation.Application.Models.ValidationEntities.Metadata()
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

            return new MetadataValidationEntity(metadata, "metadata", parentElementXpath);
        }
    }
}