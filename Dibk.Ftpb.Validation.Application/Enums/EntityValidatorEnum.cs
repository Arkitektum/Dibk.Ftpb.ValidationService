using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Enums
{
    public enum EntityValidatorEnum
    {
        AnleggstypeValidator,
        AnsvarligSoekerValidator,
        ArbeidsplasserValidator,
        BeskrivelseAvTiltakValidator,
        BygningstypeValidator,
        DispensasjonValidator,
        
        [EnumerationAttribute(XmlNode = "eiendomByggested{0}")]
        EiendomByggestedValidator,

        [EnumerationAttribute(XmlNode = "adresse")]
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
