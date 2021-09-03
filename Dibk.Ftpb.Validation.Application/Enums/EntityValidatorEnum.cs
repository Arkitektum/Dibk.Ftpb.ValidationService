using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Enums
{
    public enum EntityValidatorEnum
    {
        //TODO https://stackoverflow.com/a/60454909

        [EntityValidatorEnumerationAttribute(XmlNode = "anleggstype", ValidatorId = "1")]
        AnleggstypeValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "ansvarligSoeker", ValidatorId = "2")]
        AnsvarligSoekerValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "arbeidsplasser", ValidatorId = "3")]
        ArbeidsplasserValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "beskrivelseAvTiltak", ValidatorId = "4")]
        BeskrivelseAvTiltakValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "bygningstype", ValidatorId = "5")]
        BygningstypeValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "dispensasjon", ValidatorId = "6")]
        DispensasjonValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "eiendomByggested{0}", ValidatorId = "7")]
        EiendomByggestedValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "adresse", ValidatorId = "8")]
        EiendomsAdresseValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "adresse", ValidatorId = "9")]
        TiltakshaverEnkelAdresseValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "adresse", ValidatorId = "10")]
        EnkelAdresseValidatorV2,

        [EntityValidatorEnumerationAttribute(XmlNode = "fakturamottaker", ValidatorId = "11")]
        FakturamottakerValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "bruk", ValidatorId = "12")]
        FormaaltypeValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "kontaktperson", ValidatorId = "13")]
        TiltakshaverKontaktpersonValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "eiendomsidentifikasjon", ValidatorId = "14")]
        MatrikkelValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "naeringsgruppe", ValidatorId = "15")]
        NaeringsgruppeValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "partstype", ValidatorId = "16")]
        TiltakshaverPartstypeValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "krav{0}", ValidatorId = "17")]
        SjekklistekravValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "sjekklistepunkt", ValidatorId = "18")]
        SjekklistepunktValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "tiltaksformaal", ValidatorId = "19")]
        TiltaksformaalValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "tiltakshaver", ValidatorId = "20")]
        TiltakshaverValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "type", ValidatorId = "21")]
        TiltakstypeValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "arbeidsplasser", ValidatorId = "22")]
        ArbeidsplasserValidatorV2,
        
        [EntityValidatorEnumerationAttribute(XmlNode = "kontaktperson", ValidatorId = "23")]
        AnsvarligSoekerKontaktpersonValidator,
        
        [EntityValidatorEnumerationAttribute(XmlNode = "adresse", ValidatorId = "24")]
        AnsvarligSoekerPartstypeValidator,

        [EntityValidatorEnumerationAttribute(XmlNode = "partstype", ValidatorId = "25")]
        
        AnsvarligSoekerEnkelAdresseValidator,
        
        [EntityValidatorEnumerationAttribute(XmlNode = "adresse", ValidatorId = "26")]
        FakturamottakerEnkelAdresseValidator,
        


    }
}
