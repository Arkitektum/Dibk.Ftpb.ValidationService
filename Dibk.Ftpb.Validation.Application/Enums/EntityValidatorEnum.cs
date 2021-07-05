using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Enums
{
    public enum EntityValidatorEnum
    {
        [EnumerationAttribute(XmlNode = "anleggstype", ValidatorId = "1")]
        AnleggstypeValidator,
        AnsvarligSoekerValidator,
        ArbeidsplasserValidator,
        BeskrivelseAvTiltakValidator,
        BygningstypeValidator,
        DispensasjonValidator,

        [EnumerationAttribute(XmlNode = "eiendomByggested{0}", ValidatorId = "7")]
        EiendomByggestedValidator,

        [EnumerationAttribute(XmlNode = "adresse", ValidatorId = "8")]
        EiendomsAdresseValidator,

        EnkelAdresseValidator,
        EnkelAdresseValidatorV2,
        FakturamottakerValidator,
        FormaaltypeValidator,
        KontaktpersonValidator,
        MatrikkelValidator,
        NaeringsgruppeValidator,
        PartstypeValidator,
        SjekklistekravValidator,
        SjekklistepunktValidator,
        TiltaksformaalValidator,
        TiltakshaverValidator,
        TiltakstypeValidator,

    }
}
