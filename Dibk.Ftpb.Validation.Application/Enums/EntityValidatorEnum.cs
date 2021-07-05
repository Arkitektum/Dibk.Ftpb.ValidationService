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

        [EnumerationAttribute(XmlNode = "ansvarligSoeker", ValidatorId = "2")]
        AnsvarligSoekerValidator,

        [EnumerationAttribute(XmlNode = "arbeidsplasser", ValidatorId = "3")]
        ArbeidsplasserValidator,

        [EnumerationAttribute(XmlNode = "beskrivelseAvTiltak", ValidatorId = "4")]
        BeskrivelseAvTiltakValidator,

        [EnumerationAttribute(XmlNode = "bygningstype", ValidatorId = "5")]
        BygningstypeValidator,

        [EnumerationAttribute(XmlNode = "dispensasjon", ValidatorId = "6")]
        DispensasjonValidator,

        [EnumerationAttribute(XmlNode = "eiendomByggested{0}", ValidatorId = "7")]
        EiendomByggestedValidator,

        [EnumerationAttribute(XmlNode = "adresse", ValidatorId = "8")]
        EiendomsAdresseValidator,

        [EnumerationAttribute(XmlNode = "adresse", ValidatorId = "9")]
        EnkelAdresseValidator,

        [EnumerationAttribute(XmlNode = "adresse", ValidatorId = "10")]
        EnkelAdresseValidatorV2,

        [EnumerationAttribute(XmlNode = "fakturamottaker", ValidatorId = "11")]
        FakturamottakerValidator,

        [EnumerationAttribute(XmlNode = "bruk", ValidatorId = "12")]
        FormaaltypeValidator,

        [EnumerationAttribute(XmlNode = "kontaktperson", ValidatorId = "13")]
        KontaktpersonValidator,

        [EnumerationAttribute(XmlNode = "eiendomsidentifikasjon", ValidatorId = "14")]
        MatrikkelValidator,

        [EnumerationAttribute(XmlNode = "naeringsgruppe", ValidatorId = "15")]
        NaeringsgruppeValidator,

        [EnumerationAttribute(XmlNode = "partstype", ValidatorId = "16")]
        PartstypeValidator,

        [EnumerationAttribute(XmlNode = "krav{0}", ValidatorId = "17")]
        SjekklistekravValidator,

        [EnumerationAttribute(XmlNode = "sjekklistepunkt", ValidatorId = "18")]
        SjekklistepunktValidator,

        [EnumerationAttribute(XmlNode = "tiltaksformaal", ValidatorId = "19")]
        TiltaksformaalValidator,

        [EnumerationAttribute(XmlNode = "tiltakshaver", ValidatorId = "20")]
        TiltakshaverValidator,

        [EnumerationAttribute(XmlNode = "type", ValidatorId = "21")]
        TiltakstypeValidator,

    }
}
